using E_Commers.Domain.Contracts.Reposatory.IGenericRepo;
using Hotel_Management.DOMAIN.Contexts.HotelContext;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.BaseEntity;
using Hotel_Management.Persistance.GenericReposatory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Persistance.UOW
{
    public class UOW : IUow
    {
        private readonly HotelContext context;

        public  UOW(HotelContext context)
        {
            this.context = context;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel )
        {
            isolationLevel = IsolationLevel.Serializable;
            return await context.Database.BeginTransactionAsync(isolationLevel);
        }

        public IGenericReposatory<T, key> GenerateRepo<T, key>() where T : BaseEntity<key>
        {
            Dictionary<Type, object> repos = [];
            var entity = typeof(T);
            if (repos.ContainsKey(entity))
            {
                return (IGenericReposatory<T, key>)repos[entity];
            }
            else{
                var repo_of_entity = new GenericReposatory.GenaricRepo<T, key>(context);
                repos.Add(entity, repo_of_entity);
                return repo_of_entity;
                ;
            }

        }

        public async Task<int> SaveChanges()
        {
            return await context.SaveChangesAsync();
        }
    }
}
