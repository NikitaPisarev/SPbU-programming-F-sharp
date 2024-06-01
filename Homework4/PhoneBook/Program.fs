module PhoneBookInterface

open PhoneBook

let printCommands () =
    System.Console.WriteLine(
        """
    Hi, I'm a phone book, that's what I can do:
      Add [Name] [Number]: Add contact
      FindByPhone [Phone]: Find phone number by name
      FindByName [Name]: Find name by phone number
      ListAll: List of all contacts
      SaveToFile [FilePath]: Save phonebook in file
      LoadFromFile [FilePath]: Load phonebook from file
      Exit: Exit
    """
    )

let rec mainLoop book =
    printf "> "
    let input = System.Console.ReadLine().ToLower().Split()

    match input.[0] with
    | "add"
    | "a" when input.Length > 2 -> mainLoop (addEntry input.[1] input.[2] book)
    | "findbyname"
    | "fbn" when input.Length > 1 ->
        let result = findPhoneByName input.[1] book
        printfn "%s" result
        mainLoop book
    | "findbyphone"
    | "fbp" when input.Length > 1 ->
        let result = findNameByPhone input.[1] book
        printfn "%s" result
        mainLoop book
    | "listall"
    | "la" ->
        listAllEntries book
        |> List.iter (fun (name, phone) -> printfn "%s: %s" name phone)

        mainLoop book
    | "savetofile"
    | "stf" when input.Length > 1 ->
        saveToFile input.[1] book
        mainLoop book
    | "loadfromfile"
    | "lff" when input.Length > 1 ->
        let newBook = loadFromFile input.[1]
        mainLoop newBook
    | "exit" -> ()
    | _ ->
        printfn "Invalid command."
        mainLoop book

[<EntryPoint>]
let main argv =
    printCommands ()
    mainLoop Map.empty
    0
