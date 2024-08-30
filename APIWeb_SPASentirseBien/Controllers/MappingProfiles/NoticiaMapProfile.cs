using APIWeb_SPASentirseBien.Models;
using APIWeb_SPASentirseBien.Models.DTOs;
using APIWeb_SPASentirseBien.Models.DTOs.PatchDTOs;
using AutoMapper;

namespace APIWeb_SPASentirseBien.Controllers.MappingProfiles
{
    public class NoticiaMapProfile : Profile
    {
        public NoticiaMapProfile()
        {
            CreateMap<Noticia, NoticiaDTO>();
            CreateMap<NoticiaDTO, Noticia>();
            CreateMap<NoticiaPatchDTO, Noticia>()
            .ForMember(n => n.NoticiaId, option => option.Ignore())
            .ForMember(n => n.FechaPublicacion, option => option.Ignore());
            CreateMap<Noticia, NoticiaPatchDTO>();
        }
    }
}