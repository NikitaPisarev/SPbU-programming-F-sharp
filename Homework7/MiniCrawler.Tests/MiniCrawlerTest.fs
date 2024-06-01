module MiniCrawlerTests

open NUnit.Framework
open Moq
open System.Net
open System.Net.Http
open System.Threading
open FsUnit
open MiniCrawler

[<Test>]
let ``fetchSizes test`` () : unit =
    let client = Mock<HttpClient>()
    let url = "https://test.com/"
    let mainUrl = "https://fake.com/"

    let response = new HttpResponseMessage(HttpStatusCode.OK)

    client
        .Setup(fun client -> client.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(response)
        .Callback(fun (req: HttpRequestMessage) (ct: CancellationToken) ->
            if (req).RequestUri.AbsoluteUri = url then
                response.Content <- new StringContent("Test")
            else
                response.Content <- new StringContent($"<html><body><a href=\"{url}\">Example</a></body></html>"))
    |> ignore

    let expected = [| (url, 4) |]

    let actual = Async.RunSynchronously(fetchSizes client.Object mainUrl)

    Assert.AreEqual(expected, actual)
