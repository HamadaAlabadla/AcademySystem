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
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.ImageIdentity , src => src.MapFrom(src => src.ImageIdentity));
        }
    }
}
