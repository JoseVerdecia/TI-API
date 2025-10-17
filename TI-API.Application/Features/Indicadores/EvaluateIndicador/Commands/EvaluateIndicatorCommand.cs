using MediatR;
using TI_API.Application.Dtos;

namespace TI_API.Application.Features.Indicadores.EvaluateIndicador.Commands
{
    public class EvaluateIndicatorCommand : IRequest<IndicadorResponseDTO>
    {
        public int IndicadorId { get; set; }
    }
}
