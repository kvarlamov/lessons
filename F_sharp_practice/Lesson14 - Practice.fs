module F_sharp_practice.Lesson14___Practice

// 40.1
let rec sum (p, xs) =
    match xs with
    | [] -> 0
    | head :: tail -> if p head then head + sum (p, tail) else sum (p, tail)

// 40.2.1
let rec count (xs, n) =
    match xs with
    | [] -> 0
    | head :: tail when head < n -> count (tail, n)
    | head :: _ when head > n -> 0
    | head :: tail -> 1 + count (tail, n)

// 40.2.2
let rec insert (xs, n) =
    match xs with
    | [] -> [ n ]
    | head :: tail when head >= n -> n :: xs
    | head :: tail -> head :: insert (tail, n)

// 40.2.3
let rec intersect (xs1, xs2) =
    match (xs1, xs2) with
    | [], _ -> []
    | _, [] -> []
    | head1 :: tail1, head2 :: tail2 ->
        if head1 = head2 then head1 :: intersect (tail1, tail2)
        elif head1 < head2 then intersect (tail1, xs2)
        else intersect (xs1, tail2)

// 40.2.4
let rec plus (xs1, xs2) =
    match (xs1, xs2) with
    | [], ls2 -> ls2
    | ls1, [] -> ls1
    | head1 :: tail1, head2 :: tail2 ->
        if head1 < head2 then head1 :: plus (tail1, xs2)
        elif head1 > head2 then head2 :: plus (xs1, tail2)
        else head1 :: head2 :: plus (tail1, tail2)

// 40.2.5
let rec minus (xs1, xs2) =
    match (xs1, xs2) with
    | [], _ -> []
    | ls1, [] -> ls1
    | head1 :: tail1, head2 :: tail2 ->
        if head1 < head2 then head1 :: minus (tail1, xs2)
        elif head1 > head2 then minus (xs1, tail2)
        else minus (tail1, tail2)

// 40.3.1
let rec smallest =
    function
    | [] -> None
    | xs -> Some(List.min xs)

// 40.3.2
let rec delete (n, xs) =
    match xs with
    | [] -> []
    | head :: tail -> if head = n then tail else head :: delete (n, tail)

// 40.3.3
let rec sort lst =
    match lst with
    | [] -> []
    | _ ->
        match smallest lst with
        | None -> []
        | Some minElem -> minElem :: sort (delete (minElem, lst))

// 40.4
let rec revrev =
    function
    | [] -> []
    | head :: tail ->
        let inner = List.rev head
        let outer = revrev tail
        outer @ [ inner ]
