module MiniCrawler

open System.Net.Http
open System.Text.RegularExpressions

let extractLinks (html: string) =
    let regex = Regex(@"<a\s+(?:[^>]*?\s+)?href=([""'])(http[^""']+)\1")

    seq {
        for mtch in regex.Matches(html) do
            yield mtch.Groups.[2].Value
    }

let fetchHtml (client: HttpClient) (url: string) =
    async {
        try
            let! html = client.GetStringAsync(url) |> Async.AwaitTask
            return html
        with ex ->
            printfn "Failed to download %s. Exception: %s" url ex.Message
            return ""
    }

let fetchSizes (client: HttpClient) (url: string) =
    async {
        let! mainHtml = fetchHtml client url
        let links = extractLinks mainHtml

        let fetchTasks =
            links
            |> Seq.map (fun link ->
                async {
                    let! html = fetchHtml client link
                    return (link, html.Length)
                })

        let! results = fetchTasks |> Async.Parallel

        return results
    }

let printSizes (sizes: (string * int)[]) =
    sizes
    |> Array.iter (fun (link, size) -> printfn "%s — %d characters." link size)

let fetchAndPrintSizes (client: HttpClient) (url: string) =
    async {
        let! sizes = fetchSizes client url
        printSizes sizes
    }

[<EntryPoint>]
let main argv =
    if argv.Length > 0 then
        let client = new HttpClient()
        Async.RunSynchronously(fetchAndPrintSizes client argv.[0])
        0
    else
        printfn "Missing URL."
        1
