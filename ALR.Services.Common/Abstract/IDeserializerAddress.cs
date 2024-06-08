using ALR.Data.Dto.AddressDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.Common.Abstract
{
    public interface IDeserializerAddress
    {
        Task<List<AddressEntityDto>> Address();
        Task<List<City>> ReadCityFile(string filePath);
        Task<List<Commune>> ReadCommuneJsonFile(string filePath);
        Task<List<District>> ReadDistrictJsonFile(string filePath);
    }
}
