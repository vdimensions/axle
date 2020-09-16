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

Axle is developed .NETStandard-first, meaning that the support of the various [`.NETStandard`](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) framework versions is handled with priority. This allows the framework to cover wide range of .NET-compatible platforms, as the .NETStandard itself evolves, and also guarantees a good degree of forward-compatibility, since future versions of the .NET framework will be incremental improvements of the .NETStandard.

Additionally, in relation to earlier supported versions of the `.NETFramework`, Axle covers `net35` almost completely, and partially supports `net20`. You can refer to our [Multi-Targeting Support](./multitargeting.md) page where you can see how each __Axle Framework__ library supports the different .NETStandard and .NETFramework platforms.

## Hassle-free

Axle is designed to not stand in the way of the developer. While it provides comprehensive core libraries and handy extensions to existing .NET types, you are in no-way obliged to be completely familiar or to depend on using the types the framework comes with if you only need a particular feature, since most things will work out-of-the-box.  

However, we'd be happy to see you adopt Axle because it is capable of solving your problems in a more productive and streamlined manner. We've mentioned before the platform and framework coverage and with a good reason. Writing cross-platform and multi-targeting code with the .NETFramework can be frustrating when we have to deal with globalization, reflection and multi-threading, because the .NETStandard diiffers from the .NETFramework in those important features, and also there is a different level of suppot for the same features among the various .NETStandard versions. Usually, a developer would use conditional compliation with preprocessor directives, such as `#if NET45/#else/endif` to handle these differences, refer to shims (or write his/her own ones), all of which makes using those features less straightforward. It is needless to say that we've walked trough those same issues ourselves while developing Axle, that's why we've made it wrap these functionalities conveniently and, more importantly -- in a consistent way across the different target platforms. We also provided a few shims to port the behavior to earlier platform versions, somewhere with the help of third-party libraries.  

To further support the reliability of the framework, we have comitted to provide incremental improvements in a backwards-compatible manner as part of Axle's own release cycle, so that code which depends on Axle will not break as new versions are being introduced.  

## Extensibility

