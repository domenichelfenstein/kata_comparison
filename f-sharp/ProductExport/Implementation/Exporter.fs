namespace FSharp.ProductExport

open System.Text

module Exporter =
    let exportTaxDetails (orders: Order list) =
        let xml = StringBuilder ()

        xml.Append ("<?xml version=\"1.0\" encoding=\"UTF-8\"?>")
        |> ignore

        xml.Append ("<orderTax>") |> ignore

        for order in orders do
            xml.Append ("<order") |> ignore
            xml.Append (" date='") |> ignore
            xml.Append (FSharp.Dependency.ProductExport.Util.ToIsoDate (order.DateTime)) |> ignore
            xml.Append ("'") |> ignore
            xml.Append (">") |> ignore

            for product in order.Products do
                xml.Append ("<product") |> ignore
                xml.Append (" id='") |> ignore
                xml.Append (product.Id) |> ignore
                xml.Append ("'") |> ignore
                xml.Append (">") |> ignore
                xml.Append (product.Name) |> ignore
                xml.Append ("</product>") |> ignore

            xml.Append ("<orderTax currency='USD'>") |> ignore

            let tax = TaxCalculator.calculate order
            xml.Append ($"%0.2f{tax}%%") |> ignore
            xml.Append ("</orderTax>") |> ignore
            xml.Append ("</order>") |> ignore

        let totalTax = orders |> List.sumBy TaxCalculator.calculate

        xml.Append ($"%0.2f{totalTax}%%") |> ignore
        xml.Append ("</orderTax>") |> ignore
        FSharp.Dependency.ProductExport.XmlFormatter.PrettyPrint (xml.ToString ())
