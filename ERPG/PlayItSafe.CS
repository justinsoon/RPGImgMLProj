using RestSharp;
using System;
using System.Threading;

namespace BOT
{
    public static class PlayItSafe
    {
        public static void KeepOuttaJail()
        {
            Thread.Sleep(5000);

            var lastMessage = Helpers.GetMessages(1)[0];

            if (ENV.DebugMode)
                Console.Write(Environment.NewLine + ObjectDumper.Dump(lastMessage));

            if (lastMessage.Content.Contains("EPIC GUARD"))
            {
                var prediction = MachineLearning.PredictItemFromUri(lastMessage.Attachments[0].URL);

                var JsonData = new { content = prediction, nonce = Helpers.RandomIntAsString(), tts = false };

                var client = new RestClient(ENV.Host);

                var request = new RestRequest(Method.POST)
                    .AddHeader("Accept", "application/json")
                    .AddHeader("Authorization", Headers.Authorization)
                    .AddJsonBody(JsonData);

                Console.WriteLine(Environment.NewLine + $"{DateTime.Now} :: EPIC GUARD Start");

                var response = client.Execute(request);

                Console.WriteLine($"{DateTime.Now} :: EPIC GUARD {response.ResponseStatus}");

                if (ENV.DebugMode)
                    Console.WriteLine(Environment.NewLine + response.Content);
                    Console.WriteLine(Environment.NewLine + response.Content);

                Thread.Sleep(5000);

                foreach (Message message in Helpers.GetMessages(5))
                {
                    if (!message.Content.ToUpper().Contains("EVERYTHING SEEMS FINE") || message.Content.ToUpper().Contains("IS NOW IN JAIL"))
                        TerminateExecution();
                }

            }
        }

        public static void TerminateExecution()
        {
            Helpers.PlayAlert();
            Thread.Sleep(5000);
            Environment.Exit(0);
        }
    }
}
