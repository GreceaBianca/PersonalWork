namespace RentPrediction.BEModels.DTOs.Address
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string Floor { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int PropertyId { get; set; }
    }
}
