using ALR.Data.Dto;
using ALR.Domain.Entities.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Infrastructure.APIConfig.Mapping
{
    public class BillHistoryMappings : Profile
    {
        public BillHistoryMappings()
        {
            CreateMap<BillHistoryEntity,BillHistoryDto>().ReverseMap();
        }
    }
}
