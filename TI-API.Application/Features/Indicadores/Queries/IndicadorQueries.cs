using MediatR;
using TI_API.Application.Features.Indicadores.Dtos;

namespace TI_API.Application.Features.Indicadores.Queries
{
    public record GetIndicadorByIdQuery(int Id) : IRequest<IndicadorDto>;
    public record GetAllIndicadoresQuery() : IRequest<List<IndicadorDto>>;
}
