using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto.AddressDto
{
    public class AddressEntityDto
    {
        public City City { get; set; }
        public District District { get; set; }
        public Commune Commune { get; set; }
    }
    public class City
    {
        public string name { get; set; }
        public string label { get; set; }
        public int city_code { get; set; }
        public int value { get; set; }
        
        public List<District> Districts { get; set; }
    }

    public class District
    {
        public string label { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public int city_code { get; set; }
        public int district_code { get; set; }
        public List<Commune> Communes { get; set; }
        

    }

    public class Commune
    {
        public string label { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public int ward_code { get; set; }
        public int district_code { get; set; }
        
    }

}
