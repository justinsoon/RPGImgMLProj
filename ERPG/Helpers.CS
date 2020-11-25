using NAudio.Wave;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BOT
{
    public static class Helpers
    {
        public static Message[] GetMessages(int amount)
        {
            var client = new RestClient(ENV.Host);

            var request = new RestRequest(Method.GET)
                .AddParameter("limit", amount)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", Headers.Authorization);

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<Message[]>(response.Content);
        }

        public static string RandomIntAsString()
        {
            Random random = new Random();
            return random.Next(int.MaxValue).ToString();
        }

        public static void PrintRuntimeStats()
        {
            Console.WriteLine(new StringBuilder()
                .Append($"{DateTime.Now} :: {Utility.Runtime.Elapsed} Elapsed")
                .Append(" & ")
                .Append($"{TimeSpan.FromHours(ENV.RuntimeInHours) - Utility.Runtime.Elapsed} Remaining")
                .ToString());
        }

        public static void PrintDashes(int amount)
        {
            for (int i = 0; i < amount; i++)
                Console.Write("-");

            Console.WriteLine();
        }

        public static void PrintEnclosedInNewLines(string content)
        {
            Console.Write(Environment.NewLine);
            Console.Write(content);
            Console.Write(Environment.NewLine);
        }

        public static async void PlayAlert()
        {
            using var audioFile = new AudioFileReader(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Epic-Guard-Alert.MP3");
            using var outputDevice = new WaveOutEvent();

            outputDevice.Init(audioFile);
            outputDevice.Play();

            // Asynchronously Wait Until Playback Completes
            if (outputDevice.PlaybackState == PlaybackState.Playing)
                await Task.Delay(audioFile.TotalTime);
        }

        public static void PrintDisclaimer()
        {
            Console.WriteLine();
            Console.WriteLine("          # # # # # # # # # # # # # # # # DISCLAIMER # # # # # # # # # # # # # # # #");
            Console.WriteLine("          #                THIS BOT IS FOR RESEARCH PURPOSES ONLY.                 #");
            Console.WriteLine("          #     USE IT AT YOUR OWN RISK! IF YOU GET BANNED IT'S NOT MY PROBLEM.    #");
            Console.WriteLine("          #          PLEASE UPDATE OFFSETS DEPENDENT ON YOUR PROGRESS.             #");
            Console.WriteLine("          # TO CHANGE THIS: FORK, CHANGE, BUILD. DON'T ASK FOR CHANGES OR UPDATES. #");
            Console.WriteLine("          # # # # # # # # # # # # # # # # DISCLAIMER # # # # # # # # # # # # # # # #");
            Console.WriteLine();
        }
    }
}
