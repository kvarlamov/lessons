module F_sharp_practice.Lesson5

//PREDICATES
// 16.1
let notDivisible  (n,m) = m % n = 0

// 16.2
let  prime = function
    | n when n < 2 -> false
    | n ->
        let rec check d =
            match d with
            | d when d = n -> true
            | _ when notDivisible(d, n) -> false
            | _ -> check (d + 1)
        check 2