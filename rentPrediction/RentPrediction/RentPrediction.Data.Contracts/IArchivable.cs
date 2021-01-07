using System;

namespace RentPrediction.Data.Contracts
{
    public class IArchivable
    {
        public bool IsArchived { get; set; }
        public DateTime? ArchivedDate { get; set; }
    }
}
