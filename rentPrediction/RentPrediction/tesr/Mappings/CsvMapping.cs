using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using RentPrediction.BEModels.DTOs.Csv;
using RentPrediction.Data.Entities;

namespace RentPrediction.BEModels.Mappings
{
    public class CsvMapping : Profile
    {
        public CsvMapping()
        {
            CreateMap<Property, PropertyCsv>()
                .ForMember(dest => dest.PropertyType,
                    opt => opt.MapFrom(src => src.PropertyType.Name))

                .ForMember(dest => dest.Neighborhood,
                    opt => opt.MapFrom(src => src.Address.StreetName))
                //.ForMember(dest => dest.Country,
                //    opt => opt.MapFrom(src => src.Address.Country))
                //.ForMember(dest => dest.County,
          //  opt => opt.MapFrom(src => src.Address.County))
                .ForMember(dest => dest.Floor,
                    opt => opt.MapFrom(src => src.Address.Floor))

                .ForMember(dest => dest.HasBalcony,
                    opt => opt.MapFrom(src => src.Feature.HasBalcony))
                .ForMember(dest => dest.HasParking,
                    opt => opt.MapFrom(src => src.Feature.HasParking))
                .ForMember(dest => dest.HasGarden,
                    opt => opt.MapFrom(src => src.Feature.HasGarden))
                .ForMember(dest => dest.HasHeating,
                    opt => opt.MapFrom(src => src.Feature.HasHeating))
                .ForMember(dest => dest.NumberOfRooms,
                    opt => opt.MapFrom(src => src.Feature.NumberOfRooms))
                .ForMember(dest => dest.NumberOfBaths,
                    opt => opt.MapFrom(src => src.Feature.NumberOfBaths))
                .ForMember(dest => dest.NumberOfBalconies,
                    opt => opt.MapFrom(src => src.Feature.NumberOfBalconies))
                //.ForMember(dest => dest.IsForSale,
                //    opt => opt.MapFrom(src => src.Feature.IsForSale))
                .ForMember(dest => dest.NumberOfParkingSpots,
                    opt => opt.MapFrom(src => src.Feature.NumberOfParkingSpots))
                .ForMember(dest => dest.BuildingSeniority,
                    opt => opt.MapFrom(src => src.Feature.BuildingSeniority))
                .ForMember(dest => dest.BuildingType,
                    opt => opt.MapFrom(src => src.Feature.BuildingType))
                .ForMember(dest => dest.Endowment,
                    opt => opt.MapFrom(src => src.Feature.Endowment))
                .ForMember(dest => dest.Finish,
                    opt => opt.MapFrom(src => src.Feature.Finish))


                .ForMember(dest => dest.Partitioning,
                    opt => opt.MapFrom(src => src.Feature.Partitioning.Name));
            ;
        }
    }
}
