module F_sharp_practice.Lesson20

// 49.5.1
let even_seq = Seq.initInfinite (fun i -> 2 * (i + 1))

// 49.5.2
let fac_seq =
    let rec fact = function
        | 0 | 1 -> 1
        | x -> x * fact (x - 1)

    Seq.initInfinite fact

// 49.5.3
let seq_seq = Seq.initInfinite (fun n ->
    if n = 0 then 0
    elif n % 2 = 1 then -(n + 1)/2
    else n /2 )
    