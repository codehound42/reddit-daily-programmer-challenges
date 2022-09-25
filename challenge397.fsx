// Challenge397
// Note: Returned value format differs from original problem formulation
// Comparison implemented as operator instead of true/false result

let numeralToValue = Map.ofList [('I', 1); ('V', 5); ('X', 10); ('L', 50); ('C', 100); ('D', 500); ('M', 1000); ('-', 0)]

let combine xs ys = 
    let xsLength = List.length xs
    let ysLength = List.length ys
    if (xsLength > ysLength) then
        (xs, List.append ys [for _ in 1 .. (xsLength - ysLength) -> '-'])
    elif (xsLength < ysLength) then
        (List.append xs [for _ in 1 .. (ysLength - xsLength) -> '-'], ys)
    else
        (xs, ys)

let rec numcompareAux combined =
    match combined with
    | [] -> 0
    | (x, y)::tail ->
        try
            let xVal = Map.find x numeralToValue
            let yVal = Map.find y numeralToValue

            if (xVal > yVal) then
                1
            elif xVal < yVal then
                -1
            else
                numcompareAux tail
        with
            | :? System.Collections.Generic.KeyNotFoundException -> failwith "Invalid char value"

let numcompare (input1: string) (input2: string) =
    let xs = Seq.toList input1
    let ys = Seq.toList input2
    let combined = combine xs ys
    
    let result = List.zip (fst combined) (snd combined) |> numcompareAux
    printfn "%A" result
    result

numcompare "I" "I"  // false
numcompare "I" "II"  // true
numcompare "II" "I"  // false
numcompare "V" "IIII"  // false
numcompare "MDCLXV" "MDCLXVI"  // true
numcompare "MM" "MDCCCCLXXXXVIIII"  // false