using AutoMapper;
using TI_API.Application.Common.Extensions;
using TI_API.Application.Dtos;
using TI_API.Domain.Entities;
using TI_API.Domain.Enums;
using TI_API.Entities;
using TI_API.Application.Features.Indicadores.Dtos;

namespace TI_API.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IndicadorModel, IndicadorResponseDTO>()
                  .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.GetDisplayName()))
                  .ForMember(dest => dest.Origen, opt => opt.MapFrom(src => src.Origen.GetDisplayName()))
                  .ForMember(dest => dest.Evaluacion, opt => opt.MapFrom(src => src.Evaluacion.GetDisplayName()))
                  .ForMember(dest => dest.Proceso, opt => opt.MapFrom(src => src.Proceso))
                  .ForMember(dest => dest.ObjetivosAsignados, opt => opt.MapFrom(src => src.ObjetivosAsignados))
                  .ForMember(dest => dest.IndicadoresDeArea, opt => opt.MapFrom(src => src.IndicadoresAsignados));

            CreateMap<ProcesoModel, ProcesoSimpleDTO>()
               .ForMember(dest => dest.Evaluacion, opt => opt.MapFrom(src => src.Evaluacion.GetDisplayName()));


            CreateMap<ObjetivoModel, ObjetivoSimpleDTO>()
                 .ForMember(dest => dest.Evaluacion, opt => opt.MapFrom(src => src.Evaluacion.GetDisplayName()));

            CreateMap<IndicadorDeAreaModel, IndicadorDeAreaResponseDTO>()
                .ForMember(dest => dest.Evaluacion, opt => opt.MapFrom(src => src.Evaluacion.GetDisplayName()))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area));

            CreateMap<AreaModel, AreaSimpleDTO>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.GetDisplayName()));

            CreateMap<CreateIndicadorDTO, IndicadorModel>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => Enum.Parse<IndicadorType>(src.Tipo)))
                .ForMember(dest => dest.Origen, opt => opt.MapFrom(src => Enum.Parse<IndicadorOrigen>(src.Origen)));

            CreateMap<IndicadorDeAreaModel, IndicadorDeAreaResponseDTO>();

            CreateMap<IndicadorModel, IndicadorDto>();
            CreateMap<CreateIndicadorCommandDto, IndicadorModel>();
            CreateMap<UpdateIndicadorCommandDto, IndicadorModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            //CreateMap<UpsertIndicadorDTO, IndicadorModel>();
            //CreateMap<UpsertProcesoDTO, ProcesoModel>();
            //CreateMap<UpsertObjetivoDTO, ObjetivoModel>();
            //CreateMap<UpsertAreaDTO, AreaModel>();
            //CreateMap<UpsertIndicadorDeAreaDTO, IndicadorDeAreaModel>();
        }
    }
}
