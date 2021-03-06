// This file was auto-generated by ML.NET Model Builder. 

using System;
using System.IO;
using System.Linq;
using Microsoft.ML;
using RentPredictionML.Model;

namespace RentPredictionML.ConsoleApp
{
    class Program
    {
        //Dataset to use for predictions 
        private const string DATA_FILEPATH = @"D:\Faculta\Licenta\licenta\RentPrediction\Front End\Infrastructure\CSV\data.csv";

        static void Main(string[] args)
        {
            ModelBuilder.CreateModel();
            //// Create single instance of sample data from first line of dataset for model input
            //ModelInput sampleData = CreateSingleDataSample(DATA_FILEPATH);

            //// Make a single prediction on the sample data and print results
            //var predictionResult = ConsumeModel.Predict(sampleData);

            //Console.WriteLine("Using model to make single prediction -- Comparing actual Price with predicted Price from sample data...\n\n");
            //Console.WriteLine($"Id: {sampleData.Id}");
            //Console.WriteLine($"UsableSurface: {sampleData.UsableSurface}");
            //Console.WriteLine($"IsArchived: {sampleData.IsArchived}");
            //Console.WriteLine($"PropertyType: {sampleData.PropertyType}");
            //Console.WriteLine($"StreetName: {sampleData.StreetName}");
            //Console.WriteLine($"Floor: {sampleData.Floor}");
            //Console.WriteLine($"HasBalcony: {sampleData.HasBalcony}");
            //Console.WriteLine($"HasParking: {sampleData.HasParking}");
            //Console.WriteLine($"HasGarden: {sampleData.HasGarden}");
            //Console.WriteLine($"HasHeating: {sampleData.HasHeating}");
            //Console.WriteLine($"IsForSale: {sampleData.IsForSale}");
            //Console.WriteLine($"NumberOfRooms: {sampleData.NumberOfRooms}");
            //Console.WriteLine($"NumberOfBaths: {sampleData.NumberOfBaths}");
            //Console.WriteLine($"NumberOfBalconies: {sampleData.NumberOfBalconies}");
            //Console.WriteLine($"NumberOfParkingSpots: {sampleData.NumberOfParkingSpots}");
            //Console.WriteLine($"BuildingSeniority: {sampleData.BuildingSeniority}");
            //Console.WriteLine($"BuildingType: {sampleData.BuildingType}");
            //Console.WriteLine($"Endowment: {sampleData.Endowment}");
            //Console.WriteLine($"Finish: {sampleData.Finish}");
            //Console.WriteLine($"Partitioning: {sampleData.Partitioning}");
            //Console.WriteLine($"\n\nActual Price: {sampleData.Price} \nPredicted Price: {predictionResult.Score}\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }

        // Change this code to create your own sample data
        #region CreateSingleDataSample
        // Method to load single row of dataset to try a single prediction
        private static ModelInput CreateSingleDataSample(string dataFilePath)
        {
            // Create MLContext
            MLContext mlContext = new MLContext();

            // Load dataset
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: dataFilePath,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Use first line of dataset as model input
            // You can replace this with new test data (hardcoded or from end-user application)
            ModelInput sampleForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false)
                                                                        .First();
            return sampleForPrediction;
        }
        #endregion
    }
}
