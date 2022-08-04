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
            let mutable tax = 0.0

            for product in order.Products do
                xml.Append ("<product") |> ignore
                xml.Append (" id='") |> ignore
                xml.Append (product.Id) |> ignore
                xml.Append ("'") |> ignore
                xml.Append (">") |> ignore
                xml.Append (product.Name) |> ignore
                xml.Append ("</product>") |> ignore

                if product.IsEvent () then
                    tax <- tax + (product.Price |> Price.getAmountInCurrency "USD") * 0.25
                else
                    tax <- tax + (product.Price |> Price.getAmountInCurrency "USD") * 0.175

            xml.Append ("<orderTax currency='USD'>") |> ignore

            if order.DateTime < FSharp.Dependency.ProductExport.Util.FromIsoDate ("2018-01-01T00:00Z") then
                tax <- tax + 10.0
            else
                tax <- tax + 20.0

            xml.Append ($"%0.2f{tax}%%") |> ignore
            xml.Append ("</orderTax>") |> ignore
            xml.Append ("</order>") |> ignore

        let totalTax = TaxCalculator.calculate orders

        xml.Append ($"%0.2f{totalTax}%%") |> ignore
        xml.Append ("</orderTax>") |> ignore
        FSharp.Dependency.ProductExport.XmlFormatter.PrettyPrint (xml.ToString ())
