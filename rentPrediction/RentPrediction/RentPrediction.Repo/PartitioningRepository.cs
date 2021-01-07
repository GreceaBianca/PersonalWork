using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.Data;
using Microsoft.EntityFrameworkCore;
using RentPrediction.Data.Entities;
using RentPrediction.Repo.Contracts;
using DbContext = RentPrediction.Data.DbContext;

namespace RentPrediction.Repo
{
    public class PartitioningRepository:IPartitioningRepository
    {
        private readonly DbContext _context;
        public PartitioningRepository(DbContext context)
        {
            _context = context;
        }
        public  IQueryable<Partitioning> GetAllPartitionings()
        {
            return _context.Partitionings.AsNoTracking();
        }

        public async Task<Partitioning> GetPartitioningById(int id)
        {
           return await _context.Partitionings.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Partitioning> AddPartitioning(Partitioning newPartitioning)
        {
            var Partitioning = await GetPartitioningById(newPartitioning.Id);
            if (Partitioning != null)
            {
                throw new Exception("Partitioning already exists!");
            }
            _context.Partitionings.Add(newPartitioning);
            await _context.SaveChangesAsync();
            return newPartitioning;
        }
        public async Task<Partitioning> UpdatePartitioning(Partitioning newPartitioning)
        {
            var oldPartitioning = await GetPartitioningById(newPartitioning.Id);
            if (oldPartitioning == null || oldPartitioning.IsArchived)
            {
                throw new Exception("Partitioning can not be found");
            }
            _context.Partitionings.Update(newPartitioning);
            await _context.SaveChangesAsync();
            return newPartitioning;
        }

        public async Task DeletePartitioning(int id)
        {
            var Partitioning = await GetPartitioningById(id);
            if (Partitioning == null || Partitioning.IsArchived)
            {
                throw new Exception("Partitioning can not be found");
            }
            Partitioning.IsArchived = true;
            Partitioning.ArchivedDate = DateTime.UtcNow;
            await UpdatePartitioning(Partitioning);
            return;
        }
   
    }
}
