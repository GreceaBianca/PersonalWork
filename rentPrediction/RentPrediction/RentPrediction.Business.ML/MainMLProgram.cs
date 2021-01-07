using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.ML;
using Newtonsoft.Json;
using RentPrediction.BEModels.DTOs.Csv;
using RentPrediction.Business.Contracts;
using RentPredictionML.Model;

namespace RentPrediction.Business.ML
{
    public class MainMLProgram:IMLManager
    {
        public MainMLProgram() { }

        //Dataset to use for predictions 
        private const string DATA_FILEPATH = "D:\\Faculta\\Licenta\\licenta\\RentPrediction\\Front End\\Infrastructure\\CSV\\data.csv";

        public async Task<OutputModel> PredictPrice(PropertyCsv input)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                            "input1",
                            new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                            {
                                                "Price", input.Price.ToString()
                                            },
                                            {
                                                "UsableSurface", input.UsableSurface.ToString()
                                            },
                                            {
                                                "PropertyType", input.PropertyType
                                            },
                                            {
                                                "Neighborhood", input.Neighborhood
                                            },
                                            {
                                                "Zone", input.Zone
                                            },
                                            {
                                                "Floor", input.Floor
                                            },
                                            {
                                                "MaxFloor", input.MaxFloor
                                            },
                                            {
                                                "HasBalcony", "true"
                                            },
                                            {
                                                "HasParking", "true"
                                            },
                                            {
                                                "HasGarden", "false"
                                            },
                                            {
                                                "HasHeating", "false"
                                            },
                                            {
                                                "NumberOfRooms", input.NumberOfRooms.ToString()
                                            },
                                            {
                                                "NumberOfBaths",input.NumberOfBaths.ToString()
                                            },
                                            {
                                                "NumberOfBalconies", input.NumberOfBalconies.ToString()
                                            },
                                            {
                                                "NumberOfParkingSpots", input.NumberOfParkingSpots.ToString()
                                            },
                                            {
                                                "BuildingSeniority", input.BuildingSeniority
                                            },
                                            {
                                                "BuildingType", input.BuildingType
                                            },
                                            {
                                                "Endowment", input.Endowment
                                            },
                                            {
                                                "Finish", input.Finish
                                            },
                                            {
                                                "Partitioning", input.Partitioning
                                            },
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };

                const string apiKey = "QtJTZg0cGOC3pqB2Hu3yOdpb95Bo/t3l7D8he9umrTQFZ4rPdurxaaXTHl7wzpj1vS63avXSBAQiuO7iqNDb6Q=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                var uri =
                    "https://ussouthcentral.services.azureml.net/workspaces/f880bdde878c4b40aa96674506549e6b/services/93c986fe20ce46439d9e3aabbf911e21/execute?api-version=2.0&format=swagger";
                client.BaseAddress = new Uri(uri);

                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var indexS = result.IndexOf("Labels");
                    var firstPart = result.Substring(0, indexS - 1);
                    var stringS = result.Substring(indexS);
                    result = firstPart + stringS;
                    PriceModel ass = JsonConvert.DeserializeObject<PriceModel>(result);
                    return ass.Results.output1[0];
                }
                else
                {
                    return null;
                  
                }
            }
        }

    }
}
