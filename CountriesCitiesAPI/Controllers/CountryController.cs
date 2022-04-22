using BLL.Concrete;
using DAL.EF;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CountriesCitiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {

        CountryManager countryManager = new CountryManager(new EFCountryDAL());
      
        [HttpGet]
        public IActionResult CountryList()
        {

            var values = countryManager.TGetAll();


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
        public IActionResult Country(int id)
        {

         
            var value = countryManager.TGetById(id);    

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
        public IActionResult CountryAdd(Country country)
        {
            
            countryManager.TAdd(country);

            return Created("", country);

        }


        [HttpDelete]
        public IActionResult DeleteCountry(int id)
        {

            var value = countryManager.TGetById(id);

            if (value == null)
            {

                return NotFound();

            }
            else
            {

               countryManager.TRemove(value);
                return NoContent();

            }

        }

        [HttpPut]
        public IActionResult UpdateCountry(Country country)
        {

            var value = countryManager.TGetById(country.CountryID);

            if (value == null)
            {

                return NotFound();

            }
            else
            {

                value.name = country.name;
                value.population = country.population;
                value.flagImageUrl = country.flagImageUrl;
                value.capital = country.capital;

                countryManager.TUpdate(value);  
                return NoContent();


            }

        }


    }
}
