[<VerifyXunit.UsesVerify>]
module ``Product Export``

open System
open System.Xml
open FSharp.Dependency.ProductExport
open FSharp.ProductExport.Tests
open VerifyTests
open VerifyXunit
open Xunit

let defaultSettings = VerifySettings()
defaultSettings.UseDirectory "./Data"
defaultSettings.UseExtension "xml"

let verifyXml xml settings =
    task {
        let xmlDoc = XmlDocument()
        xmlDoc.LoadXml xml
        return! Verifier.Verify (xmlDoc, settings)
    }

let toCSList (input : 'a list) =
    ResizeArray<'a> input

[<Fact>]
let ``tax details`` () =
    let xml = XmlExporter.ExportTaxDetails ([ SampleObjects.recentOrder ; SampleObjects.oldOrder ] |> toCSList)
    verifyXml xml defaultSettings
