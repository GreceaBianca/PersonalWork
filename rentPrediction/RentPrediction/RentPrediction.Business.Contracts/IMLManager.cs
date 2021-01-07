using RentPrediction.BEModels.DTOs.Csv;
using System.Threading.Tasks;

namespace RentPrediction.Business.Contracts
{
    public interface IMLManager
    {
        Task<OutputModel> PredictPrice(PropertyCsv input);
    }
}
