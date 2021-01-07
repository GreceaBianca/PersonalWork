using System;
using System.Collections.Generic;
using RentPrediction.BEModels.DTOs.Address;
using RentPrediction.BEModels.DTOs.Feature;
using RentPrediction.BEModels.DTOs.Gallery;
using RentPrediction.BEModels.DTOs.PropertyType;
using RentPrediction.BEModels.DTOs.User;

namespace RentPrediction.BEModels.DTOs.Property
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string PredictedPrice { get; set; }
        public string Description { get; set; }
        public int Surface { get; set; }
        public int UsableSurface { get; set; }
        public DateTime CreationDate { get; set; }
        public string AvailableDate { get; set; }
        public string URL { get; set; }
        public AddressDto Address { get; set; }
        public PropertyTypeDto PropertyType { get; set; }
        public UserBriefDto User { get; set; }
        public FeatureDto Feature { get; set; }
        public List<GalleryDto> Images { get; set; }
    }
}
