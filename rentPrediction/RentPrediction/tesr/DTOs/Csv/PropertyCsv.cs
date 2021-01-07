namespace RentPrediction.BEModels.DTOs.Csv
{
    public class PropertyCsv
    {
        public string Price { get; set; }
        public int UsableSurface { get; set; }

        public string PropertyType { get; set; }

        public string Neighborhood { get; set; }
        public string Zone { get; set; }
        public string Floor { get; set; }
        public string MaxFloor { get; set; }
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
        public string Partitioning { get; set; }
    }
}
