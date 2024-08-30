using APIWeb_SPASentirseBien.Models;
using APIWeb_SPASentirseBien.Models.DTOs;
using APIWeb_SPASentirseBien.Models.DTOs.PatchDTOs;
using AutoMapper;

namespace APIWeb_SPASentirseBien.Controllers.MappingProfiles
{
    public class ServicioMapProfile : Profile
    {
        public ServicioMapProfile()
        {
            CreateMap<Servicio, ServicioDTO>();
            CreateMap<ServicioDTO, Servicio>();
            CreateMap<ServicioPatchDTO, Servicio>()
            .ForMember(n => n.ServicioId, option => option.Ignore())
            .ForMember(n => n.TipoServicio, option => option.Ignore())
            .ForMember(n => n.UsuarioClass, option => option.Ignore())
            .ForMember(n => n.UsuarioId, option => option.Ignore());
            CreateMap<Servicio, ServicioPatchDTO>();
        }
    }
}