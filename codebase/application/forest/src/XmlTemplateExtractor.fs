namespace Axle.Application.Forest

open System
open System.Globalization

open Forest.Templates.Raw
open Forest.Templates.Xml

open Axle.Resources
open Axle.Resources.Extraction


type [<Sealed>] TemplateResourceInfo internal(name:string, culture:CultureInfo, template:TemplateDefinition, originalResource:ResourceInfo) =
    inherit ResourceInfo(name, culture, "text/forest-template+xml")
    static member val internal BundleName = "ForestTemplates"
    override this.Open() =
        try 
            originalResource.Open()
        with 
        | e -> raise <| ResourceLoadException(name, this.Bundle, culture, e)
    member private __.BaseTryResolve (a:Type, b:obj byref) = base.TryResolve(a, &b)
    override this.TryResolve(targetType:Type, result:obj byref) =
        if (targetType = typeof<TemplateDefinition>)
        then
            result <- (upcast this.Value:obj)
            true
        else this.BaseTryResolve (targetType, &result)
    member __.Value with get() = template

type [<Sealed>] ResourceTemplateProvider(chain:ContextExtractionChain, bundle:string, culture:CultureInfo) =
    interface ITemplateProvider with
        member __.Load name =
            let resourceName = String.Format("{0}.xml", name)
            let resource = resourceName |> chain.Extract
            if (obj.ReferenceEquals(resource, null)) then raise <| ResourceNotFoundException(name, bundle, culture)
            use stream = resource.Open()
            stream |> XmlTemplateParser().Parse name

type [<Sealed>] XmlTemplateExtractor() =
    inherit AbstractResourceExtractor()
    override __.DoExtract(ctx:ResourceContext, name:string) =
        try
            let provider = ResourceTemplateProvider(ctx.ExtractionChain, ctx.Bundle, ctx.Culture)
            let template = Raw.loadTemplate provider name
            upcast TemplateResourceInfo(name, ctx.Culture, template, name |> ctx.ExtractionChain.Extract)
        with
        | :? ResourceNotFoundException -> Unchecked.defaultof<_>


