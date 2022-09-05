module ``Shopping Basket``

open Xunit
open FsUnit.Xunit
open FSharp.ShoppingBasket
open Swensen.Unquote

[<Fact>]
let ``returns 0 if basket is empty`` () =
    let basket = []
    let total = basket |> Basket.totalCost []
    total |> should equal 0m

[<Fact>]
let ``returns cost of product if basket contains single item`` () =
    let basket = [ "001" ]

    let product =
        { Id = "001"
          Name = "Apple"
          Price = 2.50m }

    let total =
        basket |> Basket.totalCost [ product ]

    total |> should equal 2.50m

[<Fact>]
let ``returns total if basket contains product twice`` () =
    let basket = [ "001"; "001" ]

    let product =
        { Id = "001"
          Name = "Apple"
          Price = 2.50m }

    let total =
        basket |> Basket.totalCost [ product ]

    total |> should equal 5m

[<Fact>]
let ``returns total if basket contains different products`` () =
    let basket = [ "001"; "002" ]

    let apple =
        { Id = "001"
          Name = "Apple"
          Price = 2.50m }

    let pear =
        { Id = "002"
          Name = "Pear"
          Price = 2.90m }

    let total =
        basket |> Basket.totalCost [ apple; pear ]

    total |> should equal 5.40m

[<Fact>]
let ``returns discounted total if total > $100`` () =
    let basket =
        [ "001"
          "001"
          "001"
          "001"
          "001"
          "002"
          "002"
          "003"
          "003"
          "003"
          "003"
          "003"
          "003" ]

    let a =
        { Id = "001"
          Name = "A"
          Price = 10m }

    let b =
        { Id = "002"
          Name = "B"
          Price = 25m }

    let c =
        { Id = "003"
          Name = "C"
          Price = 9.99m }

    let total =
        basket |> Basket.totalCost [ a; b ; c ]

    total |> should equal 151.94m
