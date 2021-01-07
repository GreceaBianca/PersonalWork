using System.Collections.Generic;
using RentPrediction.BEModels.DTOs.Favorite;
using RentPrediction.BEModels.DTOs.Role;

namespace RentPrediction.BEModels.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        public int RoleId { get; set; }
        public RoleDto UserRole { get; set; }
        public IList<FavoriteDto> Favorites { get; set; }
    }
}
