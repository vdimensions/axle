# Axle Framework

|Build|Status|
|--|--|
|project|[![Build status](https://ci.appveyor.com/api/projects/status/oe6ued1kbgdsc372?svg=true)](https://ci.appveyor.com/project/ivaylo5ev/axle)|
|`master` branch|[![master branch](https://ci.appveyor.com/api/projects/status/oe6ued1kbgdsc372/branch/master?svg=true)](https://ci.appveyor.com/project/ivaylo5ev/axle/branch/master)|
|`dev` branch|[![master branch](https://ci.appveyor.com/api/projects/status/oe6ued1kbgdsc372/branch/dev?svg=true)](https://ci.appveyor.com/project/ivaylo5ev/axle/branch/dev)|

## About

This repository contains a set of multiple libraries forming together the _Axle Framework_.

_Axle Framework_ is an application development framework which is aimed at achieving the following goals:

- __Provide a foundation for developing applications of any purpose__  
- __Enable good platform and framework coverage__  
- __Hassle-free__
- __Extensibility__

## Provide a Foundation for Developing Applications of Any Purpose  

The main idea behind Axle is to encourage development of modular applications. With Axle you can split your code in small pieces, called "modules", which you can further organize in different libraries or projects.  
An individual module is a sngleton object which allows you to address a particular aspect of your application promoting separation of concerns. Modules can establish non-circular dependencies among each other and use dependency-injection to obtain references to other dependent modules, or to provide depending modules with injectable objects themselves.  

Most of the _Axle Framework_ itself is designed in the same modular fashion.  

## Enable Good Platform and Framework Coverage

Axle is developed .NETStandard-first, meaning that the support of the various [`.NETStandard`](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) framework versions is handled with priority. This allows the framework to cover wide range of .NET-compatible platforms, as the .NETStandard itself evolves, and also guarantees a good degree of forward-compatibility, since future versions of the .NET framework will be incremental improvements of the .NETStandard.

In addition, in relation to earlier supported versions of the `.NETFramework`, Axle covers `net35` almost completely, and partially supports `net20`. You can refer to our [Multi-Targeting Support](./multitargeting.md) page where you can see how each __Axle Framework__ library supports the different .NETStandard and .NETFramework platforms.

## Hassle-free

Axle is designed to not stand in the way of the developer. While it provides comprehensive core libraries and handy extensions to existing .NET types, you are in no-way obliged to depend or use most of the types the framework provides, therefore it is going to work nicely with existing code.  

Instead, we'd like you to adopt Axle because it is capable of solving your particular problems in a more productive and streamlined manner. We've mentioned before the platform and framework coverage and with a good reason. Writing cross-platform and multi-targeting code with the .NETFramework can be frustrating when we have to deal with globalization, reflection and multi-threading, becauses those important features of the .NET have differences between the .NETStandard and the .NETFramework platforms, and also have different level of suppot among the various .NETStandard versions. Usually, a developer would use conditional compliation with preprocessor directives, such as `#if NET45/#else/endif` to handle these differences, and that is chunky and hardly-readable code. Axle provides convenient wrappers of the same functionalities which are consistent across the different target platforms and also brings a long a few shims to port the behavior to earlier platform versions.  

To further support this paradigm, since version 1.10.x, we have comitted to provide incremental improvements and strong backwards compatibility in Axle's own release cycle so that code which depends on Axle will not break as we introduce new versions. 

## Extensibility

