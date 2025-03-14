module F_sharp_practice.Lesson13___List2

// 1. Напишите функцию rmodd, которая получает на вход список, и выдаёт список, в который входят значения входного списка на нечётных позициях (первая позиция в списке, с индексом 0, считается чётной).
//
// 2. Напишите функцию del_even, которая получает на вход список, и выдаёт список, из которого удалены все чётные значения входного списка.
//
// 3. Напишите функцию multiplicity x xs, которая находит, сколько раз значение x встречается в списке xs.
//
// 4. Напишите функцию split, которая разделяет входной список на два следующим образом:
// split [x1; x2; ...; xn-1; xn] = ([x1; x3; ...], [x2; x4; ...])
//
// 39.5. Напишите функцию zip, которая преобразует два входных списка в результирующий список следующим образом:
// zip ([x1; x2; ...], [y1; y2; ...]) = [(x1,y1); (x2,y2); ...]
// Если длины входных списков неодинаковы, генерируйте исключение.

// 39.1
let rec rmodd = function
    | [] -> []
    | [_] -> []
    | _ :: next :: tail -> next :: rmodd tail 

// 39.2
let rec del_even = function
    | [] -> []
    | [_] -> []
    | head::tail ->
        if head % 2 <> 0 then head :: del_even tail
        else del_even tail
    
// 39.3
let rec multiplicity x xs = match xs with
    | [] -> 0
    | head::tail ->
        if head <> x then multiplicity x tail
        else 1+multiplicity x tail
    

// 39.4
let rec split = function
    | [] -> ([],[])
    | [head] -> ([head],[])
    | head :: next :: tail ->
        let (odd, even) = split tail
        (head :: odd, next :: even)
        


// 39.5
let rec zip (xs1,xs2) =
    if List.length xs1 <> List.length xs2 then raise (System.ArgumentException("length of lists xs1, xs2 is different"))
    match (xs1, xs2) with
    | ([],[]) -> []
    | (head1 :: tail1, head2::tail2) -> (head1, head2) :: zip (tail1, tail2)
    | _ -> failwith "error"