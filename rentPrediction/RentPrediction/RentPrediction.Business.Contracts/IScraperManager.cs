using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentPrediction.Business.Contracts
{
    public interface IScraperManager
    {
        Task StartScrapping(int userId);
    }
}
