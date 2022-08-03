module ``Fizz Buzz``
    open Xunit
    open FsUnit.Xunit
    open FSharp.FizzBuzz
    open Swensen.Unquote

    [<Fact>]
    let ``returns list of numbers; when no conditions are met`` () =
        test <@ fizzBuzz 1 2 = [ "1" ; "2" ] @>
        
    [<Fact>]
    let ``returns Fizz on three`` () =
        test <@ fizzBuzz 3 3 = [ "Fizz" ] @>
        
    [<Fact>]
    let ``returns Fizz on dividable by three`` () =
        test <@ fizzBuzz 6 6 = [ "Fizz" ] @>
        
    [<Theory>]
    [<InlineData(5)>]
    [<InlineData(10)>]
    [<InlineData(100)>]
    let ``returns Buzz on dividable by five`` number =
        test <@ fizzBuzz number number = [ "Buzz" ] @>
        
    [<Theory>]
    [<InlineData(15)>]
    [<InlineData(30)>]
    [<InlineData(60)>]
    let ``returns FizzBuzz if dividable by three and five`` number =
        test <@ fizzBuzz number number = [ "FizzBuzz" ] @>
        
    [<Fact>]
    let ``returns number if dividable by seven`` () =
        test <@ fizzBuzz 1 105 = [ "1"; "2"; "Fizz"; "4"; "Buzz"; "Fizz"; "7"; "8"; "Fizz"; "Buzz"; "11"; "Fizz"; "13"; "14"; "FizzBuzz"; "16"; "17"; "Fizz"; "19"; "Buzz"; "21"; "22"; "23"; "Fizz"; "Buzz"; "26"; "Fizz"; "28"; "29"; "FizzBuzz"; "31"; "32"; "Fizz"; "34"; "35"; "Fizz"; "37"; "38"; "Fizz"; "Buzz"; "41"; "42"; "43"; "44"; "FizzBuzz"; "46"; "47"; "Fizz"; "49"; "Buzz"; "Fizz"; "52"; "53"; "Fizz"; "Buzz"; "56"; "Fizz"; "58"; "59"; "FizzBuzz"; "61"; "62"; "63"; "64"; "Buzz"; "Fizz"; "67"; "68"; "Fizz"; "70"; "71"; "Fizz"; "73"; "74"; "FizzBuzz"; "76"; "77"; "Fizz"; "79"; "Buzz"; "Fizz"; "82"; "83"; "84"; "Buzz"; "86"; "Fizz"; "88"; "89"; "FizzBuzz"; "91"; "92"; "Fizz"; "94"; "Buzz"; "Fizz"; "97"; "98"; "Fizz"; "Buzz"; "101"; "Fizz"; "103"; "104"; "105" ] @>