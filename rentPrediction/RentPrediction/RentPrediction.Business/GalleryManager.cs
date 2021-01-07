using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;
using RentPrediction.Repo.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentPrediction.Business
{
    public class GalleryManager : IGalleryManager
    {
        private readonly IGalleryRepository _galleryRepository;
        public GalleryManager( IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }
        public IList<Gallery> GetAllGalleries()
        {
            return _galleryRepository.GetAllGalleries().ToList();
        }

        public async Task<Gallery> GetGalleryById(int id)
        {
            return await _galleryRepository.GetGalleryById(id);
        }

        public async Task<Gallery> AddGallery(Gallery newGallery)
        {
            return await _galleryRepository.AddGallery(newGallery);
        }

        public async Task<Gallery> UpdateGallery(Gallery gallery)
        {
            return await _galleryRepository.UpdateGallery(gallery);
        }

        public Task DeleteGallery(int id)
        {
            return _galleryRepository.DeleteGallery(id);
        }
    }
}
