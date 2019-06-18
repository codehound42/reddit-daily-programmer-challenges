(*
[2019-05-20] Challenge #378 [Easy] The Havel-Hakimi algorithm for graph realization
*)

let rec HavelHakimi inputSeq =
    let inputSeqWithoutZeros = inputSeq |> Seq.filter (fun x -> x <> 0)
    if Seq.isEmpty inputSeqWithoutZeros then
        true
    else
        let inputSeqWithoutZerosSorted = inputSeqWithoutZeros |> Seq.sortDescending
        let N = inputSeqWithoutZerosSorted |> Seq.item 0
        let inputSeqWithoutZerosSortedFirstElementRemoved = inputSeqWithoutZerosSorted |> Seq.skip 1
        let seqLength = inputSeqWithoutZerosSortedFirstElementRemoved |> Seq.length
        if N > seqLength then
            false
        else
            let firstSeqPart = inputSeqWithoutZerosSortedFirstElementRemoved
                               |> Seq.map (fun x -> x-1)
                               |> Seq.take N
            let lastSeqPart = inputSeqWithoutZerosSortedFirstElementRemoved |> Seq.skip N
            firstSeqPart 
            |> Seq.append lastSeqPart
            |> HavelHakimi


HavelHakimi (seq [5; 3; 0; 2; 6; 2; 0; 7; 2; 5]) // false
HavelHakimi (seq [4; 2; 0; 1; 5; 0]) // false
HavelHakimi (seq [3; 1; 2; 3; 1; 0]) // true
HavelHakimi (seq [16; 9; 9; 15; 9; 7; 9; 11; 17; 11; 4; 9; 12; 14; 14; 12; 17; 0; 3; 16]) // true
HavelHakimi (seq [14; 10; 17; 13; 4; 8; 6; 7; 13; 13; 17; 18; 8; 17; 2; 14; 6; 4; 7; 12]) // true
HavelHakimi (seq [15; 18; 6; 13; 12; 4; 4; 14; 1; 6; 18; 2; 6; 16; 0; 9; 10; 7; 12; 3]) // false
HavelHakimi (seq [6; 0; 10; 10; 10; 5; 8; 3; 0; 14; 16; 2; 13; 1; 2; 13; 6; 15; 5; 1]) // false
HavelHakimi (seq [2; 2; 0]) // false
HavelHakimi (seq [3; 2; 1]) // false
HavelHakimi (seq [1; 1]) // true
HavelHakimi (seq [1]) // false
HavelHakimi (seq []) // true
