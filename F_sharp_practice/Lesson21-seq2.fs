module F_sharp_practice.Lesson21_seq2

// 50.2.1
let fac_seq =
    let rec fact = function
        | 0
        | 1 -> 1
        | x -> x * fact (x - 1)

    seq { yield! Seq.initInfinite fact }


// 50.2.2
let seq_seq =
    seq {
        yield!
            Seq.initInfinite (fun n ->
                if n = 0 then 0
                elif n % 2 = 1 then -(n + 1) / 2
                else n / 2)
    }
