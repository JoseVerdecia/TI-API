using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using TI_API.Application.Common.Interfaces;
using TI_API.Application.Dtos;
using TI_API.Application.Features.Indicadores.EvaluateIndicador.Commands;
using TI_API.Domain.Entities;

namespace TI_API.Application.Features.Indicadores.EvaluateIndicador.Handlers
{
    public class EvaluateIndicatorCommandHandler : IRequestHandler<EvaluateIndicatorCommand, IndicadorResponseDTO>
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IEvaluacionService<IndicadorModel> _evaluacionService;
        private readonly IMapper _mapper;

        public EvaluateIndicatorCommandHandler(
            IUnitOfWorks unitOfWorks,
            IEvaluacionService<IndicadorModel> evaluacionService,
            IMapper mapper)
        {
            _unitOfWorks = unitOfWorks;
            _evaluacionService = evaluacionService;
            _mapper = mapper;
        }

        public async Task<IndicadorResponseDTO> Handle(EvaluateIndicatorCommand request, CancellationToken cancellationToken)
        {
            var indicador = await _unitOfWorks.Indicador.GetByAsync(
                i => i.Id == request.IndicadorId,
                includeProperties: "Proceso,ObjetivosAsignados.Objetivo");

            if (indicador == null)
                throw new NotFoundException($"Indicador con ID {request.IndicadorId} no encontrado");

            // Evaluar el indicador
            indicador.Evaluacion = _evaluacionService.Evaluar(indicador);

            // Actualizar en la base de datos
            _unitOfWorks.Indicador.Update(indicador);
            await _unitOfWorks.SaveChangesAsync();

            // Mapear a DTO
            var result = _mapper.Map<IndicadorResponseDTO>(indicador);
            result.Evaluacion = _evaluacionService.GetEvaluacionDisplay(indicador);

            return result;
        }
    }
}