module F_sharp_practice.Lesson12

// 1.Напишите функцию upto: int -> int list, которая работает так:
//
// upto n = [1; 2; ...; n].
// 2.Напишите функцию dnto: int -> int list, которая работает так:
//
// downto n = [n; n-1; n-2; ...; 1].
// 3. Напишите функцию evenn: int -> int list, которая генерирует список из первых n неотрицательных чётных чисел.



// 34.1
let rec upto n =
    if
        n <= 0 then []
    else
        upto (n - 1) @ [n]
    
// 34.2
let rec dnto n =
    if
        n <= 0 then []
    else
        n :: dnto(n - 1)

// 34.3
let rec evenn n =
    if
        n < 0 then []
    else
        evenn(n - 1) @ [2 * (n - 1)]
        