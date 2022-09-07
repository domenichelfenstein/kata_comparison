module FSharp.StringCalculator

type ParsePart =
    | Digit of int
    | Operator of Operation

and Expression =
    | Number of int
    | Operation of Operation * Expression * Expression
    | Inconclusive of int * Operation
    | Empty

and Operation =
    | Plus
    | Minus

let parseChar c =
    match c with
     | s when s >= '0' && s <= '9' -> Digit (int (string c))
     | '+' -> Operator Plus
     | '-' -> Operator Minus
        | _ -> failwith "Invalid character"
     
let rec evaluate expression =
    match expression with
    | Number n -> n
    | Operation (Plus, left, right) -> evaluate left + evaluate right
    | Operation (Minus, left, right) -> evaluate left - evaluate right
    | Empty -> 0
    | Inconclusive _ -> failwith "Inconclusive expression"
    
let rec toTree (expression: Expression) currentPart = 
    match expression, currentPart with
     | Empty, Digit d -> Number d
     | Empty, Operator o -> Inconclusive (0, o)
     | Number n, Digit d -> Number (n * 10 + d)
     | Number n, Operator o -> Inconclusive (n, o)
     | Inconclusive (n, o), Digit d -> Operation (o, Number n, Number d)
     | Operation (o, left, right), Digit d -> Operation (o, left, (toTree right (Digit d)))
     | _ -> failwith "Invalid expression"

let calculate (expression: string) =
    expression
        |> Seq.map parseChar
        |> Seq.fold toTree Empty
        |> evaluate