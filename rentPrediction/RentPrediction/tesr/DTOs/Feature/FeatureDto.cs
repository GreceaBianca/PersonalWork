using RentPrediction.Data.Entities;

namespace RentPrediction.BEModels.DTOs.Feature
{
    public class FeatureDto
    {
        public int Id { get; set; }
        public bool HasBalcony { get; set; }

        public bool HasParking { get; set; }

        public bool HasGarden { get; set; }
        public bool HasHeating { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBaths { get; set; }
        public int NumberOfBalconies { get; set; }
        public int NumberOfParkingSpots { get; set; }
        public string BuildingSeniority { get; set; }
        public string BuildingType { get; set; }
        public string Endowment { get; set; }
        public string Finish { get; set; }
        public string AvailableTime { get; set; }
        public int PropertyId { get; set; }
        public Partitioning Partitioning { get; set; }
    }
}
