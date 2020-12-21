[![Gitpod ready-to-code](https://img.shields.io/badge/Gitpod-ready--to--code-blue?logo=gitpod)](https://gitpod.io/#https://github.com/vdimensions/axle)

```
                                           __~(```)~*``)~., __.__ __.__
                                     º~(``(_`)``(``(`)_(```)(`)o~,\__, \
               --  -  -=  = ==  ==  *0~ )*~ (,_(_`,_) ===``________  _\ \_____
                       -  - --  --     (__.(___)  ======== \_ , \_ \/`7) ) _. )
                      -    -   --  P O W E R E D   B Y  == 7 (_7 7 / /7 /  __/\'
                 -    -   -=   ==  ===  ==== ============= \__,_/_/\_7 /\___7\/
                                                            \__,\_\^/_/ /\__\/
                                                                    \_\/
```

# Axle Framework

|Build|Status|
|--|--|
|project|[![Build status](https://ci.appveyor.com/api/projects/status/oe6ued1kbgdsc372?svg=true)](https://ci.appveyor.com/project/ivaylo5ev/axle)|
|`master` branch|[![master branch](https://ci.appveyor.com/api/projects/status/oe6ued1kbgdsc372/branch/master?svg=true)](https://ci.appveyor.com/project/ivaylo5ev/axle/branch/master)|

## About

This repository contains a set of multiple libraries forming together the _Axle Framework_.

The _Axle Framework_ is an application development framework which is aimed at achieving the following goals:

- __Provide a foundation for developing applications of any purpose__  
- __Enable good platform and framework coverage__  
- __Hassle-free__
- __Extensibility__

## Provide a Foundation for Developing Applications of Any Purpose  

The main idea behind Axle is to encourage development of modular applications. With Axle you can split your code in small pieces, called "modules", which you can further organize in different libraries or projects.  
An individual module is a sngleton object which allows you to address a particular aspect of your application promoting separation of concerns. Modules can establish non-circular dependencies among each other and use dependency-injection to obtain references to other dependent modules, or to provide depending modules with injectable objects themselves.  

Most of the _Axle Framework_ itself is designed in the same modular fashion.  

## Enable Good Platform and Framework Coverage

Axle is developed *.NETStandard-first*, meaning that the support of the various [`.NETStandard`](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) framework versions is handled with priority. This allows the framework to cover wide range of .NET-compatible platforms, as the .NETStandard itself evolves, and also guarantees a good degree of forward-compatibility, since future versions of the .NET framework will be incremental improvements of the .NETStandard.

Additionally, in relation to earlier supported versions of the `.NETFramework`, Axle covers `net35` almost completely, and partially supports `net20`. You can refer to our [Multi-Targeting Support](./multitargeting.md) page where you can see how each __Axle Framework__ library supports the different .NETStandard and .NETFramework platforms.

### Axle.Application

If you are developing an application, you may maturally want to be up to date with newest technology developments and use .NET/ASPNET Core, which bring in its own application foundation features such as dependency injeciton, logging abstractions and application hosting. However, these goodies are not available for earlier .NET Framework versions (such as `net40` or earlier) and sometimes we may want to create a modern .NET application that can run on an environment with a legacy framework. Well, we have you covered, because Axle comes with its own foundation (referred to later as `Axle.Application`) that is designed to work well in the old .NET framework world and also to play nice along .NET/ASPNET Core.  

Axle.Application is the core for creating a modern scalable application. It has been inspired by well-known solutions in the .NET world such as Prism/Unity, Smart client software factory, the appliaction bulding blocks introduced with .NET/ASPNET Core, as well as great fremeworks outside the .NET universe, such as Spring and Spring Boot which are among the preferred application frameworks by Java developers to date. The framework offers its own dependency container, logging abstraction and application hosting model which have been designed to enable a powerful modular framework. With Axle.Application you write re-usable components (modules) which enable you to use a modular architecture out of the box and benefit from strong decouping and better maintainability of the software you are creating. And all of these goodies offer you the same development experience, regardless if you are creating a desktop Windows Forms application, a microservice web API project or an ASPNET website.  

Furthermore, the Axle Framework delivers its own set of modules in order to bootstrap your application development. For example, similar to the Spring Boot freamework in Java, you will benefit from automatic datasource discovery and a flexible abstraction on top your database interactions which is designed to play nice with most ORM solutions, as well as to make your life easier if you prefer to use ADO.NET directly. When you are developing an ASPNET Core application, you no longer need to worry of the ASPNETs' hosting model specifics, such as the order of activating the ASPNET Core features (for example -- if you need to use SessionState feature along MVC, the former must be initialized before the MVC feature). Axle provides a declarative approach where you just need to specify that you require the SessionState and MVC modules and it will figure out by itself the right order of their initialization.

On top of the above, Axle.Application enables you to use your preferred DI containers, logging frameworks and to customize the application hosting model, so you have a better control of the infrastrucure layer of your application.

## Hassle-free

Axle is designed to not stand in the way of the developer. While it provides comprehensive core libraries and handy extensions to existing .NET types, you are in no-way obliged to be completely familiar or to depend on using the types the framework comes with if you only need a particular feature, since most things will work out-of-the-box.  

However, we'd be happy to see you adopt Axle because it is capable of solving your problems in a more productive and streamlined manner. We've mentioned before the platform and framework coverage and with a good reason. Writing cross-platform and multi-targeting code with the .NETFramework can be frustrating when we have to deal with globalization, reflection and multi-threading, because the .NETStandard diiffers from the .NETFramework in those important features, and also there is a different level of suppot for the same features among the various .NETStandard versions. Usually, a developer would use conditional compliation with preprocessor directives, such as `#if NET45/#else/endif` to handle these differences, refer to shims (or write his/her own ones), all of which makes using those features less straightforward. It is needless to say that we've walked trough those same issues ourselves while developing Axle, that's why we've made it wrap these functionalities conveniently and, more importantly -- in a consistent way across the different target platforms. We also provided a few shims to port the behavior to earlier platform versions, somewhere with the help of third-party libraries.  

To further support the reliability of the framework, we have comitted to provide incremental improvements in a backwards-compatible manner as part of Axle's own release cycle, so that code which depends on Axle will not break as new versions are being introduced.  

## Extensibility

