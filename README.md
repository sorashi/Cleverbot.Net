Cleverbot.Net
===
[![NuGet](https://img.shields.io/nuget/v/Cleverbot.Net.svg?maxAge=2592000)](https://www.nuget.org/packages/Cleverbot.Net/)
[![GitHub issues](https://img.shields.io/github/issues/Sorashi/Cleverbot.Net.svg)](https://github.com/Sorashi/Cleverbot.Net/issues)
[![GitHub stars](https://img.shields.io/github/stars/Sorashi/Cleverbot.Net.svg)](https://github.com/Sorashi/Cleverbot.Net/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/Sorashi/Cleverbot.Net.svg)](https://github.com/Sorashi/Cleverbot.Net/network)
[![Twitter](https://img.shields.io/twitter/url/https/github.com/Sorashi/Cleverbot.Net.svg?style=social)](https://twitter.com/intent/tweet?text=Wow:&url=https%3A%2F%2Fgithub.com%2FSorashi%2FCleverbot.Net)

Looking for a fast way to use Cleverbot.io or Cleverbot.com in your application? Do you hate dealing with HTTP requests? You found the right tool!

*Note: there is another API wrapper called [Cleverbot.Net](https://github.com/velddev/Cleverbot.Net), which wraps around the Cleverbot.com API, but their first commit was [2017-2-15](https://github.com/velddev/Cleverbot.Net/commit/adf55f845522aff7b1c18391dc08f9b3deae445c), whereas this project started [2016-8-20](https://github.com/Sorashi/Cleverbot.Net/commit/682e3126d91f9a1ad60a70fca5793c4095e15921). So yeah I was first, although I added the support for Cleverbot.com later.*

# How to

1. Create a Visual Studio .NET 4.5 solution and **save it**.
2. In Visual Studio menu bar, go to `Tools -> NuGet Package Manager -> Package Manager Console`.
3. Type `Install-Package Cleverbot.Net` and wait.
4. Aquire your API credentials from [Cleverbot.io](https://cleverbot.io/keys) or [Cleverbot.com](http://www.cleverbot.com/api). [Choose wisely](#cleverbot-comparison).

## If you want to use Cleverbot.io

Create a `CleverbotIoSession` and send anything:

```csharp
var session  = CleverbotSession.NewSession("apiUser", "apiKey");
var response = session.Send("Hello.");
```

A simple console client:

```csharp
using System;
using Cleverbot.Net;

class Program
{
    static void Main()
    {
        string message;
        var session = CleverbotIoSession.NewSession("apiUser", "apiKey");
        do {
            Console.Write("Type your message: ");
            message = Console.ReadLine();
            Console.Write("Bot: ");
            Console.WriteLine(session.Send(message));
        } while (message.ToLower().Trim() != "exit");
    }
}
```

## If you want to use Cleverbot.com

```csharp
// TODO: add
```

# Cleverbot comparison

Feature|Cleverbot.io|Cleverbot.com
-------|------------|-------------
Price|Free        |Free trial of 5000 calls; $1 for 1000 calls monthly; see [Pricing](http://www.cleverbot.com/api/#abovetitle)
Response relevance (IMO)|Not so good|Great

# Asynchronous requests
This wrapper allows you to use the asynchronous functionality. Basically, you just `await` the methods and add *Async* to the end of their names.

```csharp
var session  = await CleverbotSession.NewSessionAsync("apiUser", "apiKey");
var response = await session.SendAsync("Hello.");
```