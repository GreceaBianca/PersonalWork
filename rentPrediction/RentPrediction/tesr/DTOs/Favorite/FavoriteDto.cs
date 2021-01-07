

using RentPrediction.BEModels.DTOs.Property;

namespace RentPrediction.BEModels.DTOs.Favorite
{
    public class FavoriteDto
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public PropertyDto Property { get; set; }
    }
}
