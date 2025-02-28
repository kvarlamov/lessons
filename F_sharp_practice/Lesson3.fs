module F_sharp_practice.Lesson3

let days_in_month = function
    | 1|5|7|8|10|12 -> 31
    | 2 -> 28
    | 3|4|6|9|11 -> 30
    | _ -> 0