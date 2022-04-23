using Microsoft.AspNetCore.Http;

namespace CountryCityUI.Models
{
    public class CountryViewModel
    {

        public int CountryID { get; set; }

        public string name { get; set; }

        public int population { get; set; }

        public IFormFile flagImage { get; set; }

        public string capital { get; set; }


    }
}
