using Cleverbot.Net;
using System;

internal class Program
{
    private static void Main() {
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