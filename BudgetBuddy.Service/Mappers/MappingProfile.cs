using AutoMapper;
using BudgetBuddy.Domain.Models.Dtos;
using BudgetBuddy.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBuddy.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserDto>().ReverseMap();
        }
    }
}
