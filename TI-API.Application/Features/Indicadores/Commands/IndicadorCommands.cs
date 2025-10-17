using MediatR;
using TI_API.Application.Features.Indicadores.Dtos;

namespace TI_API.Application.Features.Indicadores.Commands
{
    public record CreateIndicadorCommand(CreateIndicadorCommandDto Dto) : IRequest<IndicadorDto>;
    public record UpdateIndicadorCommand(UpdateIndicadorCommandDto Dto) : IRequest<IndicadorDto>;
    public record DeleteIndicadorCommand(int Id) : IRequest<bool>;
}
