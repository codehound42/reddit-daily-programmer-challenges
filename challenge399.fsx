let letterToValue = [ 'a' .. 'z' ] |> List.mapi (fun i x -> (x, i+1)) |> Map.ofList

let letterSum (input: string) =
    input
    |> Seq.map (fun c -> Map.find c letterToValue)
    |> Seq.sum

letterSum "" |> printfn "%i" // 0
letterSum "a" |> printfn "%i" // 1
letterSum "z" |> printfn "%i" // 26
letterSum "cab" |> printfn "%i" // 6
letterSum "excellent" |> printfn "%i" // 100
letterSum "microspectrophotometries" |> printfn "%i" // 317