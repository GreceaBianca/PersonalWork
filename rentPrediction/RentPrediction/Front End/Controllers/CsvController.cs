using AutoMapper;
using CsvHelper;
using Front_End.Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using RentPrediction.BEModels.DTOs.Csv;
using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CsvController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IPropertyManager _propertyManager;
        private readonly IMLManager _mlManager;

        public CsvController(IPropertyManager propertyManager, IMapper mapper, IMLManager mlManager)
        {
            _mlManager = mlManager;
            _propertyManager = propertyManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task Predict()
        {
            await _mlManager.PredictPrice(null);
        }

        [HttpGet]
        public async Task CreateCsv()
        {
            var properties = await _propertyManager.GetAll();
            var models = new List<PropertyCsv>();
            var include = true;
            foreach (var property in properties)
            {
                include = true;
                if (!property.IsArchived) include = false;
                var model = _mapper.Map<Property, PropertyCsv>(property);
                model.Floor= property.Address.Floor.Split('/')[0];
                model.MaxFloor= property.Address.Floor.Split('/')[1];
                if (!string.IsNullOrEmpty(model.Neighborhood))
                {
                    if (model.Neighborhood.Contains("Cartier:"))
                    {
                        model.Neighborhood = model.Neighborhood.Split("Cartier:")[1];
                    }
                    if (model.Neighborhood.Contains(','))
                    {
                        if (model.Neighborhood.Contains("Zona"))
                        {
                            model.Neighborhood = property.Address.StreetName.Split(',')[1];
                            model.Zone = property.Address.StreetName.Split(',')[0].Split("Zona ")[^1];
                        }
                        else
                        {
                            model.Neighborhood = property.Address.StreetName.Split(',')[0];
                            model.Zone = property.Address.StreetName.Split(',')[1];
                            if (model.Zone.Contains("zona "))
                            {
                                model.Zone = model.Zone.Split("zona ")[^1];
                            }
                            else
                            {
                                include = false;
                                model.Zone = "Necunoscuta";
                            }
                        }
                        if (string.IsNullOrEmpty(model.Zone))
                        {
                            include = false;
                            model.Zone = "Necunoscuta";
                        }
                    }
                    if (model.Neighborhood.Contains("Floresti"))
                    {
                        model.Neighborhood = "Floresti";
                    }
                    if (model.Neighborhood.Contains("Marasti"))
                    {
                        model.Neighborhood = "Marasti";
                    }
                    if (model.Neighborhood.Contains("Zorilor"))
                    {
                        model.Neighborhood = "Zorilor";
                    }
                    if (model.Neighborhood.Contains("Manastur"))
                    {
                        model.Neighborhood = "Manastur";
                    }
                    if (model.Neighborhood.Contains("Buna Ziua"))
                    {
                        model.Neighborhood = "Buna Ziua";
                    }
                    if (model.Neighborhood.Contains("Gheorgheni"))
                    {
                        model.Neighborhood = "Gheorgheni";
                    }
                    if (model.Neighborhood.Contains("Grigorescu"))
                    {
                        model.Neighborhood = "Grigorescu";
                    }
                    if (model.Neighborhood.Contains("Intre"))
                    {
                        model.Neighborhood = "Intre Lacuri";
                    }
                    if (model.Neighborhood.Contains("Andrei Muresanu"))
                    {
                        model.Neighborhood = "Andrei Muresanu";
                    }
                    if (model.Neighborhood.Contains("Central"))
                    {
                        model.Neighborhood = "Central";
                    }
                    if (model.Neighborhood.Contains("Rotund"))
                    {
                        model.Neighborhood = "Dambul Rotund";
                    }
                    if (model.Neighborhood.Contains("Borhanci"))
                    {
                        model.Neighborhood = "Borhanci";
                    }
                    if (model.Neighborhood.Contains("Baciu"))
                    {
                        model.Neighborhood = "Baciu";
                    }
                    if (model.Neighborhood.Contains("Iris"))
                    {
                        model.Neighborhood = "Iris";
                    }
                }
                else
                {
                    include = false;
                    model.Neighborhood = "Necunoscut";
                }
                if (model.Price.Contains(' '))
                {
                    model.Price = model.Price.Split(' ')[0];
                }
                else
                {
                    if (model.Price.Contains('€'))
                    {
                        model.Price = model.Price.Split('€')[0];
                    }
                }

                if (model.NumberOfRooms == 0)
                {
                    model.NumberOfRooms = 1;
                }
                if (string.IsNullOrEmpty(model.Zone))
                {
                    include = false;
                    model.Zone = "Necunoscuta";
                }

                try
                {
                    if (model.Price.Contains('.'))
                    {
                        float smallPrice = float.Parse(model.Price);
                        smallPrice = smallPrice * 1000;
                        model.Price = (Convert.ToInt32(smallPrice)).ToString();
                    }
                }
                catch (Exception e) { }

                if (include)
                {
                    models.Add(model);
                }
            }

            Create(models);
        }
        public void Create(List<PropertyCsv> data)
        {

            using (var writer = new StreamWriter("D:\\Faculta\\Licenta\\licenta\\RentPrediction\\Front End\\Infrastructure\\CSV\\data.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(data);
            }
        }
    }
}
