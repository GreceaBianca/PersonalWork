using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;
using RentPrediction.Repo.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RentPrediction.Business
{
    public class FavoriteManager : IFavoriteManager
    {
        private readonly IFavoriteRepository _favoriteRepository;
        public FavoriteManager( IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }
        public IList<Favorite> GetAllFavorites()
        {
            return _favoriteRepository.GetAllFavorites().Where(f=>!f.IsArchived).ToList();
        }

        public IList<Favorite> GetAllFavoritesByUserId(int userId)
        {
            return _favoriteRepository.GetAllFavorites().Where(f => !f.IsArchived&&f.UserId==userId).Include(f=>f.Property)
                .ThenInclude(p=>p.Galleries)
                .ToList();
        }

        public async Task<Favorite> GetFavoriteById(int id)
        {
            return await _favoriteRepository.GetFavoriteById(id);
        }

        public async Task<Favorite> AddFavorite(Favorite newFavorite)
        {
            return await _favoriteRepository.AddFavorite(newFavorite);
        }

        public async Task<Favorite> UpdateFavorite(Favorite favorite)
        {
            return await _favoriteRepository.UpdateFavorite(favorite);
        }

        public Task DeleteFavorite(int id)
        {
            return _favoriteRepository.DeleteFavorite(id);
        }
    }
}
