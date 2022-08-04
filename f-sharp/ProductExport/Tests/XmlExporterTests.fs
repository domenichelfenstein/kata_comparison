[<VerifyXunit.UsesVerify>]
module ``Product Export``

open System
open System.Text
open System.Text.RegularExpressions
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

[<Fact>]
let ``store`` () =
    let xml = XmlExporter.ExportStore SampleObjects.flagshipStore
    verifyXml xml defaultSettings

[<Fact>]
let ``history`` () =
    let xml = XmlExporter.ExportHistory ([ SampleObjects.recentOrder ; SampleObjects.oldOrder ] |> toCSList)
    let settings = defaultSettings
    let scrubber (input: StringBuilder) =
        let regex = "createdAt=\"[^\"]+\""
        let replacement = "createdAt=\"2018-09-20T00:00Z\""
        let scrubbed = Regex.Replace (input.ToString(), regex, replacement)
        input.Clear().Append(scrubbed) |> ignore
    settings.AddScrubber scrubber
    verifyXml xml settings

[<Fact>]
let ``full`` () =
    let xml = XmlExporter.ExportFull ([ SampleObjects.recentOrder ; SampleObjects.oldOrder ] |> toCSList)
    verifyXml xml defaultSettings
