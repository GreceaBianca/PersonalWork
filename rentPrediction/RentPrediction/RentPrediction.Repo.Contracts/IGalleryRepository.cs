using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.Data.Entities;

namespace RentPrediction.Repo.Contracts
{
    public interface IGalleryRepository
    {
        IQueryable<Gallery> GetAllGalleries();
        Task<Gallery> GetGalleryById(int id);
        Task<Gallery> AddGallery(Gallery newGallery);
        Task<Gallery> UpdateGallery(Gallery gallery);
        Task DeleteGallery(int id);
    }
}
