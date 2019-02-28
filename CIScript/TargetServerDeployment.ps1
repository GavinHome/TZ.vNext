#
# TargetServerDeployment.ps1
# Ҫ����.NETFRAMWORK 4.5������ PowerShell 2.0������
#

param(
# Web\SQLSERVER\SERVICEĿ��������������ļ�(������������ַ���˺�������)
# Դ�������е�TeamCity���������·��
[xml]$buildServerConfigXml = $(throw "δ����Ŀ��������������ļ�")
)

<#
	��ѹ�ļ�����
#>
Add-Type -AssemblyName System.IO.Compression.FileSystem

function Unzip-File()
{
    param([string]$ZipFile, [string]$TargetFolder)
    [System.IO.Compression.ZipFile]::ExtractToDirectory($ZipFile, $TargetFolder)
}

# BUILD�ڵ�
$buildXml = $buildServerConfigXml.DeploymentConfig.Project | ? { $_.Name -eq "BUILD" } | % { $_.ConfigGroup.ConfigItem }
# BUILD�µ�SharedFolderPhysicalPath�ڵ㣬�����ļ��������ַ
$buildPhysical = $buildXml | ? { $_.Name -eq "SharedFolderPhysicalPath"} | % { $_.'#text' } 
# BUILD�µ�BuildPackageName�ڵ㣬����������
$buildPackageName = $buildXml | ? { $_.Name -eq "BuildPackageName"} | % { $_.'#text' }
# ƴ�������ļ�������·���Լ�����
$buildPhysical = $buildPhysical + "\" + $buildPackageName;

# BUILD�ڵ��µ�SharedFolderPhysicalPath�ڵ�
$unzipPath = $buildXml | ? { $_.Name -eq "SharedFolderPhysicalPath"} | % { $_.'#text' } 
$unzipPath = $unzipPath + "\" + $buildPackageName
# ��ǰʱ��
$dataTime = Get-Date -Format yyyyMMddHHmmss
# ƴ��ѹ�ļ�������Ϊ�������Ƽ���ʱ���ļ���
$unzipPath = $unzipPath + $dataTime

Unzip-File -ZipFile $buildPhysical -TargetFolder $unzipPath #����������ѹ

# ��ѹ����ļ��У�Ҳ������Ŀ��Ŀ¼
$projectRootPhysicalPath = $unzipPath

## ����Web

	# web���ýڵ�
	$websXml = $buildServerConfigXml.DeploymentConfig.Project | ? { $_.Name -eq "WEB" }
	foreach($webNode in $websXml){
		$webXml = $webNode | % { $_.ConfigGroup.ConfigItem } 
		# web�µ�WebSourcePath�ڵ㣬Ҳ������վ�������Ŀ��Ŀ¼�����·��
		$webSourcePath = $webXml | ? { $_.Name -eq "WebSourcePath"} | % { $_.'#text' }
		# web��Ŀ�µ������Ҫ����ʱ�������ļ�
		$lastUsedBuildConfiguration = $webXml | ? { $_.Name -eq "LastUsedBuildConfiguration"} | % { $_.'#text' }
		# web��Ŀ�µĵ�ǰ�����ļ�
		$webConfiguration = $webXml | ? { $_.Name -eq "WebConfiguration"} | % { $_.'#text' }
		# web��Ŀ������·��
		$webPath = $projectRootPhysicalPath + "\" + $webSourcePath
		# web��Ŀ�µ������Ҫ����ʱ�������ļ�����·��
		$webLastUsedBuildConfiguration = $webPath + "\" + $lastUsedBuildConfiguration
		# web��Ŀ�µĵ�ǰ�����ļ�����·��
		$webConfig = $webPath + "\" + $webConfiguration

		# ����web����ģ��
		Import-Module WebAdministration
		# �ر���վ
		Stop-WebSite ( $webXml | ? { $_.Name -eq "DeployIisApp"} | % { $_.'#text' } )
		# ��վ������·��
		$webPhysicalPath = $webXml | ? { $_.Name -eq "RemoteSitePhysicalPath"} | % { $_.'#text' }
		# ��վ�����ļ�XML����
		$webPreviousConfig = [xml](Get-Content ($webPhysicalPath + "\" + $webConfiguration)  -Encoding UTF8)
		# ��ȡ��ǰ��һ�汾��
		$preVersion = [int]$webPreviousConfig.configuration.clientDependency.GetAttribute("version")

		# ��ȡ��ǰ��վ�ļ�XML����
		$webCurrentConfig = [xml](Get-Content $webLastUsedBuildConfiguration -Encoding UTF8)
		# ������վ�汾��
		$webCurrentConfig.configuration.clientDependency.SetAttribute("version", ($preVersion + 1))
		# �����ļ�
		$webCurrentConfig.Save($webLastUsedBuildConfiguration)

		# ��վ�ı�������·��
		$webBackupPhysicalPath = $webXml | ? { $_.Name -eq "RemoteBackupPhysicalPath"} | % { $_.'#text' }
		# �Ƿ���Ҫ����
		$needBackUp = $webXml | ? { $_.Name -eq "Backup"} | % { $_.'#text' }

		# ���������ļ�
		Copy-Item $webConfig  ( $webConfig + ".bak" )
		# �滻�����ļ�
		Copy-Item $webLastUsedBuildConfiguration $webConfig

		If($needBackUp -eq "True"){
			# ������վ
			New-Item -ItemType Directory -Force -Path  ( $webBackupPhysicalPath + "\" + $dataTime + "\" + $webSourcePath )
			Copy-Item -Path ($webPhysicalPath + "\*") -Destination ( $webBackupPhysicalPath + "\" + $dataTime + "\" + $webSourcePath ) -recurse -Force 
		}

		# �����е���վ�ļ���������վ�����ļ�����
		Copy-Item -Path ($webPath + "\*") -Destination $webPhysicalPath -recurse -Force 

		#������վ
		Start-WebSite ( $webXml | ? { $_.Name -eq "DeployIisApp"} | % { $_.'#text' })
	}
	
## ������̨SERVICE
	$programsXml = $buildServerConfigXml.DeploymentConfig.Project | ? { $_.Name -eq "EXE" }
	$exeCommond = @()

	foreach($programNode in $programsXml){
		# ����ڵ�
		$programXml = $programNode | % { $_.ConfigGroup.ConfigItem } 
		# ������������·��
		$programPath = $projectRootPhysicalPath + "\" + ($programXml | ? { $_.Name -eq "ProgramSourcePath"} | % { $_.'#text' })
		# ������Ҫ����������
		$programLastUsedBuildConfiguration = $programPath + "\" + ($programXml | ? { $_.Name -eq "LastUsedBuildConfiguration"} | % { $_.'#text' })
		# ������е�ǰ������
		$programConfig = $programPath + "\" + ($programXml | ? { $_.Name -eq "ProgramConfiguration"} | % { $_.'#text' })
		# ��������
		Copy-Item $programConfig ( $programConfig + ".bak" )
		# �滻����
		Copy-Item $programLastUsedBuildConfiguration $programConfig

		# ���г�������·��
		$programPhysicalPath = $programXml | ? { $_.Name -eq "RemoteProgramPhysicalPath"} | % { $_.'#text' }
		# ���򱸷�·��
		$programBackupPhysicalPath = $programXml | ? { $_.Name -eq "RemoteBackupPhysicalPath"} | % { $_.'#text' }
		# �Ƿ���Ҫ����
		$needBackUp = $programXml | ? { $_.Name -eq "Backup"} | % { $_.'#text' }
		# ��������
		$programName = $programXml | ? { $_.Name -eq "DeployApp"} | % { $_.'#text' }
		# �ر�ָ���ļ����еĽ���
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
			# ���ݳ���
			New-Item -ItemType Directory -Force -Path ($programBackupPhysicalPath + "\" + $dataTime + "\" + ($programXml | ? { $_.Name -eq "ProgramSourcePath"} | % { $_.'#text' }))
			Copy-Item -Path ($programPhysicalPath + "\*") -Destination ($programBackupPhysicalPath + "\" + $dataTime + "\" + ($programXml | ? { $_.Name -eq "ProgramSourcePath"} | % { $_.'#text' })) -recurse -Force
		}

		# ���������������г����·����
		Copy-Item -Path ( $programPath + "\*" ) -Destination $programPhysicalPath -recurse -Force

		# ���н����û�������
		$executeUserName = $programXml | ? { $_.Name -eq "ExecuteUserName"} | % { $_.'#text' };
		$executePassword = $programXml | ? { $_.Name -eq "ExecutePassword" } | % { $_.'#text' };
		# Ŀ����������� 
		$executePassword = ConvertTo-SecureString $executePassword -AsPlainText -Force; 
		# �����Զ���֤����
		$executeCred = New-Object System.Management.Automation.PSCredential($executeUserName, $executePassword);

		$exeSession = New-PSSession -ComputerName localhost -Credential $executeCred

		# ����ָ������
		$exeCommond += Invoke-Command -Session $exeSession -ScriptBlock {param($programPhysicalPath,$programName) Invoke-WmiMethod -path win32_process -name create -argumentlist ( $programPhysicalPath + "\" + $programName )} -ArgumentList $programPhysicalPath, $programName
	}

	$exeCommond

## �������ݿ�
	$databasesXml = $buildServerConfigXml.DeploymentConfig.Project | ? { $_.Name -eq "SQL" }
	foreach($databaseNode in $databasesXml){
		#������Ϣ 
		$databaseXml = $databaseNode | % { $_.ConfigGroup.ConfigItem } 
		$database = $databaseXml | ? { $_.Name -eq "DataBaseName"} | % { $_.'#text' }
		$databaseServer = $databaseXml | ? { $_.Name -eq "DeployServiceURL"} | % { $_.'#text' }
		$databaseUserName = $databaseXml | ? { $_.Name -eq "UserName"} | % { $_.'#text' }
		$databasePassword = $databaseXml | ? { $_.Name -eq "PassWord"} | % { $_.'#text' }

		$sqlScriptfile = $databaseXml | ? { $_.Name -eq "ExecuteSqlPath"} | % { $_.'#text' }

		#�������Ӷ���
		$SqlConn = New-Object System.Data.SqlClient.SqlConnection

		#ʹ���˺�����MSSQL
		$SqlConn.ConnectionString = "Data Source=$databaseServer;Initial Catalog=$database;user id=$databaseUserName;pwd=$databasePassword"

		#�����ݿ�����
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

		#�ر����ݿ�����  
		$SqlConn.close()
	}
	