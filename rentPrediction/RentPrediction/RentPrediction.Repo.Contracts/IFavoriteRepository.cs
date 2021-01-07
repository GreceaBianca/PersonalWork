using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.Data.Entities;

namespace RentPrediction.Repo.Contracts
{
    public interface IFavoriteRepository
    {
        IQueryable<Favorite> GetAllFavorites();
        Task<Favorite> GetFavoriteById(int id);
        Task<Favorite> AddFavorite(Favorite newFavorite);
        Task<Favorite> UpdateFavorite(Favorite favorite);
        Task DeleteFavorite(int id);
    }
}
