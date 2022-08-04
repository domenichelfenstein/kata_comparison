namespace FSharp.ProductExport

module TaxCalculator =
    let calculate (order: Order) =
        let thresholdDate = FSharp.Dependency.ProductExport.Util.FromIsoDate "2018-01-01T00:00Z"
        
        (if order.DateTime < thresholdDate then 10.0 else 20.0) +
            (order.Products
                |> List.map (fun p -> (if p.IsEvent() then 0.25 else 0.175) * (p.Price |> Price.getAmountInCurrency "USD"))
                |> List.sum)
