module F_sharp_practice.Lesson16_set

// 42.3
let rec allSubsets n k =
    if k = 0 then [[]]
    else if  k>n then []
    else
        let subsetIncludeN = allSubsets (n-1) (k-1)
        let subsetWithN = List.map (fun subset -> n :: subset) subsetIncludeN
        let subsetWithoutN = allSubsets (n-1) k
        
        subsetWithN @ subsetWithoutN