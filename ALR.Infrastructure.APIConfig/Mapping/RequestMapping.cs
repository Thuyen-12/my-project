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
    public class RequestMapping : Profile
    {
        public RequestMapping()
        {
            CreateMap<RequestEntity, RequestDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.userEntity.Profile.UserName));
        }
    }
}
