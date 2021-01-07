using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.Data.Entities;

namespace RentPrediction.Business.Contracts
{
    public interface IGalleryManager
    {
        IList<Gallery> GetAllGalleries();
        Task<Gallery> GetGalleryById(int id);
        Task<Gallery> AddGallery(Gallery newGallery);
        Task<Gallery> UpdateGallery(Gallery gallery);
        Task DeleteGallery(int id);
    }
}
