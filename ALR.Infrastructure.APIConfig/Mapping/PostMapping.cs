using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using AutoMapper;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Infrastructure.APIConfig.Mapping
{
    public class PostMapping : Profile
    {
        public PostMapping()
        {
            CreateMap<PostEntity, PostDto>().ReverseMap();

            CreateMap<ServicesPackageEntity, PostDto>()
                .ForMember(des => des.userId, opt => opt.MapFrom(src => src.userId))
                .ForMember(des => des.levelId, opt => opt.MapFrom(src => src.serviceId)).ReverseMap();

            CreateMap<ServiceEntity, PostDto>()
                 .ForMember(des => des.levelId, opt => opt.MapFrom(src => src.serviceId))
                  .ForMember(des => des.level, opt => opt.MapFrom(src => src.serviceName))
                  .ReverseMap();

            CreateMap<EditPostDto, PostEntity>().ReverseMap();
        }
    }
}
