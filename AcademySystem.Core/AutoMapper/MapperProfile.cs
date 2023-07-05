using AcademySystem.Core.Dtos;
using AcademySystem.Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademySystem.Core.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        { 
            CreateMap<StudentDto , Student>()
                .ForMember(b => b.ImageIdentity,b => b.Ignore())
                .ForMember(b => b.ImageStudent,b => b.Ignore())
                .ForMember(b => b.TimeOfRegistration,b => b.Ignore())
                .ForMember(b => b.appUserId,b => b.Ignore())
                .ForMember(b => b.appUser,b => b.Ignore())
                .ReverseMap();
        }
    }
}
