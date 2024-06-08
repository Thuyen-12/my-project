using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Infrastructure.APIConfig.Mapping
{
    public class FeedbackMapping: Profile
    {
        public FeedbackMapping() 
        {
            CreateMap<FeedBackDto, FeedbackEntity>()
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<EditFeedBackDto, FeedbackEntity>().ReverseMap();
        }
    }
}
