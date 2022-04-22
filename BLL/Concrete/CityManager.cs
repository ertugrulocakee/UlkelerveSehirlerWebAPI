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
    public class CityManager : ICityService
    {

        ICityDAL cityDAL;

        public CityManager(ICityDAL cityDAL)
        {
            this.cityDAL = cityDAL;
        }

        public List<City> GetCityWithCountry()
        {
            return cityDAL.CityWithCountry().ToList();
        }

        public void TAdd(City t){
              
            cityDAL.Add(t);
        }
     

        public List<City> TGetAll()
        {
            return cityDAL.GetAll();    
        }

        public City TGetById(int id)
        {
           return cityDAL.GetT(id);
        }

        public List<City> TGetListbyFilter()
        {
            throw new NotImplementedException();
        }

        public void TRemove(City t)
        {
            cityDAL.Remove(t);
        }
 
        public void TUpdate(City t)
        {
            cityDAL.Update(t);
        }
    }
}
