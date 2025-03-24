module F_sharp_practice.Lesson18_imp

// 47.4.1
let f n =
    let mutable result = 1
    let mutable i = 1

    while i <= n do
        result <- result * i
        i <- i + 1

    result
        

// 47.4.2
let fibo n =
    if n < 0 then
        failwith "error"
    elif n = 0 then
        0
    elif n = 1 then
        1
    else
        let mutable a = 0
        let mutable b = 1
        let mutable i = 2
        while i <= n do
            let temp = a + b
            a <- b 
            b <- temp 
            i <- i + 1
        b 

