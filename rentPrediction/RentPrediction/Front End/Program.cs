using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace Front_End
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //ModelInput input = new ModelInput()
            //{
            //    Vendor_id = "CMT",
            //    Rate_code = 1,
            //    Passenger_count = 1,
            //    Trip_distance = 3.8f,
            //    Payment_type = "CRD"
            //};

            //// Make prediction
            //ModelOutput prediction = ConsumeModel.Predict(input);

            //// Print Prediction
            //Console.WriteLine($"Predicted Fare: {prediction.Score}");
            //Console.ReadKey();
          
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
