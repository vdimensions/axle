# Contribution Guidelines

## Welcome, dear Contributor!

If you are a developer and want to submit code changes, bugfixes or even featerures, then you shoud follow the guidelines included herein.

## C# Code Conventions

The Axle project conventions are based upon the standard code conventions for C# on all C# projects. You may refer to thsese online sources for detailed reference:

- [MSDN](https://msdn.microsoft.com/en-us/library/ff926074.aspx)
- [dofactory](http://www.dofactory.com/reference/csharp-coding-standards)

### General

Here are a number of notable rules which we highly encouragle you to follow:

1. Always use brackets, even for simple statements:

        if (a == b)
        {
            Console.WriteLine("True");
        }

2. Use spaces to ident, no tab symbols. Configure your Editor/IDE not to place tabs.
3. Use identation size of 4 spaces.
4. Place each class in a separate .cs file. Exceptions are allowed for small nested types and delegates. Types with the same name and different number of generic type arguments are also allowed to reside in the same file.
5. Partial class rules:
   - When creating partial classes, include meaningful name suffix in the .cs file. For example, if you want to separate certain interface implementation in a different partial class, use the following naming pattern `{YOUR_CLASS_NAME}.{INTERFACE_NAME}.cs`.  
   - At least one of the partial classes (considered the primary part) must abide to the default name pattern `{CLASS_NAME}.cs`.  
   - Specify qualifiers such as `sealed` and `protected` only in the primary part of the file and omit them in all other parts. The compiler will infer them correctly, but will not allow you to mix, for example `private` and `internal` in different parts for the same type. Keeping the qualifiers in one place will contribute to better code readability and will allow class visibility changes to be controlled from a single location.  
   - Do not use partial classes to define interfaces. It is hard to gasp the API that the particular interface exposes if we have to look that up in multiple places.  
   - Do not use partial classes to define `static` classes containing extension methods. For extension methods, consider multiple `static` classes that group methods based on logical relevance. You may always place these classes in the same namespace if you prefer them to be imported at once.  
6. Document the functionality of all the publicly visible members of your classes, structs or interfaces.  

### Extension Methods

The extension methods by nature are merely static methods with enhanced meaning to the compiler. Every imported namespace trough the `using` directive that contains extension methods will automatically make them available for usage. It is therefore possible to have two extension methods comming from different sources to become simultaneously available, given the appropriate imports are in place. Imagine if some of the imported methods have the same signature -- the compiler will choke in confusion on the convenient extension-method-call synthax, and will force the developer to perform static method invocation on the desired extension method. This is not really convenient and practically __defeats__ the purpose of the extension methods.  

Because in the Axle framework we introduce various extension methods of our own, we want to avoid potential conflicts with existing extension methods coming from any third party libraries your project may reference, the extensions defined by the .NET framework itself, or even your own extension methods. While it is impossible to know beforehand of all the potential collisions that may occur, we have adopted a strategy that helps mitigate this problem. To get around the issue, we have established some rules which we'd like you to follow:

- Consider using a dedicated namespace for extension methods directly under the root namespace, such as: `Axle.Extensions` in the `Axle` project.
- Create a sub-namespace with the name of the type you are extending. For example, if you write extensions methods on the `String` class, you should be defining the `Axle.Extensions.String` namespace to place your static class inside. This particular step provides another layer of isolation for the extension method's scope, and also helps with code readability as the extended type's name is present in the usings list.  
- If you are extending an `interface` type, ommit the preceding `I` character of the interface name. For example `Axle.Extensions.Comparable` will contain extension methods to the `IComparable` interface.  
- Avoid name clashes with known extension methods on the extended type. For example, do not use names such as `Select` or `OrderBy` on types extending `IEnumerable<T>`, since methods with these names are already defined in the `System.Linq` namespace.

### Mixin Extensions

We define two types of extension methods - **general purpose** extension methods and **mixins**.
We are refering to all standard extension methods as being **general purpose** and the former guidelines are directed at those. It is most likely that your extension methods fall in this category.

We call **mixins** extension methods that are:  

- Defined on an interface which *comes from the Axle framework itself*.
- Are placed in the same file or a file in the same namespace as the interface.
- Introduces a common behavior that is is completely agnostic of the possible implementations.

Example scenario for a good **mixin** candidate is a convenience extension that internally invokes the existing methods or properties exposed by the extended interface. Placing the mixin alongside the interface being extended will make the extension available to developers without forcing them to import the extension namespaces, simply because it will be anyway imported because of the interface.  

## Versioning & Backwards Compatibility

We advocate for following the semantic version guidelines. Breaking changes must be avoided if possible, and should be introduced only as major releases.  

## Code of Conduct

We believe that good things are born and last in an environment which is positive and provides for collaboration, creativity and fulfilment. Given that the world we live in is often harsh unpredictable, it is our responsibility, as individuals, to create and maintain a healthy surrouding and constructive mindset. That is why we encourage you to be kind and respecful to both the work and personality of others.  
