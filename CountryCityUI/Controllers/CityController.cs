using BLL.Concrete;
using CountryCityUI.Models;
using DAL.EF;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CountryCityUI.Controllers
{
    public class CityController : Controller
    {

        CountryManager countryManager = new CountryManager(new EFCountryDAL());
        CityManager cityManager = new CityManager(new EFCityDAL());
            public async Task<IActionResult> Index()
            {

                var httpClient = new HttpClient();
                var responseMessage = await httpClient.GetAsync("https://localhost:44312/api/City");
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<City>>(jsonString);

                return View(values);
            }

        [HttpGet]
        public IActionResult AddCity()
        {

            GetCountries();
            return View();


        }

        [HttpPost]
        public async Task<IActionResult> AddCity(CityViewModel cityViewModel)
        {

            try
            {
                var country = countryManager.TGetAll().Where(m=>m.CountryID == cityViewModel.CountryID).FirstOrDefault();   
            }
            catch
            {

                ViewBag.Message = "Ulke Secilmelidir!";

                GetCountries();

                return View();

            }


            string[] validFileTypes = { "gif", "jpg", "png" };
            bool isValidType = false;


            if (ModelState.IsValid)
            {

                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(cityViewModel.image.FileName);

                for (int i = 0; i < validFileTypes.Length; i++)
                {
                    if (extension == "." + validFileTypes[i])
                    {
                        isValidType = true;
                        break;
                    }
                }

                if (!isValidType)
                {
                    ViewBag.Message = "Lutfen png,jpg ve gif dosyasi yukleyin!";
                    GetCountries();
                    return View();
                }

                var imagename = Guid.NewGuid() + extension;
                var saveLocation = resource + "/wwwroot/cityimage/" + imagename;
                var stream = new FileStream(saveLocation, FileMode.Create);
                await cityViewModel.image.CopyToAsync(stream);


                City city = new City();

                city.name = cityViewModel.name; 
                city.population = cityViewModel.population;
                city.imageUrl = imagename;
             
                city.CountryID = cityViewModel.CountryID;   

                var httpClient = new HttpClient();
                var jsonCity = JsonConvert.SerializeObject(city);
                StringContent content = new StringContent(jsonCity, Encoding.UTF8, "application/json");
                var responseMessage = await httpClient.PostAsync("https://localhost:44312/api/City", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    
                    return RedirectToAction("Index", "City");

                }

                GetCountries();
                return View(city);

            }
            else
            {

                GetCountries();

                return View();

            }

        }


       

        [HttpGet]
        public async Task<IActionResult> UpdateCity(int id)
        {


            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:44312/api/City/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {

                var cityJson = await responseMessage.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<City>(cityJson);

                CityEditViewModel viewModel = new CityEditViewModel();
                viewModel.Id = values.Id;
                viewModel.population = values.population;
                viewModel.name = values.name;
                viewModel.CountryID = values.CountryID; 
                
                GetCountries();

                return View(viewModel);

            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> UpdateCity([FromForm] CityEditViewModel cityEditViewModel)
        {

            try
            {
                var country = countryManager.TGetAll().Where(m => m.CountryID == cityEditViewModel.CountryID).FirstOrDefault();

            }
            catch
            {

                ViewBag.Message = "Ulke Secilmelidir!";

                GetCountries();

                return View();

            }


            string[] validFileTypes = { "gif", "jpg", "png" };
            bool isValidType = false;


            if (ModelState.IsValid)
            {


                City city = new City();
                city.Id = cityEditViewModel.Id; 
                city.name = cityEditViewModel.name;
                city.population = cityEditViewModel.population;
                city.CountryID = cityEditViewModel.CountryID;
                city.imageUrl = cityManager.TGetById(cityEditViewModel.Id).imageUrl;



                if (cityEditViewModel.image != null)
                {

                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(cityEditViewModel.image.FileName);

                    for (int i = 0; i < validFileTypes.Length; i++)
                    {
                        if (extension == "." + validFileTypes[i])
                        {
                            isValidType = true;
                            break;
                        }
                    }

                    if (!isValidType)
                    {
                        ViewBag.Message = "Lutfen png,jpg ve gif dosyasi yukleyin!";
                        GetCountries();
                        return View();
                    }

                    var imagename = Guid.NewGuid() + extension;
                    var saveLocation = resource + "/wwwroot/cityimage/" + imagename;
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    await cityEditViewModel.image.CopyToAsync(stream);

                    city.imageUrl = imagename;
                    
                }

                var httpClient = new HttpClient();
                var jsonCity = JsonConvert.SerializeObject(city);
                var content = new StringContent(jsonCity, Encoding.UTF8, "application/json");
                var responseMessage = await httpClient.PutAsync("https://localhost:44312/api/City", content);
               
                if (responseMessage.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }

                GetCountries();
                return View(city);

            }
            else
            {

                GetCountries();
                return View();

            }

        }


        public async Task<IActionResult> DeleteCity(int id)
        {

            var httpClient = new HttpClient();
            var responseMessage = await httpClient.DeleteAsync("https://localhost:44312/api/City?id=" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }

            return View();

        }


        public IActionResult CityDetail(int id)
        {

            var value = cityManager.GetCityWithCountry().Where(m=>m.Id == id).FirstOrDefault();

            return View(value);


        }


        protected void GetCountries()
        {

            var countriesList = (from i in countryManager.TGetAll().ToList()
                                 select new SelectListItem()
                                 {

                                     Text = i.name,
                                     Value = i.CountryID.ToString()

                                 }).ToList();

            countriesList.Insert(0, new SelectListItem()
            {
                Text = "---Ülke Seçin---",
                Value = String.Empty

            });

            ViewBag.Countries = countriesList;


        }


    }
}
