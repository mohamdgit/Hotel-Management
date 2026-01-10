using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.BaseEntity
{
    public class BaseEntity<Key>
    {
        public Key Id { get; set; }
    }
}
