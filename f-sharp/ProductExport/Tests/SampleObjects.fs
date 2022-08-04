module FSharp.ProductExport.Tests.SampleObjects
open FSharp.Dependency.ProductExport

let cherryBloom = Product("Cherry Bloom", "LIPSTICK01", 30, Price(14.99, "USD"))
let flagshipStore = Store("Nordstan", "4189", [| cherryBloom |])
let makeover = StoreEvent("Makeover", "EVENT02", flagshipStore, Price(149.99, "USD"))


let recentOrder = Order("1234", Util.FromIsoDate("2018-09-01T00:00Z"), flagshipStore, [| makeover |])
let oldOrder = Order("1235", Util.FromIsoDate("2017-09-01T00:00Z"), flagshipStore, [| cherryBloom |])