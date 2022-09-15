// TODO: Finish up

let numeralToValue = Map.ofList [('I', 1); ('V', 5); ('X', 10); ('L', 50); ('C', 100); ('D', 500); ('M', 1000)]

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
    let combined = List.zip xs ys
    let result = numcompareAux combined
    printfn "%i" result

numcompare "V" "I"