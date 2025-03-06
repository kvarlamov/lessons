module F_sharp_practice.Lesson9


//В фэнтези-РПГ принята следующая денежная система: в одном золотом 20 серебряных, а в одном серебряном 12 медяков.
//Суммы в такой системе задаются тройками целых чисел (золотые, серебряные, медяки), например (1, 0, 128) или (32, 23, 5).
//Реализуйте инфиксный оператор .+. , который складывает деньги, представленные в виде троек, и инфиксный оператор .-. ,
//который вычитает деньги. Результат приводите к формату, когда количество медяков не превышает 11, а количество серебряных не превышает 19.

let normalise(gold, silver, copper) =
    let extraSilver = copper / 12
    let newCopper = copper % 12
    let extraGold = (silver + extraSilver) / 20
    let newSilver = (silver + extraSilver) % 20
    let newGold = gold + extraGold
    (newGold, newSilver, newCopper)

// 23.4.1
let (.+.) x y =
    let g1, s1, c1 = x
    let g2, s2, c2 = y
    
    let gold = g1 + g2
    let silver = s1 + s2
    let copper = c1 + c2
    
    normalise(gold, silver, copper)

let (.-.) x y =
    let g1, s1, c1 = x
    let g2, s2, c2 = y
    
    let totalCopper = (g1 * 240 + s1 * 12 + c1) - (g2 * 240 + s2 * 12 + c2)
    
    let finalGold = totalCopper / 240
    let finalSilver = (totalCopper % 240) / 12
    let finalCopper = totalCopper % 12
    
    normalise (finalGold, finalSilver, finalCopper)

// Реализуйте логику работы с комплексными числами. Комплексное число задаётся парой вещественных чисел (x,y).
// Правила сложения и умножения:
// (a, b) + (c, d) = (a + c, b + d)
// (a, b) * (c, d) = (ac - bd, bc + ad)
// Вычитание реализуется сложением через инверсию:
// -(a, b) = (-a,-b),
// Деление реализуется умножением через инверсию:
// 1/(a, b) = (a/(a^2+b^2),-b/(a^2+b^2))
// Реализуйте соответствующие инфиксные операторы .+ , .- , .* и ./ .
// 23.4.2
let (.+) x y =
    let a,b = x
    let c,d = y
    
    (a+c, b+d)
let (.-) x y =
    let a,b = x
    let c,d = y
    
    (a-c, b-d)
let (.*) x y =
    let a,b = x
    let c,d = y
    
    (a*c - b*d, b*c + a*d)
let (./) x y =
    let a,b = x
    let c,d = y
    let denominator = c * c + d * d
    ((a * c + b * d) / denominator, (b * c - a * d) / denominator)

