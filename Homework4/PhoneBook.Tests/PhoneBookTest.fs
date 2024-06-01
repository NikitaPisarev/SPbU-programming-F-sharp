module PhoneBook.Tests

open NUnit.Framework
open FsUnit
open PhoneBook

[<Test>]
let ``Add entry to PhoneBook should work`` () =
    let initialBook = Map.empty
    let updatedBook = addEntry "Alice" "123" initialBook
    let result = Map.tryFind "Alice" updatedBook
    result |> should equal (Some { Name = "Alice"; Phone = "123" })

[<Test>]
let ``Find phone by name should return correct phone number`` () =
    let phoneBook = Map.empty |> addEntry "Bob" "424214"
    let result = findPhoneByName "Bob" phoneBook

    match result with
    | Found phone -> phone |> should equal "424214"
    | NotFound _ -> Assert.Fail("Phone not found.")

[<Test>]
let ``Find name by phone should return correct name`` () =
    let phoneBook = Map.empty |> addEntry "Bob" "424214"
    let result = findNameByPhone "424214" phoneBook

    match result with
    | Found name -> name |> should equal "Bob"
    | NotFound _ -> Assert.Fail("Name not found.")

[<Test>]
let ``List all entries correctly`` () =
    let phoneBook =
        Map.empty |> addEntry "Alice" "1" |> addEntry "Bob" "2" |> addEntry "Carol" "3"

    let expectedEntries = [ ("Alice", "1"); ("Bob", "2"); ("Carol", "3") ]

    let actualEntries = listAllEntries phoneBook
    actualEntries |> should equal expectedEntries

[<Test>]
let ``Load from file should correctly populate the phone book`` () =
    let expectedPhoneBook =
        Map.empty
        |> Map.add "Alice" { Name = "Alice"; Phone = "1" }
        |> Map.add "Bob" { Name = "Bob"; Phone = "2" }

    let actualPhoneBook = PhoneBook.loadFromFile "../../../test.txt"
    actualPhoneBook |> should equal expectedPhoneBook

[<Test>]
let ``Save to file should correctly write phone book entries to file`` () =
    let phoneBook =
        Map.empty
        |> Map.add "Carol" { Name = "Alice"; Phone = "3" }
        |> Map.add "Dave" { Name = "Bob"; Phone = "4" }

    saveToFile "output_test.txt" phoneBook

    let lines = System.IO.File.ReadAllLines("output_test.txt")
    let expectedLines = [| "Alice, 3"; "Bob, 4" |]
    lines |> should equal expectedLines
