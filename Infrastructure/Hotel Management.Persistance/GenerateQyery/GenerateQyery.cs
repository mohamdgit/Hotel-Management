using Hotel_Management.DOMAIN.Contracts.Ispecification;
using Hotel_Management.DOMAIN.Models.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Persistance.GenerateQyery
{
    public static class GenerateQyery
    {
        public static IQueryable<Entity> GenerateQueries<Entity, key> (IQueryable<Entity> basequery,ISpecification<Entity, key> spec )
            where Entity:BaseEntity<key>
        {
            var query = basequery;
            if(spec.Creteria is not null)
            {
                query = query.Where(spec.Creteria);
            }
            if (spec.OrderByAsc is not null)
            {
                query = query.OrderBy(spec.OrderByAsc);
            }
            if (spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }
            if (spec.includes is not null&& spec.includes.Any())
            {
                query = spec.includes.Aggregate(query,(newquery,expression)=> newquery.Include(expression));
            }
            return query;
        }
    }
}
