#
# TargetServerDeployment.ps1
# 要求有.NETFRAMWORK 4.5及以上 PowerShell 2.0及以上
#

param(
# Web\SQLSERVER\SERVICE目标服务器的配置文件(包括服务器地址，账号与密码)
# 源服务器中的TeamCity发布包存放路径
[xml]$buildServerConfigXml = $(throw "未输入目标服务器的配置文件")
)

<#
	解压文件函数
#>
Add-Type -AssemblyName System.IO.Compression.FileSystem

function Unzip-File()
{
    param([string]$ZipFile, [string]$TargetFolder)
    [System.IO.Compression.ZipFile]::ExtractToDirectory($ZipFile, $TargetFolder)
}

# BUILD节点
$buildXml = $buildServerConfigXml.DeploymentConfig.Project | ? { $_.Name -eq "BUILD" } | % { $_.ConfigGroup.ConfigItem }
# BUILD下的SharedFolderPhysicalPath节点，共享文件夹物理地址
$buildPhysical = $buildXml | ? { $_.Name -eq "SharedFolderPhysicalPath"} | % { $_.'#text' } 
# BUILD下的BuildPackageName节点，打包后的名称
$buildPackageName = $buildXml | ? { $_.Name -eq "BuildPackageName"} | % { $_.'#text' }
# 拼打包后的文件夹完整路径以及名称
$buildPhysical = $buildPhysical + "\" + $buildPackageName;

# BUILD节点下的SharedFolderPhysicalPath节点
$unzipPath = $buildXml | ? { $_.Name -eq "SharedFolderPhysicalPath"} | % { $_.'#text' } 
$unzipPath = $unzipPath + "\" + $buildPackageName
# 当前时间
$dataTime = Get-Date -Format yyyyMMddHHmmss
# 拼解压文件夹名称为，包名称加上时间文件夹
$unzipPath = $unzipPath + $dataTime

Unzip-File -ZipFile $buildPhysical -TargetFolder $unzipPath #将发布包解压

# 解压后的文件夹，也就是项目根目录
$projectRootPhysicalPath = $unzipPath

## 发布Web

	# web配置节点
	$websXml = $buildServerConfigXml.DeploymentConfig.Project | ? { $_.Name -eq "WEB" }
	foreach($webNode in $websXml){
		$webXml = $webNode | % { $_.ConfigGroup.ConfigItem } 
		# web下的WebSourcePath节点，也就是网站相对于项目根目录的相对路径
		$webSourcePath = $webXml | ? { $_.Name -eq "WebSourcePath"} | % { $_.'#text' }
		# web项目下的最后需要发布时的配置文件
		$lastUsedBuildConfiguration = $webXml | ? { $_.Name -eq "LastUsedBuildConfiguration"} | % { $_.'#text' }
		# web项目下的当前配置文件
		$webConfiguration = $webXml | ? { $_.Name -eq "WebConfiguration"} | % { $_.'#text' }
		# web项目的物理路径
		$webPath = $projectRootPhysicalPath + "\" + $webSourcePath
		# web项目下的最后需要发布时的配置文件物理路径
		$webLastUsedBuildConfiguration = $webPath + "\" + $lastUsedBuildConfiguration
		# web项目下的当前配置文件物理路径
		$webConfig = $webPath + "\" + $webConfiguration

		# 导入web管理模块
		Import-Module WebAdministration
		# 关闭网站
		Stop-WebSite ( $webXml | ? { $_.Name -eq "DeployIisApp"} | % { $_.'#text' } )
		# 网站的物理路径
		$webPhysicalPath = $webXml | ? { $_.Name -eq "RemoteSitePhysicalPath"} | % { $_.'#text' }
		# 网站配置文件XML对象
		$webPreviousConfig = [xml](Get-Content ($webPhysicalPath + "\" + $webConfiguration)  -Encoding UTF8)
		# 获取当前上一版本号
		$preVersion = [int]$webPreviousConfig.configuration.clientDependency.GetAttribute("version")

		# 获取当前网站文件XML对象
		$webCurrentConfig = [xml](Get-Content $webLastUsedBuildConfiguration -Encoding UTF8)
		# 设置网站版本号
		$webCurrentConfig.configuration.clientDependency.SetAttribute("version", ($preVersion + 1))
		# 保存文件
		$webCurrentConfig.Save($webLastUsedBuildConfiguration)

		# 网站的备份物理路径
		$webBackupPhysicalPath = $webXml | ? { $_.Name -eq "RemoteBackupPhysicalPath"} | % { $_.'#text' }
		# 是否需要备份
		$needBackUp = $webXml | ? { $_.Name -eq "Backup"} | % { $_.'#text' }

		# 备份配置文件
		Copy-Item $webConfig  ( $webConfig + ".bak" )
		# 替换配置文件
		Copy-Item $webLastUsedBuildConfiguration $webConfig

		If($needBackUp -eq "True"){
			# 备份网站
			New-Item -ItemType Directory -Force -Path  ( $webBackupPhysicalPath + "\" + $dataTime + "\" + $webSourcePath )
			Copy-Item -Path ($webPhysicalPath + "\*") -Destination ( $webBackupPhysicalPath + "\" + $dataTime + "\" + $webSourcePath ) -recurse -Force 
		}

		# 将包中的网站文件拷贝至网站物理文件夹中
		Copy-Item -Path ($webPath + "\*") -Destination $webPhysicalPath -recurse -Force 

		#开启网站
		Start-WebSite ( $webXml | ? { $_.Name -eq "DeployIisApp"} | % { $_.'#text' })
	}
	
## 发布后台SERVICE
	$programsXml = $buildServerConfigXml.DeploymentConfig.Project | ? { $_.Name -eq "EXE" }
	$exeCommond = @()

	foreach($programNode in $programsXml){
		# 程序节点
		$programXml = $programNode | % { $_.ConfigGroup.ConfigItem } 
		# 发布程序物理路径
		$programPath = $projectRootPhysicalPath + "\" + ($programXml | ? { $_.Name -eq "ProgramSourcePath"} | % { $_.'#text' })
		# 程序需要发布的配置
		$programLastUsedBuildConfiguration = $programPath + "\" + ($programXml | ? { $_.Name -eq "LastUsedBuildConfiguration"} | % { $_.'#text' })
		# 程序包中当前的配置
		$programConfig = $programPath + "\" + ($programXml | ? { $_.Name -eq "ProgramConfiguration"} | % { $_.'#text' })
		# 备份配置
		Copy-Item $programConfig ( $programConfig + ".bak" )
		# 替换配置
		Copy-Item $programLastUsedBuildConfiguration $programConfig

		# 运行程序物理路径
		$programPhysicalPath = $programXml | ? { $_.Name -eq "RemoteProgramPhysicalPath"} | % { $_.'#text' }
		# 程序备份路径
		$programBackupPhysicalPath = $programXml | ? { $_.Name -eq "RemoteBackupPhysicalPath"} | % { $_.'#text' }
		# 是否需要备份
		$needBackUp = $programXml | ? { $_.Name -eq "Backup"} | % { $_.'#text' }
		# 程序名称
		$programName = $programXml | ? { $_.Name -eq "DeployApp"} | % { $_.'#text' }
		# 关闭指定文件夹中的进程
		Get-Process | Where-Object { $_.Path -like { "*" + $programPhysicalPath + "*" }} | Stop-Process
		$process = Get-WmiObject win32_process | ? {$_.Path -eq ( $programPhysicalPath + "\" + $programName )} | Select-Object
		if($process){
		    $returnval = $process.terminate()
			$processid = $process.handle
			if($returnval.returnvalue -eq 0) {
			  write-host "The process `($processid`) terminated successfully"
			}
			else {
			  write-host "The process `($processid`) termination has some problems"
			}
		}

		If($needBackUp -eq "True"){
			# 备份程序
			New-Item -ItemType Directory -Force -Path ($programBackupPhysicalPath + "\" + $dataTime + "\" + ($programXml | ? { $_.Name -eq "ProgramSourcePath"} | % { $_.'#text' }))
			Copy-Item -Path ($programPhysicalPath + "\*") -Destination ($programBackupPhysicalPath + "\" + $dataTime + "\" + ($programXml | ? { $_.Name -eq "ProgramSourcePath"} | % { $_.'#text' })) -recurse -Force
		}

		# 将打包程序放入运行程序的路径中
		Copy-Item -Path ( $programPath + "\*" ) -Destination $programPhysicalPath -recurse -Force

		# 运行进程用户名密码
		$executeUserName = $programXml | ? { $_.Name -eq "ExecuteUserName"} | % { $_.'#text' };
		$executePassword = $programXml | ? { $_.Name -eq "ExecutePassword" } | % { $_.'#text' };
		# 目标服务器密码 
		$executePassword = ConvertTo-SecureString $executePassword -AsPlainText -Force; 
		# 创建自动认证对象
		$executeCred = New-Object System.Management.Automation.PSCredential($executeUserName, $executePassword);

		$exeSession = New-PSSession -ComputerName localhost -Credential $executeCred

		# 运行指定程序
		$exeCommond += Invoke-Command -Session $exeSession -ScriptBlock {param($programPhysicalPath,$programName) Invoke-WmiMethod -path win32_process -name create -argumentlist ( $programPhysicalPath + "\" + $programName )} -ArgumentList $programPhysicalPath, $programName
	}

	$exeCommond

## 发布数据库
	$databasesXml = $buildServerConfigXml.DeploymentConfig.Project | ? { $_.Name -eq "SQL" }
	foreach($databaseNode in $databasesXml){
		#配置信息 
		$databaseXml = $databaseNode | % { $_.ConfigGroup.ConfigItem } 
		$database = $databaseXml | ? { $_.Name -eq "DataBaseName"} | % { $_.'#text' }
		$databaseServer = $databaseXml | ? { $_.Name -eq "DeployServiceURL"} | % { $_.'#text' }
		$databaseUserName = $databaseXml | ? { $_.Name -eq "UserName"} | % { $_.'#text' }
		$databasePassword = $databaseXml | ? { $_.Name -eq "PassWord"} | % { $_.'#text' }

		$sqlScriptfile = $databaseXml | ? { $_.Name -eq "ExecuteSqlPath"} | % { $_.'#text' }

		#创建连接对象
		$SqlConn = New-Object System.Data.SqlClient.SqlConnection

		#使用账号连接MSSQL
		$SqlConn.ConnectionString = "Data Source=$databaseServer;Initial Catalog=$database;user id=$databaseUserName;pwd=$databasePassword"

		#打开数据库连接
		$SqlConn.open()

		$backUpPath = ($databaseXml | ? { $_.Name -eq "RemoteBackupPhysicalPath"} | % { $_.'#text' }) + "\" + ($databaseXml | ? { $_.Name -eq "DataBaseName"} | % { $_.'#text' }) + $dataTime + ".bak"
		[System.Text.StringBuilder]$SqlString = ""
		$SqlCmd = $SqlConn.CreateCommand()
		$dataBaseNeedBackUp = $databaseXml | ? { $_.Name -eq "Backup"} | % { $_.'#text' }
		if($dataBaseNeedBackUp -eq "True"){
			$SqlString = "USE [$database]; BACKUP DATABASE [$database] To Disk = '$backUpPath';"
			$SqlCmd.commandtext = $SqlString
			$SqlCmd.ExecuteScalar()
		}
	    
		$SQLCommandText = @(Get-Content -Path ($projectRootPhysicalPath + "\" + $sqlScriptfile))
		foreach($SQLCommandOne in $SQLCommandText)
		{
			if($SQLCommandOne -ne "go")
			{
				# Preparation of SQL packet
				$SQLPacket += $SQLCommandOne + "`n"
			}
			else
			{
				Write-Host "---------------------------------------------"
				Write-Host "Executed SQL packet:"
				Write-Host $SQLPacket
				$IsSQLErr = $false
				# Execution of SQL packet
				try
				{
					$SqlCmd.commandtext = $SQLPacket
					$SqlCmd.ExecuteScalar()
				}
				catch
				{
					$IsSQLErr = $true
				}
				if(-not $IsSQLErr)
				{
					Write-Host "Execution successful"
				}
				else
				{
					Write-Host "Execution failed" -ForegroundColor Red
				}
			
				$SQLPacket = ""
			}
		}

		#关闭数据库连接  
		$SqlConn.close()
	}
	