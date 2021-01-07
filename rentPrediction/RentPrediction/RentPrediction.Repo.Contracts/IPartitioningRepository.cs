using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.Data.Entities;

namespace RentPrediction.Repo.Contracts
{
    public interface IPartitioningRepository
    {
        IQueryable<Partitioning> GetAllPartitionings();
        Task<Partitioning> GetPartitioningById(int id);
        Task<Partitioning> AddPartitioning(Partitioning newPartitioning);
        Task<Partitioning> UpdatePartitioning(Partitioning address);
        Task DeletePartitioning(int id);
    }
}
