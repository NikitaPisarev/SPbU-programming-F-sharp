module PhoneBook

type PhoneBookEntry = { Name: string; Phone: string }
type PhoneBook = Map<string, PhoneBookEntry>

type Result<'T> =
    | Found of 'T
    | NotFound of string

let addEntry name phone (phoneBook: PhoneBook) =
    Map.add name { Name = name; Phone = phone } phoneBook

let findPhoneByName name (phoneBook: PhoneBook) =
    match Map.tryFind name phoneBook with
    | Some(entry) -> Found entry.Phone
    | None -> NotFound "Entry not found."

let findNameByPhone phone (phoneBook: PhoneBook) =
    phoneBook
    |> Map.toList
    |> List.tryFind (fun (_, entry) -> entry.Phone = phone)
    |> function
        | Some(name, _) -> Found name
        | None -> NotFound "Entry not found."

let listAllEntries (phoneBook: PhoneBook) =
    phoneBook |> Map.toList |> List.map (fun (name, entry) -> (name, entry.Phone))

let saveToFile filename (phoneBook: PhoneBook) =
    let lines =
        phoneBook
        |> Map.toList
        |> List.map (fun (_, entry) -> sprintf "%s, %s" entry.Name entry.Phone)

    System.IO.File.WriteAllLines(filename, lines)
    printfn "Data saved to %s." filename

let loadFromFile filename =
    let lines = System.IO.File.ReadAllLines(filename)

    let phoneBook =
        lines
        |> Array.fold
            (fun acc line ->
                let parts = line.Split(", ")

                if parts.Length = 2 then
                    Map.add parts.[0] { Name = parts.[0]; Phone = parts.[1] } acc
                else
                    acc)
            Map.empty

    printfn "Data loaded from %s." filename
    phoneBook
