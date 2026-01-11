using Hotel_Management.DOMAIN.Contracts.Ispecification;
using Hotel_Management.DOMAIN.Models.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Specification
{
    public class BaseSpecification<entity, key> : ISpecification<entity, key> where entity : BaseEntity<key>
    {
        public BaseSpecification(Expression<Func<entity, bool>> Creteria)
        {
            this.Creteria = Creteria;


        }

        public Expression<Func<entity, bool>> Creteria { get; private set; }

        public Expression<Func<entity, object>> OrderByAsc { get; private set; }
        public Expression<Func<entity, object>> OrderByDesc { get; private set; }

        public List<Expression<Func<entity, object>>> includes { get; private set; } = [];
       

        public void Addincludesfunc(Expression<Func<entity, object>> Include)
        {
            includes.Add(Include);
        }
        public void OrderByAscFun(Expression<Func<entity, object>> OrderByAsc)
        {
            this.OrderByAsc = OrderByAsc;
        }
        public void OrderByDescFun(Expression<Func<entity, object>> OrderByDesc)
        {
            this.OrderByDesc = OrderByDesc;
        }
     
    }
}
