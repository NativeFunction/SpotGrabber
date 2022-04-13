// This file was auto-generated by ML.NET Model Builder. 

using System;
using GenericParkingLot.Model;


namespace GenericParkingLot.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Input path to image for test: ");
            string path = Console.ReadLine();

            ModelInput sampleData = new ModelInput()
            {
                ImageSource = path,
            };


            // Make a single prediction on the sample data and print results

            var predictionResult = ConsumeModel.Predict(sampleData);//300 ms per image - 500 images = 2.4s

            

            Console.WriteLine("Using model to make single prediction -- Comparing actual Label with predicted Label from sample data...\n\n");
            Console.WriteLine($"ImageSource: {sampleData.ImageSource}");
            Console.WriteLine($"\n\nPredicted Label value {predictionResult.Prediction} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n\n");
            
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }
    }
}
