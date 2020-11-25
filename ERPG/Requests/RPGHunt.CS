using RestSharp;
using System;
using System.Threading.Tasks;

namespace BOT.Requests
{
    public static class RPGHunt
    {
        private static readonly string Command = "RPG hunt";
        //private static readonly int CooldownInSeconds = 35;
        private static readonly int CooldownInSeconds = RPGCD.Hunt;

        private static IRestResponse Response;
        private static DateTime LastCallTime;
        private static int SecondsElapsed;

        public static async void MakeRequest()
        {
            // resets response from previous call, if any
            if (Response != null) Response = null;

            // create POST data
            var JsonData = new { content = Command, nonce = Helpers.RandomIntAsString(), tts = false };

            // if the call has been made in a previous iteration, then establish how many seconds have passed since
            if (LastCallTime != DateTime.MinValue)
                SecondsElapsed = Convert.ToInt32((DateTime.Now - LastCallTime).TotalSeconds);

            //  if the cooldown has reset or if this is the first iteration, make the API call
            if ((SecondsElapsed > CooldownInSeconds) || (LastCallTime == DateTime.MinValue))
            {
                var client = new RestClient(ENV.Host);

                var request = new RestRequest(Method.POST)
                    .AddHeader("Accept", "application/json")
                    .AddHeader("Authorization", Headers.Authorization)
                    .AddJsonBody(JsonData);

                Console.WriteLine($"{DateTime.Now} :: {Command} Start");

                Response = client.Execute(request);

                Console.WriteLine($"{DateTime.Now} :: {Command} {Response.ResponseStatus}");
                Helpers.PrintRuntimeStats();

                if (ENV.DebugMode)
                    Helpers.PrintEnclosedInNewLines(Response.Content);

                PlayItSafe.KeepOuttaJail();

                LastCallTime = DateTime.Now;
                SecondsElapsed = 0;

                Helpers.PrintDashes(100);
            }

            // if the cooldown has not finished resetting, wait for the remainder of the time
            if (SecondsElapsed < CooldownInSeconds)
                await Task.Delay((CooldownInSeconds - SecondsElapsed) * 1000);
        }
    }
}
