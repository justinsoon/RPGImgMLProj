using ERPGML.Model;
using System;
using System.IO;
using System.Net;

namespace BOT
{
    public class MachineLearning
    {
        public static string PredictItemFromUri(string uri)
        {
            var temp = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "epic_guard.png";

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(uri), temp);
            }

            // Create Single Instance Of Sample Data
            ModelInput sampleData = new ModelInput()
            {
                ImageSource = temp
            };

            // Make A Single Prediction On The Sample Data
            var predictionResult = ConsumeModel.Predict(sampleData);

            File.Delete(temp);

            Console.WriteLine($"Image Source: {uri}");
            Console.WriteLine($"Predicted Label Value: {predictionResult.Prediction}");
            Console.WriteLine($"Predicted Label Scores: [{string.Join(", ", predictionResult.Score)}]");

            return predictionResult.Prediction; 
        }
    }
}
