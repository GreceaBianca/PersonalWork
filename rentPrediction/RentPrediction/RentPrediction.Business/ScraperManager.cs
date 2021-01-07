using AutoMapper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using RentPrediction.BEModels.DTOs.Address;
using RentPrediction.BEModels.DTOs.Feature;
using RentPrediction.BEModels.DTOs.Property;
using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;
using RentPrediction.Repo.Contracts;
using RentPredictionML.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Internal;
using RentPrediction.BEModels.DTOs.Csv;

namespace RentPrediction.Business
{
    public class ScraperManager : IScraperManager
    {
        private readonly IPropertyManager _propertyManager;
        private readonly IMapper _mapper;
        private readonly IPropertyTypeManager _propertyTypeManager;
        private readonly IAddressManager _addressManager;
        private readonly IFeatureManager _featureManager;
        private readonly IMLManager _mlManager;
        private readonly IPartitioningRepository _partitioningRepository;

        public ScraperManager(IMLManager mlManager, IPropertyManager propertyManager, IPropertyTypeManager propertyTypeManager, IPartitioningRepository partitioningRepository, IMapper mapper, IAddressManager addressManager, IFeatureManager featureManager)
        {
            _mlManager = mlManager;
            _propertyManager = propertyManager;
            _propertyTypeManager = propertyTypeManager;
            _featureManager = featureManager;
            _addressManager = addressManager;
            _partitioningRepository = partitioningRepository;
            _mapper = mapper;
        }

        public async Task StartScrapping(int userId)
        {
            IWebDriver driver = new ChromeDriver();
            StringBuilder verificationErrors = new StringBuilder();
            List<PropertyType> propertyTypes = _propertyTypeManager.GetAllPropertyTypes().ToList();

            List<Partitioning> partitionings = _partitioningRepository.GetAllPartitionings().ToList();
            driver.Navigate().GoToUrl("https://www.blitz-imobiliare.ro/inchirieri-apartamente-cluj");

            Thread.Sleep(LevelThreadSleep(5)); //element is not yet visible https://sqa.stackexchange.com/questions/38469/c-element-not-interactable-error
            driver.FindElement(By.XPath("//div[4]/div/button[2]")).Click();
            var hasMorePages = true;
            var properties = new List<Property>();
 
            
            while (hasMorePages)
            {
                try
                {
                    IWebElement blitzContainer = driver.FindElement(By.ClassName("lista-oferte"));
                    List<IWebElement> blitzProperties = blitzContainer.FindElements(By.ClassName("card__el")).ToList();

                    for (var i = 0; i < blitzProperties.Count; i++)
                    {
                        var blitzProperty = blitzProperties[i];

                        var newProperty = new Property();
                        newProperty.Address = new Address();
                        newProperty.Feature = new Feature();
                        newProperty.UserId = userId;
                        //I suppose it is not a problem
                        newProperty.Address.Country = "Romania";
                        Thread.Sleep(LevelThreadSleep(2));
                        var price = blitzProperty.FindElement(By.ClassName("card__price--full")).Text.Split(' ');
                        if (price.Length > 0)
                            newProperty.Price = price[0];

                        var descriptionDetails = blitzProperty.FindElement(By.ClassName("card__description")).Text;
                        var commaValues = descriptionDetails.Split(',');

                        newProperty.Address.County = commaValues[0];
                        if (commaValues.Length > 2)
                            newProperty.Address.StreetName = commaValues[1] + " , " + commaValues[2];

                        PropertyType propertyType = null;
                        var propertyTypeName = "Garsoniera";
                        if (commaValues.Length == 4)
                        {
                            var propertyName = commaValues[3].Split(' ');
                            if (propertyName.Length > 1)
                            {
                                propertyTypeName = propertyName[1];
                            }
                            else
                            {
                                propertyTypeName = commaValues[3];
                            }
                        }

                        propertyType = propertyTypes.Find(pt => pt.Name == propertyTypeName);
                        newProperty.PropertyTypeId = propertyType?.Id ?? 1;

                        newProperty.Name = blitzProperty.FindElement(By.ClassName("card__title")).Text;

                        try
                        {
                            var url = blitzProperty.FindElement(By.ClassName("card__image"));
                            if (url != null)
                            {

                                newProperty.URL = url.FindElement(By.TagName("a")).GetAttribute("href");
                                url.Click();

                                //now we are in the Property's page
                                try
                                {
                                    try
                                    {
                                        IWebElement imagesContent = driver.FindElement(By.Id("photo-content"));
                                        List<IWebElement> images =
                                            imagesContent.FindElements(By.TagName("img")).ToList();
                                        newProperty.Galleries = new List<Gallery>();
                                        var maxLength = images.Count >= 3 ? 3 : images.Count;
                                        //for (var imageindex = 0; imageindex < images.Count; imageindex++)
                                        for (var imageindex = 0; imageindex < maxLength; imageindex++)
                                        {
                                            var image = images[imageindex];
                                            var imageUrl = image.GetAttribute("src");
                                            var imageName = image.GetAttribute("alt");
                                            WebClient downloader = new WebClient();
                                            string time = string.Format(@"{0}", DateTime.Now.Ticks);
                                            downloader.DownloadFile(imageUrl,
                                                "D:\\Faculta\\Licenta\\licenta\\RentPrediction\\Front End\\ClientApp\\src\\assets\\img\\propertyImages\\" +
                                                time  + ".jpg");
                                            newProperty.Galleries.Add(new Gallery()
                                            {
                                                ImageURL =
                                                    "D:\\Faculta\\Licenta\\licenta\\RentPrediction\\Front End\\ClientApp\\src\\assets\\img\\propertyImages\\" +
                                                    time + ".jpg"
                                            });
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                    }
                                    Thread.Sleep(LevelThreadSleep(2));
                                    IWebElement caracteristici = driver.FindElement(By.ClassName("properties"));
                                    try
                                    {
                                        Thread.Sleep(LevelThreadSleep(2));
                                        List<IWebElement> toateCaracteristicile =
                                            caracteristici.FindElements(By.TagName("p")).ToList();
                                        Thread.Sleep(LevelThreadSleep(2));

                                        var camere = toateCaracteristicile[0].Text.Split(' ');
                                        if (camere[camere.Length - 1] == "Garsoniera")
                                        {
                                            newProperty.PropertyTypeId =
                                                propertyTypes.Find(pt => pt.Name == "Garsoniera").Id;
                                            newProperty.Feature.NumberOfRooms = 0;
                                        }
                                        else
                                        {
                                            newProperty.Feature.NumberOfRooms = Int32.Parse(camere[camere.Length - 1]);
                                        }

                                        var suprafataUtila = toateCaracteristicile[1].Text.Split(' ');
                                        var suprafata = suprafataUtila[suprafataUtila.Length - 2];
                                        newProperty.UsableSurface = Int32.Parse(suprafata);

                                        var bai = toateCaracteristicile[2].Text.Split(' ');
                                        newProperty.Feature.NumberOfBaths = Int32.Parse(bai[bai.Length - 1]);

                                        var index = 3;
                                        var balcon = toateCaracteristicile[3].Text.Split(' ');
                                        if (balcon[1].Contains("balcoane"))
                                        {
                                            newProperty.Feature.HasBalcony = true;
                                            newProperty.Feature.NumberOfBalconies =
                                                Int32.Parse(balcon[balcon.Length - 1]);
                                            index++;
                                        }
                                        else
                                        {
                                            newProperty.Feature.HasBalcony = false;
                                            newProperty.Feature.NumberOfBalconies = 0;
                                        }

                                        var floor = toateCaracteristicile[index].Text.Split(' ');
                                        newProperty.Address.Floor = floor[floor.Length - 2] + floor[floor.Length - 1];
                                        index++;

                                        var parcare = toateCaracteristicile[index].Text.Split(' ');
                                        if (parcare[2].Contains("parcare"))
                                        {
                                            newProperty.Feature.HasParking = true;
                                            newProperty.Feature.NumberOfParkingSpots = Int32.Parse(parcare[3]);
                                            index++;
                                        }
                                        else
                                        {
                                            newProperty.Feature.HasParking = false;
                                            newProperty.Feature.NumberOfParkingSpots = 0;
                                        }

                                        var partition = toateCaracteristicile[index].Text.Split(' ');
                                        var partitioning = partitionings.Find(p =>
                                            p.Name == partition[partition.Length - 1]);
                                        if (partitioning != null)
                                        {
                                            newProperty.Feature.PartitioningId = partitioning.Id;
                                        }
                                        else
                                        {
                                            newProperty.Feature.PartitioningId = partitionings[0].Id;
                                        }

                                        index++;
                                        var buildingSeniority = toateCaracteristicile[index].Text.Split(' ');
                                        newProperty.Feature.BuildingSeniority =
                                            buildingSeniority[buildingSeniority.Length - 1];

                                        index += 2;
                                        var availableTime = toateCaracteristicile[index].Text.Split(':');
                                        newProperty.Feature.AvailableTime = availableTime[availableTime.Length - 1];

                                        index++;
                                        var buildingType = toateCaracteristicile[index].Text.Split(':');
                                        newProperty.Feature.BuildingType = buildingType[buildingType.Length - 1];

                                        index++;
                                        var endowment = toateCaracteristicile[index].Text.Split(':');
                                        newProperty.Feature.Endowment = endowment[endowment.Length - 1];

                                        index++;
                                        var finish = toateCaracteristicile[index].Text.Split(':');
                                        newProperty.Feature.Finish = finish[finish.Length - 1];

                                        //try
                                        //{
                                        //    var altele = driver.FindElements(By.ClassName("dropdown__container"));
                                        //    altele[0].Click();
                                        //    var descriptionValue = altele[0];
                                        //    descriptionValue = altele[0].FindElement(By.ClassName("dropdown__el"));
                                        //    newProperty.Description =
                                        //        descriptionValue.FindElement(By.TagName("p")).Text;
                                        //}
                                        //catch (Exception e)
                                        //{
                                        //}

                                        newProperty.CreationDate = DateTime.UtcNow;
                                        properties.Add(newProperty);

                                    }
                                    catch (Exception e)
                                    {
                                    }
                                    finally
                                    {
                                        driver.Navigate().Back();
                                    }
                                }
                                catch (Exception e)
                                {
                                    i = i - 1;
                                }
                            }
                            else
                            {
                                i = i - 1;
                            }
                        }
                        catch (Exception e)
                        {
                        }

                        blitzContainer = driver.FindElement(By.ClassName("lista-oferte"));
                        blitzProperties = blitzContainer.FindElements(By.ClassName("card__el")).ToList();
                    }

                    try
                    {
                        IWebElement pagesContainer = driver.FindElement(By.ClassName("pagination"));
                        List<IWebElement> pages = pagesContainer.FindElements(By.TagName("li")).ToList();
                        for (var j = 0; j < pages.Count; j++)
                        {
                            if (pages[j].GetAttribute("class").Contains("active"))
                            {
                                if (j == pages.Count - 1)
                                {
                                    hasMorePages = false;
                                }
                                else
                                {
                                    driver.ExecuteJavaScript("arguments[0].scrollIntoView(true);", pagesContainer);
                                    Thread.Sleep(LevelThreadSleep(3));
                                    pages[j + 1].FindElement(By.TagName("a")).Click();

                                    Thread.Sleep(LevelThreadSleep(4));
                                    break;
                                }
                            }
                            pagesContainer = driver.FindElement(By.ClassName("pagination"));
                            pages = pagesContainer.FindElements(By.TagName("li")).ToList();
                        }
                    }
                    catch (Exception e)
                    {
                        hasMorePages = false;
                    }
                }
                catch (Exception e)
                {
                    hasMorePages = false;
                }

                if (properties.Count >= 50)
                {
                    hasMorePages = false;
                }
            }

            for (var k = 0; k < properties.Count; k++)
            {
                var property = properties[k];
                try
                {

                    var existsProperty = await _propertyManager.GetPropertyByURL(property.URL);
                    if (existsProperty == null)
                    {
                        property.PredictedPrice = await PredictPrice(property);
                        await _propertyManager.AddProperty(property);
                    }
                    else
                    {
                        var propertyDto = _mapper.Map<Property, PropertyDto>(existsProperty);
                        var addressDto = propertyDto.Address;
                        var featureDto = propertyDto.Feature;

                        if (!CompareAddresses(addressDto, property.Address))
                        {
                            var addressUpdatedDto = UpdateAdress(addressDto, property.Address);
                            var addressUpdated = _mapper.Map<AddressDto, Address>(addressUpdatedDto);
                            await _addressManager.UpdateAddress(addressUpdated);
                        }

                        if (!CompareFeatures(featureDto, property.Feature))
                        {
                            var featureUpdatedDto = UpdateFeature(featureDto, property.Feature);
                            var featureUpdated = _mapper.Map<FeatureDto, Feature>(featureUpdatedDto);
                            await _featureManager.UpdateFeature(featureUpdated);
                        }
                        var predictedPrice = await PredictPrice(property);
                        propertyDto.PredictedPrice = predictedPrice;
                        var propertyEntity = _mapper.Map<PropertyDto, Property>(propertyDto);
                        propertyEntity.UserId = userId;
                        if (propertyEntity.PropertyTypeId == 0)
                        {
                            propertyEntity.PropertyTypeId = propertyEntity.PropertyType.Id;
                        }

                        propertyEntity.PropertyType = null;
                        propertyEntity.Address = null;
                        propertyEntity.User = null;
                        propertyEntity.Feature = null;
                        propertyEntity.Galleries = null;
                        propertyEntity.Favorites = null;
                        propertyEntity.Description = propertyDto.Description;
                        propertyEntity.Price = propertyDto.Price;
                        propertyEntity.UsableSurface = propertyDto.UsableSurface;
                        propertyEntity.PredictedPrice = predictedPrice;
                        await _propertyManager.UpdateProperty(propertyEntity);
                    }
                }
                catch (Exception e)
                {
                    var newEx = e;
                    var problem = property;
                }
            }
            await VerifyUpdate(userId);
        }
        private bool CompareFeatures(FeatureDto p1, Feature p2)
        {
            if (p1.BuildingSeniority != p2.BuildingSeniority) return false;
            if (p1.BuildingType != p2.BuildingType) return false;
            if (p1.Endowment != p2.Endowment) return false;
            if (p1.Finish != p2.Finish) return false;
            if (p1.HasBalcony != p2.HasBalcony) return false;
            if (p1.HasGarden != p2.HasGarden) return false;
            if (p1.HasHeating != p2.HasHeating) return false;
            if (p1.HasParking != p2.HasParking) return false;
            if (p1.NumberOfBalconies != p2.NumberOfBalconies) return false;
            if (p1.NumberOfBaths != p2.NumberOfBaths) return false;
            if (p1.NumberOfParkingSpots != p2.NumberOfParkingSpots) return false;
            if (p1.NumberOfRooms != p2.NumberOfRooms) return false;
            return true;
            // return p1.Feature.Partitioning.Name == p2.Feature.Partitioning.Name;
        }

        private FeatureDto UpdateFeature(FeatureDto f2, Feature f)
        {
            if (!string.IsNullOrEmpty(f2.BuildingSeniority))
                f2.BuildingSeniority = f.BuildingSeniority;
            if (!string.IsNullOrEmpty(f2.BuildingType))
                f2.BuildingType = f.BuildingType;
            if (!string.IsNullOrEmpty(f2.Endowment))
                f2.Endowment = f.Endowment;
            if (!string.IsNullOrEmpty(f2.Finish))
                f2.Finish = f.Finish;
            f2.HasBalcony = f.HasBalcony;
            f2.HasGarden = f.HasGarden;
            f2.HasHeating = f.HasHeating;
            f2.HasParking = f.HasParking;
            f2.NumberOfBalconies = f.NumberOfBalconies;
            f2.NumberOfBaths = f.NumberOfBaths;
            f2.NumberOfParkingSpots = f.NumberOfParkingSpots;
            f2.NumberOfRooms = f.NumberOfRooms;
            return f2;
        }
        private bool CompareAddresses(AddressDto p1, Address p2)
        {
            if (p1.Country != p2.Country) return false;
            if (p1.County != p2.County) return false;
            if (p1.Floor != p2.Floor) return false;
            if (p1.StreetName != p2.StreetName) return false;
            if (p1.StreetNumber != p2.StreetNumber) return false;
            return true;
            // return p1.Feature.Partitioning.Name == p2.Feature.Partitioning.Name;
        }

        private AddressDto UpdateAdress(AddressDto a, Address a2)
        {
            if (!string.IsNullOrEmpty(a2.Country))
                a.Country = a2.Country;
            if (!string.IsNullOrEmpty(a2.County))
                a.County = a2.County;
            if (!string.IsNullOrEmpty(a2.Floor))
                a.Floor = a2.Floor;
            if (!string.IsNullOrEmpty(a2.StreetName))
                a.StreetName = a2.StreetName;
            return a;
        }
        private bool CompareProperties(PropertyDto p1, Property p2)
        {
            if (p1.Description != p2.Description) return false;
            if (p1.Price != p2.Price) return false;
            if (p1.Surface != p2.Surface) return false;
            if (p1.UsableSurface != p2.UsableSurface) return false;
            return true;
            // return p1.Feature.Partitioning.Name == p2.Feature.Partitioning.Name;
        }
        private Property UpdateProperty(PropertyDto p2, Property p1)
        {
            p1.Description = p2.Description;
            p1.Price = p2.Price;
            p1.Surface = p2.Surface;
            p1.UsableSurface = p2.UsableSurface;
            return p1;
            // return p1.Feature.Partitioning.Name == p2.Feature.Partitioning.Name;
        }

        private async Task VerifyUpdate(int userId)
        {
            IWebDriver driver = new ChromeDriver();

            var properties = _propertyManager.GetAllProperties().ToList();
            driver.Navigate().GoToUrl(properties[0].URL);

            Thread.Sleep(LevelThreadSleep(5)); //element is not yet visible https://sqa.stackexchange.com/questions/38469/c-element-not-interactable-error

            var ex = driver.FindElement(By.XPath("//div[4]/div/button[2]"));
            if (ex.GetAttribute("title") == "Accept cookies")
                ex.Click();
            List<PropertyType> propertyTypes = _propertyTypeManager.GetAllPropertyTypes().ToList();

            List<Partitioning> partitionings = _partitioningRepository.GetAllPartitionings().ToList();
            foreach (var property in properties)
            {
                try
                {
                    driver.Navigate().GoToUrl(property.URL);

                    try
                    {
                        try
                        {
                            var exception = driver.FindElement(By.ClassName("inactive-offer-msg"));
                            property.IsArchived = true;
                            property.ArchivedDate = DateTime.UtcNow;
                            await _propertyManager.UpdateProperty(property);
                        }
                        catch
                        {
                            var newProperty = new Property();
                            newProperty.Address = new Address();
                            newProperty.Feature = new Feature();
                            newProperty.UserId = userId;
                            //I suppose it is not a problem
                            newProperty.Address.Country = "Romania";
                            newProperty.Address.County = "Cluj-Napoca";

                            var price = driver.FindElement(By.ClassName("pret-intreg")).Text;
                            var dontSetPrice = false;
                            if (price.Length > 0)
                                newProperty.Price = price;
                            else
                            {
                                dontSetPrice = true;
                            }

                            var zone = driver.FindElement(By.ClassName("info__cartier")).Text;
                            if (!string.IsNullOrEmpty(zone))
                            {
                                newProperty.Address.StreetName = zone;
                            }
                            Thread.Sleep(LevelThreadSleep(1));
                            IWebElement caracteristici = driver.FindElement(By.ClassName("properties"));
                            try
                            {
                                Thread.Sleep(LevelThreadSleep(2));
                                List<IWebElement> toateCaracteristicile =
                                    caracteristici.FindElements(By.TagName("p")).ToList();
                                Thread.Sleep(LevelThreadSleep(2));

                                var camere = toateCaracteristicile[0].Text.Split(' ');
                                if (camere[camere.Length - 1] == "Garsoniera")
                                {
                                    newProperty.PropertyTypeId =
                                        propertyTypes.Find(pt => pt.Name == "Garsoniera").Id;
                                    newProperty.Feature.NumberOfRooms = 0;
                                }
                                else
                                {
                                    newProperty.Feature.NumberOfRooms = Int32.Parse(camere[camere.Length - 1]);
                                }

                                var suprafataUtila = toateCaracteristicile[1].Text.Split(' ');
                                var suprafata = suprafataUtila[suprafataUtila.Length - 2];
                                newProperty.UsableSurface = Int32.Parse(suprafata);

                                var bai = toateCaracteristicile[2].Text.Split(' ');
                                newProperty.Feature.NumberOfBaths = Int32.Parse(bai[bai.Length - 1]);


                                var index = 3;
                                var balcon = toateCaracteristicile[3].Text.Split(' ');
                                if (balcon[1].Contains("balcoane"))
                                {
                                    newProperty.Feature.HasBalcony = true;
                                    newProperty.Feature.NumberOfBalconies =
                                        Int32.Parse(balcon[balcon.Length - 1]);
                                    index++;
                                }
                                else
                                {
                                    newProperty.Feature.HasBalcony = false;
                                    newProperty.Feature.NumberOfBalconies = 0;
                                }

                                var floor = toateCaracteristicile[index].Text.Split(' ');
                                newProperty.Address.Floor = floor[floor.Length - 2] + floor[floor.Length - 1];
                                index++;

                                var parcare = toateCaracteristicile[index].Text.Split(' ');
                                if (parcare[2].Contains("parcare"))
                                {
                                    newProperty.Feature.HasParking = true;
                                    newProperty.Feature.NumberOfParkingSpots = Int32.Parse(parcare[3]);
                                    index++;
                                }
                                else
                                {
                                    newProperty.Feature.HasParking = false;
                                    newProperty.Feature.NumberOfParkingSpots = 0;
                                }

                                var partition = toateCaracteristicile[index].Text.Split(' ');
                                var partitioning = partitionings.Find(p =>
                                    p.Name == partition[partition.Length - 1]);
                                if (partitioning != null)
                                {
                                    newProperty.Feature.PartitioningId = partitioning.Id;
                                }
                                else
                                {
                                    newProperty.Feature.PartitioningId = partitionings[0].Id;
                                }

                                index++;
                                var buildingSeniority = toateCaracteristicile[index].Text.Split(' ');
                                newProperty.Feature.BuildingSeniority =
                                    buildingSeniority[buildingSeniority.Length - 1];

                                index += 2;
                                var availableTime = toateCaracteristicile[index].Text.Split(':');
                                newProperty.Feature.AvailableTime = availableTime[availableTime.Length - 1];

                                index++;
                                var buildingType = toateCaracteristicile[index].Text.Split(':');
                                newProperty.Feature.BuildingType = buildingType[buildingType.Length - 1];

                                index++;
                                var endowment = toateCaracteristicile[index].Text.Split(':');
                                newProperty.Feature.Endowment = endowment[endowment.Length - 1];

                                index++;
                                var finish = toateCaracteristicile[index].Text.Split(':');
                                newProperty.Feature.Finish = finish[finish.Length - 1];

                                var altele = driver.FindElements(By.ClassName("dropdown__container"));
                                altele[0].Click();
                                newProperty.Description =
                                    altele[0].FindElement(By.TagName("p")).Text;



                                //verify to update
                                var propertyDto = _mapper.Map<Property, PropertyDto>(property);
                                var addressDto = propertyDto.Address;
                                var featureDto = propertyDto.Feature;

                                if (!CompareAddresses(addressDto, newProperty.Address))
                                {
                                    var addressUpdatedDto = UpdateAdress(addressDto, newProperty.Address);
                                    var addressUpdated = _mapper.Map<AddressDto, Address>(addressUpdatedDto);
                                    await _addressManager.UpdateAddress(addressUpdated);
                                }

                                if (!CompareFeatures(featureDto, newProperty.Feature))
                                {
                                    var featureUpdatedDto = UpdateFeature(featureDto, newProperty.Feature);
                                    var featureUpdated = _mapper.Map<FeatureDto, Feature>(featureUpdatedDto);
                                    await _featureManager.UpdateFeature(featureUpdated);
                                }

                                if (!CompareProperties(propertyDto, newProperty))
                                {
                                    var p1 = await _propertyManager.GetPropertyByIdNoInclude(propertyDto.Id);
                                    p1.Description = newProperty.Description;
                                    if (!dontSetPrice)
                                    {
                                        p1.Price = newProperty.Price;
                                    }
                                    p1.Surface = newProperty.Surface;
                                    p1.UsableSurface = newProperty.UsableSurface;
                                    await _propertyManager.UpdateProperty(p1);
                                }
                                var prop = await _propertyManager.GetPropertyByIdNoInclude(propertyDto.Id);
                            
                                var predictedPrice = await PredictPrice(property);
                                property.PredictedPrice = predictedPrice;
                                await _propertyManager.UpdateProperty(property);
                            }
                            catch (Exception e)
                            {
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
                catch (Exception e)
                {

                }
            }
        }
        private bool IsAlertPresent(IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private int LevelThreadSleep(int level)
        {
            var random = new Random();
            if (level == 1)
            {
                return random.Next(100, 300);
            }
            if (level == 2)
            {
                return random.Next(200, 400);
            }
            if (level == 3)
            {
                return random.Next(500, 700);
            }
            if (level == 4)
            {
                return random.Next(1000, 1200);
            }
            return random.Next(2000, 2200);
        }
        private async Task<string> PredictPrice(Property property)
        {
            PropertyCsv model = _mapper.Map<Property, PropertyCsv>(property);
            model.Floor = property.Address.Floor.Split('/')[0];
            model.MaxFloor = property.Address.Floor.Split('/')[1];
            if (!string.IsNullOrEmpty(model.Neighborhood))
            {
                if (model.Neighborhood.Contains("Cartier:"))
                {
                    var indexSubstring = model.Neighborhood.IndexOf("Cartier:");
                    model.Neighborhood = model.Neighborhood.Substring(indexSubstring + 8);
                }

                if (model.Neighborhood.Contains(','))
                {
                    if (model.Neighborhood.Contains("Zona"))
                    {
                        model.Neighborhood = property.Address.StreetName.Split(',')[1];
                        model.Zone = property.Address.StreetName.Split(',')[0];
                        var indexSubstring = model.Zone.IndexOf("Zona ");
                        model.Neighborhood = model.Zone.Substring(indexSubstring + 5);

                    }
                    else
                    {
                        model.Neighborhood = property.Address.StreetName.Split(',')[0];
                        model.Zone = property.Address.StreetName.Split(',')[1];
                        if (model.Zone.Contains("zona "))
                        {
                            var indexSubstring = model.Zone.IndexOf("zona ");
                            model.Zone = model.Zone.Substring(indexSubstring + 5);
                        }
                        else
                        {
                            model.Zone = "Necunoscuta";
                        }
                    }

                    if (string.IsNullOrEmpty(model.Zone))
                    {
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
            catch (Exception e)
            {
            }

            var predictedPrice = await _mlManager.PredictPrice(model);
            return predictedPrice.ScoredLabels;
        }
    }
   
}
