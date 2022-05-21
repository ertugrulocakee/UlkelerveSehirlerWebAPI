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
    public class EFCityDAL : GenericRepository<City>, ICityDAL
    {
        public List<City> CityWithCountry()
        {

            using (var c = new Context())
            {

                return  c.Cities.Include(x => x.Country).ToList();

            }

        }
    }
}
