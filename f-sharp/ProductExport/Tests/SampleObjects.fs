namespace FSharp.ProductExport.Tests

module DependencySampleObjects =
    open FSharp.Dependency.ProductExport

    let cherryBloom =
        Product ("Cherry Bloom", "LIPSTICK01", 30, Price (14.99, "USD"))

    let flagshipStore =
        Store ("Nordstan", "4189", [| cherryBloom |])

    let makeover =
        StoreEvent ("Makeover", "EVENT02", flagshipStore, Price (149.99, "USD"))


    let recentOrder =
        Order ("1234", Util.FromIsoDate ("2018-09-01T00:00Z"), flagshipStore, [| makeover |])

    let oldOrder =
        Order ("1235", Util.FromIsoDate ("2017-09-01T00:00Z"), flagshipStore, [| cherryBloom |])


module SampleObjects =
    open FSharp.ProductExport

    let cherryBloom =
        Product (
            "Cherry Bloom",
            "LIPSTICK01",
            30,
            { Price.Amount = 14.99
              CurrencyCode = "USD" }
        )

    let flagshipStore =
        { Store.Name = "Nordstan"
          Id = "4189"
          Products = [ cherryBloom ] }

    let makeover =
        StoreEvent (
            "Makeover",
            "EVENT02",
            { Price.Amount = 149.99
              CurrencyCode = "USD" }
        )


    let recentOrder =
        { Order.Id = "1234"
          DateTime = FSharp.Dependency.ProductExport.Util.FromIsoDate ("2018-09-01T00:00Z")
          Store = flagshipStore
          Products = [ makeover ] }

    let oldOrder =
        { Order.Id = "1235"
          DateTime = FSharp.Dependency.ProductExport.Util.FromIsoDate ("2017-09-01T00:00Z")
          Store = flagshipStore
          Products = [ cherryBloom ] }
