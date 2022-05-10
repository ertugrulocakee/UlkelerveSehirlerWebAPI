using BLL.Abstract;
using DAL.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class CountryManager : ICountryService
    {

        ICountryDAL countryDAL;

        public CountryManager(ICountryDAL countryDAL)
        {
            this.countryDAL = countryDAL;
        }

        public void TAdd(Country t)
        {
            countryDAL.Add(t);
        }

        
        public List<Country> TGetAll()
        {
            return countryDAL.GetAll(); 
        }

        public List<Country> TGetAllCities()
        {
            return countryDAL.GetAllCity();
        }

        public Country TGetById(int id)
        {
            return countryDAL.GetT(id);
        }

        public List<Country> TGetListbyFilter()
        {
            throw new NotImplementedException();
        }

        public void TRemove(Country t)
        {
            countryDAL.Remove(t);
        }

     
        public void TUpdate(Country t)
        {
            countryDAL.Update(t);
        }

    
    }
}
