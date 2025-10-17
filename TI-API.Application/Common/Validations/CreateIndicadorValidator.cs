using FluentValidation;
using TI_API.Application.Dtos;

namespace TI_API.Application.Common.Validations
{
    public class CreateIndicadorDtoValidator : AbstractValidator<CreateIndicadorDTO>
    {
        public CreateIndicadorDtoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(250);
            RuleFor(x => x.MetaCumplir).NotEmpty().NotNull();
            RuleFor(x => x.ProcesoId).GreaterThan(0).NotNull();
            RuleFor(x => x.Tipo).IsInEnum();
            RuleFor(x => x.Origen).IsInEnum();
            RuleFor(x => x.ObjetivosIds).NotNull();
            RuleForEach(x => x.IndicadoresDeArea).SetValidator(new IndicadorDeAreaCreateDTO);
        }
    }
}
