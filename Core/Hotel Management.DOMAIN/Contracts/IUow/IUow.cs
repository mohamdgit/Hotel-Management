using E_Commers.Domain.Contracts.Reposatory.IGenericRepo;
using Hotel_Management.DOMAIN.Models.BaseEntity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Contracts.IUow
{
    public interface IUow
    {
        public IGenericReposatory<T, key> GenerateRepo<T, key>() where T : BaseEntity<key>;
        public Task<int> SaveChanges();
        Task<IDbContextTransaction> BeginTransactionAsync(
               IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}
