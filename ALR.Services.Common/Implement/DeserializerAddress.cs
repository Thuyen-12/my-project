using ALR.Data.Dto.AddressDto;
using ALR.Services.Common.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.Common.Implement
{
    public class DeserializerAddress : IDeserializerAddress
    {
        public async Task<List<City>> ReadCityFile(string filePath)
        {
            try
            {
                using (StreamReader file = File.OpenText(filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    List<City> locations = (List<City>)serializer.Deserialize(file, typeof(List<City>));
                    file.Close();
                    return locations;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filePath} not found.");
                return new List<City>();
            }
            catch (JsonException)
            {
                Console.WriteLine($"Error decoding JSON from {filePath}.");
                return new List<City>();
            }
        }
        public async Task<List<District>> ReadDistrictJsonFile(string filePath)
        {
            try
            {
                using (StreamReader file = File.OpenText(filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    List<District> locations = (List<District>)serializer.Deserialize(file, typeof(List<District>));
                    file.Close();
                    return locations;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filePath} not found.");
                return new List<District>();
            }
            catch (JsonException)
            {
                Console.WriteLine($"Error decoding JSON from {filePath}.");
                return new List<District>();
            }
        }
        public async Task<List<Commune>> ReadCommuneJsonFile(string filePath)
        {
            try
            {
                using (StreamReader file = File.OpenText(filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    List<Commune> locations = (List<Commune>)serializer.Deserialize(file, typeof(List<Commune>));
                    file.Close();
                    return locations;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filePath} not found.");
                return new List<Commune>();
            }
            catch (JsonException)
            {
                Console.WriteLine($"Error decoding JSON from {filePath}.");
                return new List<Commune>();
            }
        }
        public async Task<List<AddressEntityDto>> Address()
        {
            List<AddressEntityDto> listAddress = new List<AddressEntityDto>();
            List<City> cities = await ReadCityFile("C:\\Users\\hobbs.la\\Desktop\\DATN_Spring24_Main\\alr_capstone_project\\ALR_Capston_Project\\ALR.Data.Database\\City.json");
            List<District> districts = await ReadDistrictJsonFile("C:\\Users\\hobbs.la\\Desktop\\DATN_Spring24_Main\\alr_capstone_project\\ALR_Capston_Project\\ALR.Data.Database\\DistrictData.json");
            List<Commune> communes = await ReadCommuneJsonFile("C:\\Users\\hobbs.la\\Desktop\\DATN_Spring24_Main\\alr_capstone_project\\ALR_Capston_Project\\ALR.Data.Database\\Commune.json");
            foreach (var city in cities)
            {
                foreach (var district in districts)
                {
                    if (district.city_code == city.city_code)
                    {
                        foreach (var ward in communes)
                        {
                            if (ward.district_code == district.district_code)
                            {
                                AddressEntityDto addressEntity = new AddressEntityDto
                                {
                                    City = city,
                                    District = district,
                                    Commune = ward
                                };
                                listAddress.Add(addressEntity);
                            }
                        }
                    }
                }

            }
            return listAddress;
        }
    }
}
