module ``String Calculator``
open Xunit
open FsUnit.Xunit
open FSharp.StringCalculator
open Swensen.Unquote

[<Fact>]
let ``empty string returns zero`` () =
    let result = calculate ""
    result |> should equal 0

[<Theory>]
[<InlineData("1", 1)>]
[<InlineData("456", 456)>]
[<InlineData("-2", -2)>]
let ``single number returns the value`` (input, expected) =
    let result = calculate input
    result |> should equal expected

[<Theory>]
[<InlineData("1+1", 2)>]
[<InlineData("57+100", 157)>]
[<InlineData("1000+0", 1000)>]
let ``addition of two numbers returns sum`` (input, expected) =
    let result = calculate input
    result |> should equal expected