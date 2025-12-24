
using Hotel_Management.DOMAIN.Contracts.Ispecification;
using Hotel_Management.DOMAIN.Models.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commers.Domain.Contracts.Reposatory.IGenericRepo
{
    public interface IGenericReposatory<T,key> where T : BaseEntity<key>
    {
        Task<T> GetById(key Id);
       IEnumerable<T> GetAllSpecificationAsync(ISpecification<T,key> spec);
        public IQueryable<T> GetAllAsync();
        Task Add(T entity);
        void Update( T new_T);
        void Delete(key id);
        public void Deletephoto(T entity);

    }
}
