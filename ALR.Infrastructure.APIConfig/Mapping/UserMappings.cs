using ALR.Data.Dto;
using ALR.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Infrastructure.APIConfig.Mapping
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<AdminCreateAccountDto, UserEntity>()
            .ForMember(dest => dest.UserEntityID, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.ProfileID, opt => opt.Ignore());

            CreateMap<AdminCreateAccountDto, ProfileEntity>()
                .ForMember(dest => dest.ProfileID, opt => opt.MapFrom(src => Guid.NewGuid())); 

        }
    }
}
