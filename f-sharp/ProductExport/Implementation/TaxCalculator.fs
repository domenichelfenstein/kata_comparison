namespace FSharp.ProductExport

module TaxCalculator =
    let calculate (order: Order) =
        let mutable tax : double = 0.0
        if order.DateTime < FSharp.Dependency.ProductExport.Util.FromIsoDate("2018-01-01T00:00Z") then
            tax <- tax + 10.0
        else
            tax <- tax + 20.0
            
        for product in order.Products do
            if product.IsEvent () then
                tax <- tax + (product.Price |> Price.getAmountInCurrency "USD") * 0.25
            else
                tax <- tax + (product.Price |> Price.getAmountInCurrency "USD") * 0.175
                    
        tax
