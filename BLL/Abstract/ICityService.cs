using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ICityService : IGenericService<City>
    {

        List<City> GetCityWithCountry();    

    }
}
