module FSharp.FizzBuzz

let fizzBuzz start limit : string list =
    let (|Fizz|Not|) input = if input % 3 = 0 then Fizz else Not
    let (|Buzz|Not|) input = if input % 5 = 0 then Buzz else Not
    let (|By7|NotBy7|) input = if input % 7 = 0 then By7 else NotBy7

    [ for i in start..limit do
          match i with
          | Fizz & Buzz & NotBy7 -> "FizzBuzz"
          | Fizz & NotBy7 -> "Fizz"
          | Buzz & NotBy7 -> "Buzz"
          | number -> number.ToString() ]
