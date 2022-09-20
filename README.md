# 基于 ASP.NET 的图书管理系统
使用基于 ASP.NET Core 的 B/S 应用，数据库是 MySQL，编程语言是 C# ，SDK 是 .NET 5 ，考虑到学习和稳定使用，没有选择最新版的 SDK 6。整体采用 MVC 架构，开发平台用的是 JetBrains 的 Rider。

在服务器与数据库的连接方面，我选择了微软的 Entity Framework Core 这个库作为ORM，使用这个库可以用 C# 的 Linq 语法进行数据库操作，在与数据库连接时会转成 SQL 语句。

在项目分层时，我把项目大致分为：

Entities：映射数据库表的实体类。

Dal：数据库操作层

Controllers：控制器。用于处理 Web 请求，并且作为业务逻辑层（Bll）使用，因为此项目无太复杂的业务逻辑处理，就没单独抽出来作为一层（如 Service）。

Views：视图。用于图形界面显示，整体为 HTML+CSS+C# 的 .cshtml 格式文件。

Models：此 Model 层主要用于 Controllers 与 Views 交互。

项目文件主要结构树：
```
└───Library.Web
    ├───Controllers
    ├───Dal
    ├───Entities
    ├───Migrations
    ├───Models
    │   └───EnumModels
    ├───Views
    │   ├───Account
    │   ├───Admin
    │   ├───Home
    │   └───Shared
    └───wwwroot
```
wwwroot 中存放的是 Web 页面需要的静态资源文件，主要是 Bootstrap 和 jQuery 库。

[图片展示](https://github.com/yazawaniconii/LibraryDemo/blob/main/SCREEMSHOTS.md)

