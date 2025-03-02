module F_sharp_practice.Lesson6

//STRINGS

// 17.1
let rec pow  = function
    | (s, 0) -> ""
    | (s, n) -> s + pow(s, n-1)

// 17.2
let rec isIthChar (s,n,c) = n >= 0 && n < String.length s && s.[n] = c

// 17.3
let rec occFromIth  = function
    | ("",_,_) -> 0
    | (s,n,_) when n < 0 || n >= String.length s -> 0
    | (s,n,c) ->
        if s.[n] = c then
            1 + occFromIth(s, n+1, c)
        else
            occFromIth(s, n+1, c)