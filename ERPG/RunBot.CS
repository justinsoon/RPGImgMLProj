using BOT.Requests;
using System;
using System.Threading;

namespace BOT
{
    public class RunBot
    {
        public static void Main()
        {
            ENV.DebugMode = true;

            Helpers.PrintDisclaimer();

            Console.Write("Channel ID: ");
            ENV.Host = "https://discord.com/api/v8/channels/" + Console.ReadLine() + "/messages";

            Console.Write("Authorization Header: ");
            Headers.Authorization = Console.ReadLine();

            Console.Write("Input chop, fish, pickup, etc...\n");
            Console.Write("Work type: ");
            RPGWorkType.WorkType = Console.ReadLine();

            Console.Write("Input the cooldown in seconds.\n");
            Console.Write("Work CD: ");
            RPGCD.Work = Convert.ToInt32(Console.ReadLine());

            Console.Write("Hunt CD: ");
            RPGCD.Hunt = Convert.ToInt32(Console.ReadLine());

            Console.Write("Heal CD: ");
            RPGCD.Heal = Convert.ToInt32(Console.ReadLine());

            Console.Write("BuyHP CD: ");
            RPGCD.BuyHP = Convert.ToInt32(Console.ReadLine());

            //Console.Write("Adventure CD: ");
            //RPGCD.Adventure = Console.ReadLine();

            Helpers.PrintDashes(100);

            ENV.RuntimeInHours = 12;

            Utility.Runtime.Start();

            while (Utility.Runtime.Elapsed < TimeSpan.FromHours(ENV.RuntimeInHours))
            {
                RPGWork.MakeRequest("RPG " + RPGWorkType.WorkType);
                RPGBuyHP.MakeRequest("RPG buy life potion 1");
                RPGHeal.MakeRequest();
                RPGHunt.MakeRequest();
            }

            Utility.Runtime.Stop();
        }
    }
}
