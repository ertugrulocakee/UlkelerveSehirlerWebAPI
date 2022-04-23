using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CountryCityUI.Models
{
    public class CityViewModel
    {

    
        [Required(ErrorMessage="Şehir adı boş olamaz!")]
        [MaxLength(40,ErrorMessage ="Şehir adı maksimum 40 karakterden oluşabilir!")]
        public string name { get; set; }

        [Range(1, 10000000000,ErrorMessage ="Nüfus 1 ile 10 milyar arasında olabilir!")]
        public int population { get; set; }

        [Required(ErrorMessage ="Resim Seçilmelidir!")]
        public IFormFile image { get; set; }

        public int CountryID { get; set; }


    }
}
