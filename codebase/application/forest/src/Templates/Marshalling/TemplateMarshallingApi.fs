namespace Axle.Application.Forest.Resources.Marshalling

open Forest.Templates.Raw

open Axle.Resources
open Axle.Resources.Extraction


type [<Interface>] IForestTemplateMarshaller =
    abstract member ResolveResourceName: templateName:string -> string
    abstract member Unmarshal: name:string -> ResourceInfo -> Template option
    abstract ChainedExtractors: IResourceExtractor seq with get

type [<Interface>] IForestTemplateMarshallerRegistry =
    abstract member Register: marshaller:IForestTemplateMarshaller -> IForestTemplateMarshallerRegistry
    abstract Marshallers:IForestTemplateMarshaller seq with get

