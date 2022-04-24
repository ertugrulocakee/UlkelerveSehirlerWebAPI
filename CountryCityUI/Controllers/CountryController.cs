using BLL.Concrete;
using CountryCityUI.Models;
using DAL.EF;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CountryCityUI.Controllers
{
    public class CountryController : Controller
    {

        CountryManager countryManager = new CountryManager(new EFCountryDAL());

        public async Task<IActionResult> Index()
        {

            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:44312/api/Country");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Country>>(jsonString);

            return View(values);

        }

        [HttpGet]
        public IActionResult AddCountry()
        {

            return View();
        }

        [HttpPost]  
        public async Task<IActionResult> AddCountry(CountryViewModel countryViewModel)
        {

            string[] validFileTypes = { "gif", "jpg", "png" };
            bool isValidType = false;


            if (ModelState.IsValid)
            {

                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(countryViewModel.flagImage.FileName);

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
                  
                    return View();
                }

                var imagename = Guid.NewGuid() + extension;
                var saveLocation = resource + "/wwwroot/countryimage/" + imagename;
                var stream = new FileStream(saveLocation, FileMode.Create);
                await countryViewModel.flagImage.CopyToAsync(stream);


                Country country = new Country();

                country.name = countryViewModel.name;
                country.population = countryViewModel.population;
                country.flagImageUrl = imagename;
                country.capital = countryViewModel.capital;
                

                var httpClient = new HttpClient();
                var jsonCountry = JsonConvert.SerializeObject(country);
                StringContent content = new StringContent(jsonCountry, Encoding.UTF8, "application/json");
                var responseMessage = await httpClient.PostAsync("https://localhost:44312/api/Country", content);

                if (responseMessage.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index", "Country");

                }

              
                return View(country);

            }
            else
            {

                return View();

            }

        }


        [HttpGet]
        public async Task<IActionResult> UpdateCountry(int id)
        {


            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:44312/api/Country/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {

                var countryJson = await responseMessage.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<Country>(countryJson);

                CountryEditViewModel viewModel = new CountryEditViewModel();
                viewModel.id = values.CountryID;
                viewModel.population = values.population;
                viewModel.name = values.name;
                viewModel.capital = values.capital;

                return View(viewModel);

            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> UpdateCountry(CountryEditViewModel countryEditViewModel)
        {
          
            string[] validFileTypes = { "gif", "jpg", "png" };
            bool isValidType = false;


            if (ModelState.IsValid)
            {


                Country country = new Country();
                country.CountryID = countryEditViewModel.id;
                country.name = countryEditViewModel.name;
                country.population = countryEditViewModel.population;
                country.capital = countryEditViewModel.capital;
                country.flagImageUrl = countryManager.TGetById(countryEditViewModel.id).flagImageUrl;



                if (countryEditViewModel.flagImage != null)
                {

                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(countryEditViewModel.flagImage.FileName);

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
                
                        return View();
                    }

                    var imagename = Guid.NewGuid() + extension;
                    var saveLocation = resource + "/wwwroot/countryimage/" + imagename;
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    await countryEditViewModel.flagImage.CopyToAsync(stream);

                    country.flagImageUrl = imagename;

                }

                var httpClient = new HttpClient();
                var jsonCountry = JsonConvert.SerializeObject(country);
                var content = new StringContent(jsonCountry, Encoding.UTF8, "application/json");
                var responseMessage = await httpClient.PutAsync("https://localhost:44312/api/Country", content);

                if (responseMessage.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }


                return View(country);

            }
            else
            {

                return View();

            }

        }




        public async Task<IActionResult> DeleteCountry(int id)
        {

            var httpClient = new HttpClient();
            var responseMessage = await httpClient.DeleteAsync("https://localhost:44312/api/Country?id=" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }

            return View();

        }

    }
}
