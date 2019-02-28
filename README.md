# TZ.vNext

#### 项目介绍
框架代号"vNext"，目的是基于asp.net core集成优秀的技术框架，形成一套完善的前后端分离的跨平台开发的企业级开发框架。前端采用vue + typescript等技术，后台数据访问采用odata标准。

#### 软件架构
asp.net core mvc/webapi + odata + vue + typescript 

#### 安装教程

1. git clone http://172.16.226.146/TZIWB/TZ.vNext
2. cd TZ.vNext
3. dotnet restore
4. cd TZ.vNext.Web
5. npm install
6. 安装vscode 
7. cd TZ.vNext
8. 用以下命令打开项目:code .
9. F5运行

#### 使用说明

1. 前端采用kendo-vue & element ui，具体请前往各自官方技术网站参考学习
2. 后台框架：asp.net core  & odata，具体请前往各自官方技术网站参考学习
3. 本项目可运行至win, mac, linux。win下请使用vs2017/vscode开发;mac下推荐vscode开发，毕竟mac下的vs太大，而且还不是很成熟;linux，鉴于此平台下属于高级玩家的范围，所以我个人建议使用一般的文本编辑器，使用命令编译运行，当然也可以使用vscode等工具，具体请google学习
4. dotnet core 项目的编译运行等基础知识请前往微软技术网站参考学习
5. EntityFrameworkProfiler: 抓sql工具，目前仅用于windows端；使用方法参考最后一部分列出的参考网址。

注：第五步之后，运行项目可能会报错误，是typescript的一个语法错误，此时需要修改两个文件:
\TZ.vNext.Web\node_modules\vue-router\types\roueter.d.ts
\TZ.vNext.Web\node_modules\vue-router\types\vue.d.ts 
将这两个文件中的import Vue = require("vue")修改为import Vue  from 'vue'

#### 参与贡献

1. Fork 本项目
2. 新建 Feat_xxx 分支
3. 提交代码
4. 新建 Pull Request


#### todo list

1. 权限控制，考虑使用token的方式，需要形成一套单独的机制，包括grid等组件权限控制机制等
2. 日志，异常处理等基础性的工作
3. 多数据库多环境
4. 前后端打包配置，后端teamcity, 前端webpack已做了一部分工作，但是需要优化
5. 框架级样式调整
6. 公共组件开发：文件上传/下载，导入导出.....[后续补充]
7. 一个长期的计划：所有框架的知识要点需要形成一个博客文章系列，不仅仅是介绍的形式，包括介绍，使用，以及进阶主题等等。

#### 打包脚本
1. 编译： dotnet build -c Release
2. 发布： dotnet publish TZ.vNext.Web.csproj -c Release -f netcoreapp2.0  -o ./publish/
3. 运行： dotnet TZ.vNext.Web.dll

#### 参考链接
1. element ui : http://element-cn.eleme.io/#/zh-CN/component/installation

2. kendo-vue : https://www.telerik.com/kendo-vue-ui/components/grid/

3. vue : https://vuejs.org/v2/guide/

4. odata : http://odata.github.io/WebApi/

5. typescript : http://www.typescriptlang.org/docs/home.html

6. dotnet core : https://www.microsoft.com/net/learn/get-started/macos

7. asp.net core : https://docs.microsoft.com/zh-cn/aspnet/core/?view=aspnetcore-2.1

8. EntityFrameworkProfiler: https://www.hibernatingrhinos.com/products/efprof/learn

#### 其他说明

1. 运行npm install，后运行出现错误：类似找不到Vue，此时请联系作者，告诉你如何修改npm代码后成功运行，或者你也可以自己google。
2. 本项目服务端依赖"Telerik.UI.for.AspNet.Core", 如果还原包时报错，请添加手动添加nuget源：https://nuget.telerik.com/nuget， 并再次还原。