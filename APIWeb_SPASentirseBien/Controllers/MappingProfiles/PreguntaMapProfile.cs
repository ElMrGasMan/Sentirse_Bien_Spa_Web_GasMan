using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb_SPASentirseBien.Models;
using APIWeb_SPASentirseBien.Models.DTOs;
using APIWeb_SPASentirseBien.Models.DTOs.PatchDTOs;
using AutoMapper;

namespace APIWeb_SPASentirseBien.Controllers.MappingProfiles
{
    public class PreguntaMapProfile : Profile
    {
        public PreguntaMapProfile()
        {
            CreateMap<Pregunta, PreguntaDTO>();
            CreateMap<PreguntaDTO, Pregunta>();
            CreateMap<PreguntaPatchDTO, Pregunta>()
            .ForMember(n => n.PreguntaId, option => option.Ignore())
            .ForMember(n => n.FechaPublicacion, option => option.Ignore())
            .ForMember(n => n.UsuarioClass, option => option.Ignore())
            .ForMember(n => n.UsuarioId, option => option.Ignore());
            CreateMap<Pregunta, PreguntaPatchDTO>();
        }
    }
}