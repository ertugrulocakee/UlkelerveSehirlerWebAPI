using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class City
    {

        [Key]
        public int Id { get; set; } 

        public string name { get; set; }        

        public int population { get; set; } 

        public string imageUrl { get; set; }

        public int CountryID { get; set; }  

        public Country Country { get; set; }    


    }
}
