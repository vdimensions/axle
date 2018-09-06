namespace Axle.Application.Forest

open System
open System.Globalization

open Forest.Templates
open Forest.Templates.Raw
open Forest.Templates.Xml

open Axle.Resources
open Axle.Resources.Extraction
open Axle.Resources.Xml
open Axle.Resources.Xml.Extraction


module internal ResourceManagerIntegration =
    [<Literal>]
    let BundleName = "ForestTemplates"

    type [<Sealed>] TemplateResourceInfo (name:string, culture:CultureInfo, template:Template, originalResource:ResourceInfo) =
        inherit ResourceInfo(name, culture, "text/forest-template+xml")
        
        override this.Open() =
            try 
                originalResource.Open()
            with 
            | e -> raise <| ResourceLoadException(name, this.Bundle, culture, e)
        member private __.BaseTryResolve (a:Type, b:obj byref) = base.TryResolve(a, &b)
        override this.TryResolve(targetType:Type, result:obj byref) =
            if (targetType = typeof<Template>)
            then
                result <- (upcast this.Value:obj)
                true
            else this.BaseTryResolve (targetType, &result)
        member __.Value with get() = template

    type [<Sealed>] private ResourceChainTemplateProvider(chain:ContextExtractionChain, bundle:string, culture:CultureInfo, parser:ITemplateParser) =
        interface ITemplateProvider with
            member __.Load name =
                let resourceName = name
                let resource = chain.Extract resourceName
                if (obj.ReferenceEquals(resource, null)) then raise <| ResourceNotFoundException(name, bundle, culture)
                use stream = resource.Open()
                stream |> parser.Parse name

    type [<Sealed>] private XmlTemplateValueExtractor() =
        inherit AbstractResourceExtractor()
         override __.DoExtract(ctx:ResourceContext, name:string) =
            let xmlName = String.Format("{0}.xml", name)
            match XDocumentExtractor().Extract(ctx, xmlName) with
            | :? XDocumentResourceInfo as xdoc ->
                let template = xdoc.Value |> XmlTemplateParser().ParseXml name 
                upcast TemplateResourceInfo(name, ctx.Culture, template, name |> ctx.ExtractionChain.Extract)
            | _ -> Unchecked.defaultof<_>

    let createExtractors () : IResourceExtractor = 
        upcast XmlTemplateValueExtractor()
