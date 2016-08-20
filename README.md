Cleverbot.Net
===
[![NuGet](https://img.shields.io/nuget/v/Cleverbot.Net.svg?maxAge=2592000)](https://www.nuget.org/packages/Cleverbot.Net/)
[![GitHub issues](https://img.shields.io/github/issues/Sorashi/Cleverbot.Net.svg)](https://github.com/Sorashi/Cleverbot.Net/issues)
[![GitHub stars](https://img.shields.io/github/stars/Sorashi/Cleverbot.Net.svg)](https://github.com/Sorashi/Cleverbot.Net/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/Sorashi/Cleverbot.Net.svg)](https://github.com/Sorashi/Cleverbot.Net/network)
[![Twitter](https://img.shields.io/twitter/url/https/github.com/Sorashi/Cleverbot.Net.svg?style=social)](https://twitter.com/intent/tweet?text=Wow:&url=https%3A%2F%2Fgithub.com%2FSorashi%2FCleverbot.Net)

Looking for a fast way to use Cleverbot.io in your application? Do you hate dealing with HTTP requests? You found the right tool!

This project uses [cleverbot.io][1].

# How to

1. Create a Visual Studio .NET 4.5 solution and **save it**.
2. In Visual Studio menu bar, go to `Tools -> NuGet Package Manager -> Package Manager Console`.
3. Type `Install-Package Cleverbot.Net` and wait.
4. Aquire your API credentials [here](https://cleverbot.io/keys).

Then create a `CleverbotSession` and send anything:

```csharp
var session = CleverbotSession.NewSession("apiUser", "apiKey");
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
        var session = CleverbotSession.NewSession("apiUser", "apiKey");
        do {
            Console.Write("Type your message: ");
            message = Console.ReadLine();
            Console.Write("Bot: ");
            Console.WriteLine(session.Send(message));
        } while (message.ToLower().Trim() != "exit");
    }
}
```

[1]: http://cleverbot.io