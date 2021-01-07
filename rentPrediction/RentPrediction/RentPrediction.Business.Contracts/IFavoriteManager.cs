using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.Data.Entities;

namespace RentPrediction.Business.Contracts
{
    public interface IFavoriteManager
    {
        IList<Favorite> GetAllFavorites();
        IList<Favorite> GetAllFavoritesByUserId(int userId);
        Task<Favorite> GetFavoriteById(int id);
        Task<Favorite> AddFavorite(Favorite newFavorite);
        Task<Favorite> UpdateFavorite(Favorite favorite);
        Task DeleteFavorite(int id);
    }
}
