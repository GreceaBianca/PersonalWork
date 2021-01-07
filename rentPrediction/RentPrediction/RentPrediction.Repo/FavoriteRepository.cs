using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentPrediction.Data.Entities;
using RentPrediction.Repo.Contracts;
using DbContext = RentPrediction.Data.DbContext;

namespace RentPrediction.Repo
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly DbContext _context;
        public FavoriteRepository(DbContext context)
        {
            _context = context;
        }
        public IQueryable<Favorite> GetAllFavorites()
        {
            return _context.Favorites;
        }

        public async Task<Favorite> GetFavoriteById(int id)
        {
            return await _context.Favorites.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Favorite> AddFavorite(Favorite newFavorite)
        {
            var favorite = await GetFavoriteById(newFavorite.Id);
            if (favorite != null)
            {
                throw new Exception("Favorite already exists!");
            }
            _context.Favorites.Add(newFavorite);
            await _context.SaveChangesAsync();
            return newFavorite;
        }
        public async Task<Favorite> UpdateFavorite(Favorite favorite)
        {
            var oldFavorite = await GetFavoriteById(favorite.Id);
            if (oldFavorite == null || oldFavorite.IsArchived)
            {
                throw new Exception("Favorite can not be found");
            }
            _context.Favorites.Update(favorite);
            await _context.SaveChangesAsync();
            return favorite;
        }

        public async Task DeleteFavorite(int id)
        {
            var favorite = await GetFavoriteById(id);
            if (favorite == null || favorite.IsArchived)
            {
                throw new Exception("Favorite can not be found");
            }
            favorite.IsArchived = true;
            favorite.ArchivedDate = DateTime.UtcNow;
            await UpdateFavorite(favorite);
            return;
        }
    }
}
