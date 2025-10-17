using AutoMapper;
using MediatR;
using TI_API.Application.Common.Interfaces;
using TI_API.Application.Features.Indicadores.Dtos;
using TI_API.Domain.Entities;

namespace TI_API.Application.Features.Indicadores.Queries
{
    public class GetIndicadorByIdQueryHandler : IRequestHandler<GetIndicadorByIdQuery, IndicadorDto>
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;
        public GetIndicadorByIdQueryHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
        }
        public async Task<IndicadorDto> Handle(GetIndicadorByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWorks.Indicador.GetByIdAsync(request.Id);
            return entity == null ? null : _mapper.Map<IndicadorDto>(entity);
        }
    }

    public class GetAllIndicadoresQueryHandler : IRequestHandler<GetAllIndicadoresQuery, List<IndicadorDto>>
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;
        public GetAllIndicadoresQueryHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
        }
        public async Task<List<IndicadorDto>> Handle(GetAllIndicadoresQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWorks.Indicador.GetAllAsync();
            return _mapper.Map<List<IndicadorDto>>(entities);
        }
    }
}
