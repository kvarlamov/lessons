module F_sharp_practice.Lesson11

type F = 
  | AM
  | PM

type TimeOfDay = { hours : int; minutes : int; f: F }

let transform (x: TimeOfDay) =
    match x.f with
    | AM -> x.hours * 60 + x.minutes
    | PM -> (x.hours + 12) * 60 + x.minutes

let (.>.) x y = (transform x) > (transform y)