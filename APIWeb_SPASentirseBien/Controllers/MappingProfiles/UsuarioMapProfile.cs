using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb_SPASentirseBien.Models;
using APIWeb_SPASentirseBien.Models.DTOs;
using AutoMapper;

namespace APIWeb_SPASentirseBien.Controllers.MappingProfiles
{
    public class UsuarioMapProfile : Profile
    {
        public UsuarioMapProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        }
    }
}