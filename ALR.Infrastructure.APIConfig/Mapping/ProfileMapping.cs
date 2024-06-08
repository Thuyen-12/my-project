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
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<ProfileDto, ProfileEntity>().ReverseMap();

            CreateMap<AccountProfileDto, ProfileEntity>().ReverseMap();

            CreateMap<AccountProfileDto, UserEntity>().ReverseMap();
        }
    }
}
