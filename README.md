# Realtime-Socket-AsyncChat
基于c#用unity引擎开发的一款实时异步聊天室Demo  
## 服务端
使用Visual Studio 2022 .NET FrameWork window窗体应用；  
解决方案中除聊天室的项外还包含了两种Socket连接模板，聊天室则采用其中的socket异步连接方式。
## 客户端
unity版本：2018.4.36f1  
三个场景分别对应服务端解决方案中三个项目，其中chat为聊天室场景。
## 说明
原型为书籍《unity3D网络游戏实战》中聊天室的练习项目，部分代码对书本进行了参考。  
本项目在其原基础上更改控制台形式服务端为窗体应用，另外实现了根据另一客户端IP及端口进行单独聊天等功能。
