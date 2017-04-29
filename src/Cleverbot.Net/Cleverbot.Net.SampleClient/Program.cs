using System;

namespace Cleverbot.Net.SampleClient
{
    internal class Program
    {
        private static void Main() {
            string message;
            // use either Cleverbot.com or Cleverbot.io
            var comSession = CleverbotComSession.NewSession("apiKey");
            var ioSession = CleverbotIoSession.NewSession("apiUser", "apiKey");
            do {
                Console.Write("Type your message: ");
                message = Console.ReadLine();
                Console.Write("Bot: ");
                Console.WriteLine(comSession.Send(message)); // or ioSession.Send(message)
            } while (message?.ToLower().Trim() != "exit");
        }
    }
}