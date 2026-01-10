using E_Commers.Domain.Contracts.Reposatory.IGenericRepo;
using Hotel_Management.DOMAIN.Contexts.HotelContext;
using Hotel_Management.DOMAIN.Contracts.Ispecification;
using Hotel_Management.DOMAIN.Models.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Persistance.GenericReposatory
{
    public class GenaricRepo<T, key> : IGenericReposatory<T, key> where T : BaseEntity<key>
    {
        private readonly HotelContext _context;

        public GenaricRepo(HotelContext Context)
        {
            _context = Context;
        }
        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            
        }

        public void Delete(key id)
        {
            var entity =  _context.Set<T>().Find(id);
            if(entity is not null)
            {
                _context.Set<T>().Remove(entity);
            }
           
           
           
        }
        public void Deletephoto(T entity)
        {
            _context.Set<T>().Remove(entity);
        }


        public IQueryable<T> GetAllAsync()
        {
            var res = _context.Set<T>();
            ;
            return res;
        }
        public async Task<T?> GetById(key  Id)
        {
            var entity = await _context.Set<T>().FindAsync(Id);
            if (entity is null)
            {
                throw new Exception("not exist") ;
            }
            return entity;
        }
        public void Update(T new_T)
        {
            
                _context.Set<T>().Update(new_T);
            
           
        }
        public  IEnumerable<T> GetAllSpecificationAsync(ISpecification<T, key> spec)
        {
            var query = _context.Set<T>();
            var entities =    GenerateQyery.GenerateQyery.GenerateQueries(query, spec);
            return entities.ToList(); 
        }

       
    }
}
