using Hotel_Management.DOMAIN.Models.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Contracts.Ispecification
{
    public interface ISpecification<entity,key> where entity : BaseEntity<key>
    {
        public Expression<Func<entity,bool>> Creteria { get;  }
        public Expression<Func<entity, object>> OrderByAsc { get;  }
        public Expression<Func<entity, object>> OrderByDesc { get;  }

        public List<Expression<Func<entity, object>>> includes { get; }
  


    }
}
