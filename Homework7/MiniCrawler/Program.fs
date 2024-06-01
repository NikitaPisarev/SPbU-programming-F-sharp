module MiniCrawler

open System.Net.Http
open System.Text.RegularExpressions

let extractLinks (html: string) =
    let regex = Regex(@"<a\s+(?:[^>]*?\s+)?href=([""'])(http[^""']+)\1")

    seq {
        for mtch in regex.Matches(html) do
            yield mtch.Groups.[2].Value
    }

let fetchHtml (url: string) =
    async {
        try
            use client = new HttpClient()
            let! html = client.GetStringAsync(url) |> Async.AwaitTask
            return html
        with ex ->
            printfn "Failed to download %s. Exception: %s" url ex.Message
            return ""
    }


let fetchAndPrintSizes (url: string) =
    async {
        let! mainHtml = fetchHtml url
        let links = extractLinks mainHtml

        let fetchTasks =
            links
            |> Seq.map (fun link ->
                async {
                    let! html = fetchHtml link
                    return (link, html.Length)
                })

        let! results = fetchTasks |> Async.Parallel

        results
        |> Array.iter (fun (link, size) -> printfn "%s — %d characters." link size)
    }

[<EntryPoint>]
let main argv =
    if argv.Length > 0 then
        Async.RunSynchronously(fetchAndPrintSizes argv.[0])
    else
        printfn "Missing URL."

    0
