using System;
using System.Collections.Generic;
using System.Text;

namespace RentPrediction.BEModels.DTOs.User
{
    public class UserResetPasswordDto
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
