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
    public class GalleryRepository : IGalleryRepository
    {
        private readonly DbContext _context;
        public GalleryRepository(DbContext context)
        {
            _context = context;
        }
        public IQueryable<Gallery> GetAllGalleries()
        {
            return _context.Galleries;
        }

        public async Task<Gallery> GetGalleryById(int id)
        {
            return await _context.Galleries.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Gallery> AddGallery(Gallery newGallery)
        {
            var role = await GetGalleryById(newGallery.Id);
            if (role != null)
            {
                throw new Exception("Gallery already exists!");
            }
            _context.Galleries.Add(newGallery);
            await _context.SaveChangesAsync();
            return newGallery;
        }
        public async Task<Gallery> UpdateGallery(Gallery role)
        {
            var oldGallery = await GetGalleryById(role.Id);
            if (oldGallery == null || oldGallery.IsArchived)
            {
                throw new Exception("Gallery can not be found");
            }
            _context.Galleries.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task DeleteGallery(int id)
        {
            var role = await GetGalleryById(id);
            if (role == null || role.IsArchived)
            {
                throw new Exception("Gallery can not be found");
            }
            role.IsArchived = true;
            role.ArchivedDate = DateTime.UtcNow;
            await UpdateGallery(role);
            return;
        }
    }
}
