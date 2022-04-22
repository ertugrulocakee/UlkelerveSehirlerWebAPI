using BLL.Concrete;
using DAL.EF;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CountriesCitiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {

        CityManager cityManager = new CityManager(new EFCityDAL());

        [HttpGet]
        public IActionResult CityList()
        {

            var values = cityManager.TGetAll();


            if (values == null)
            {

                return NotFound();
            }
            else
            {

                return Ok(values);

            }

        }


        [HttpGet("{id}")]
        public IActionResult City(int id)
        {


            var value = cityManager.TGetById(id);

            if (value == null)
            {

                return NotFound();
            }
            else
            {

                return Ok(value);

            }

        }


        [HttpPost]
        public IActionResult CityAdd(City city)
        {

            cityManager.TAdd(city);

            return Created("", city);

        }


        [HttpDelete]
        public IActionResult DeleteCity(int id)
        {

            var value = cityManager.TGetById(id);

            if (value == null)
            {

                return NotFound();

            }
            else
            {

                cityManager.TRemove(value);
                return NoContent();

            }

        }

        [HttpPut]
        public IActionResult UpdateCity(City city)
        {

            var value = cityManager.TGetById(city.Id);

            if (value == null)
            {

                return NotFound();

            }
            else
            {

                value.name = city.name;
                value.population = city.population;
                value.imageUrl = city.imageUrl;
                value.CountryID = city.CountryID;

                cityManager.TUpdate(value);
                return NoContent();


            }

        }
    }
}
