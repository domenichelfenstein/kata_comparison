module FSharp.ShoppingBasket

type Product = { Id : string ; Name : string ; Price : decimal }
    
module Basket =
    let totalCost products basket =
        let discounts =
            let fivePercent total = if total > 100m then Some (total * 0.05m) else None
            let tenPercent total = if total > 200m then Some (total * 0.10m) else None
            [ fivePercent; tenPercent ]
        
        let applyDiscount total =
            discounts
            |> List.tryFindBack (fun f -> f total |> Option.isSome)
            |> Option.bind (fun f -> f total)
            |> Option.map (fun discount -> total - discount)
            |> Option.defaultValue total
        
        let roundToTwoDecimals (total: decimal) = System.Math.Round(total, 2)
        
        basket
        |> List.map (fun item -> products |> List.find (fun p -> p.Id = item))
        |> List.sumBy (fun product -> product.Price)
        |> applyDiscount
        |> roundToTwoDecimals