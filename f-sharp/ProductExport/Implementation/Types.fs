namespace FSharp.ProductExport

open System

type Price = {
    Amount: double
    CurrencyCode: string
}


type IProduct =
    abstract member IsEvent : unit -> bool
    abstract member Price : Price
    abstract member Name : string
    abstract member Id : string
    
type Product(Name: string, Type: string, Weight: int, Price: Price) =
    interface IProduct with
        member this.IsEvent() = false
        member this.Price = Price
        member this.Name = Name
        member this.Id = Type

type StoreEvent(Name: string, Type: string, Price : Price) =
    interface IProduct with
        member this.IsEvent() = true
        member this.Price = Price
        member this.Name = Name
        member this.Id = Type

type Store = {
    Name: string
    Id: string
    Products: IProduct list
}

type Order = {
    Id: string
    DateTime: DateTime
    Store: Store
    Products: IProduct list
}

module Price =
    let getAmountInCurrency currencyCode price =
        if price.CurrencyCode = currencyCode then
            price.Amount
        else
            failwith "shouldn't call this from a unit test, it will do a slow db lookup"