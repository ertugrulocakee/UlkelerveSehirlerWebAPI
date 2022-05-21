using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Country
    {

        [Key]
        public int CountryID { get; set; }

        public string name { get; set; }

        public int population { get; set; }

        public  string flagImageUrl {get;set;}

        public string capital { get; set; }

        [JsonIgnore]
        public List<City> Cities { get; set; }  


    }
}
