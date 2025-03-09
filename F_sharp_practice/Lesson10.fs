module F_sharp_practice.Lesson10

// Время дня может быть представлено тройкой (часы, минуты, половина дня),
// где часы принимают значение в диапазоне от 0 до 11, минуты -- от 0 до 59,
// и половина дня -- это либо "AM" (первые 12 часов), либо "PM" (вторые 12 часов).
// Реализуйте инфиксный оператор .>. , который сравнивает два корректных значения типа TimeOfDay.

exception TimeOfDayExc


type TimeOfDay = { hours: int; minutes: int; f: string }

let validateTime { hours = h; minutes = m; f = f } =
    if (h < 0 || h > 11) then
        failwith "Время должно быть в диапазоне от 0 до 11"
    elif (m < 0 || m > 59) then
        failwith "Минуты должны быть в диапазоне от 0 до 59"
    elif f <> "AM" && f <> "PM" then
        failwith "половина дня -- это либо AM (первые 12 часов), либо PM (вторые 12 часов)"
    else
        ()

let (.>.) x y =
    validateTime x
    validateTime y
    let { hours = h1; minutes = m1; f = f1 } = x
    let { hours = h2; minutes = m2; f = f2 } = y

    match f1, f2 with
    | "PM", "AM" -> true
    | "AM", "PM" -> false
    | _, _ ->
        if h1 > h2 then true
        elif h1 < h2 then false
        else m1 > m2
