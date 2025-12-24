using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Contracts.Seeding
{
    public interface ISeeding
    {
        public Task SeedUserRoles();
    }
}
