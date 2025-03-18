module F_sharp_practice.Lesson15_HOF

// 41.4.1
let list_filter f xs =
    List.foldBack (fun x acc -> if f x then x :: acc else acc) xs []


// 41.4.2
let sum (p, xs) =
    List.fold (fun acc x -> if p x then x + acc else acc) 0 xs

// 41.4.3
let revrev = function
    | mainList ->
        let innerFold lst = List.fold (fun head tail -> tail :: head) [] lst
        List.fold (fun acc lst -> (innerFold lst) :: acc) [] mainList
