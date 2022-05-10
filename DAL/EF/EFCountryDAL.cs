using DAL.Abstract;
using DAL.Concrete;
using DAL.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class EFCountryDAL : GenericRepository<Country>, ICountryDAL
    {
        public List<Country> GetAllCity()
        {
            using (var c = new Context())
            {

                return c.Countries.Include(x => x.Cities).ToList();

            }
        }
    }
}
