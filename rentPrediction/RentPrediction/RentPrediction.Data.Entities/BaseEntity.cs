using System;
using System.Collections.Generic;
using System.Text;
using RentPrediction.Data.Contracts;

namespace RentPrediction.Data.Entities
{
    public class BaseEntity : IArchivable
    {
        public int Id { get; set; }
    }
}
