using AutoMapper;
using BillingProcessor.Entities;
using BillingProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingProcessor
{
    public class BillingMappingProfile:Profile
    {
        public BillingMappingProfile()
        {
            CreateMap<BillingDto, Billing>()
                .ForMember(dest=> dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.value, opt => opt.MapFrom(src => Convert.ToDecimal(src.Value.Amount)))
                .ForMember(dest => dest.typeId, opt => opt.MapFrom(src => src.Type.Id))
                .ForMember(dest => dest.tax, opt => opt.MapFrom(src => Convert.ToInt32(src.Tax.Percentage)))
                .ForMember(dest => dest.orderId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
