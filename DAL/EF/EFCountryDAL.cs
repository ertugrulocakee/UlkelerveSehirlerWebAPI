using DAL.Abstract;
using DAL.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class EFCountryDAL : GenericRepository<Country>,ICountryDAL
    {
    }
}
