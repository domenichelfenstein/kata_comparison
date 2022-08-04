namespace FSharp.ProductExport

open System.Text

module Exporter =
    let exportTaxDetails (orders: Order list) =
        let ordersXml =
            let toOrderXml order =
                let toIso = FSharp.Dependency.ProductExport.Util.ToIsoDate
                let productsXml =
                    order.Products
                    |> List.map (fun p -> $"<product  id='{p.Id}'>{p.Name}</product>")
                    |> String.concat ""
                let orderTax = $"<orderTax currency='USD'>%0.2f{(TaxCalculator.calculate order)}%%</orderTax>"
                $"<order date='{toIso order.DateTime}'>{productsXml}{orderTax}</order>"
            
            orders
            |> List.map toOrderXml
            |> String.concat ""
            
        let totalTax = orders |> List.sumBy TaxCalculator.calculate
        let full = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?><orderTax>{ordersXml}%0.2f{totalTax}%%</orderTax>"
        FSharp.Dependency.ProductExport.XmlFormatter.PrettyPrint full
