module F_sharp_practice.Lesson16_set

// 42.3
let rec allSubsets n k =
    if k = 0 then
        Set.ofList [ Set.empty ]
    else if k > n then
        Set.empty
    else
        let subsetIncludeN = allSubsets (n - 1) (k - 1)
        let subsetWithN = Set.map (fun subset -> Set.add n subset) subsetIncludeN
        let subsetWithoutN = allSubsets (n - 1) k

        Set.union subsetWithN subsetWithoutN