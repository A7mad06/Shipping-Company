using AutoMapper;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mapping
{
    public class CustomerProfile: Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, UserResultDTO>().ForMember(p => p.UserName, options => options.MapFrom(s => s.UserName)).ForMember(p => p.Email, options => options.MapFrom(m => m.Email)).ReverseMap();

            CreateMap<RegisterDTO, Customer>().ReverseMap();
        }
        
    }
}
