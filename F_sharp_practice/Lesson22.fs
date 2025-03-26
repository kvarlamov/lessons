module F_sharp_practice.Lesson22

type 'a cell = Nil | Cons of 'a * Lazy<'a cell>

let hd (s : 'a cell) : 'a =
  match s with
    Nil -> failwith "hd"
  | Cons (x, _) -> x

let tl (s : 'a cell) : Lazy<'a cell> =
  match s with
    Nil -> failwith "tl"
  | Cons (_, g) -> g

// 51.3
let rec nth (s : 'a cell) (n : int) : 'a =
  match n with
  | 0 -> hd s
  | _ -> nth (tl(s).Force()) (n - 1)