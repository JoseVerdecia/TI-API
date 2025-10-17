using FluentValidation;
using TI_API.Application.Features.Indicadores.Dtos;

namespace TI_API.Application.Features.Indicadores.Validators
{
    public class CreateIndicadorCommandDtoValidator : AbstractValidator<CreateIndicadorCommandDto>
    {
        public CreateIndicadorCommandDtoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(250);
            RuleFor(x => x.MetaCumplir).NotEmpty();
            RuleFor(x => x.ProcesoId).GreaterThan(0);
            RuleFor(x => x.Tipo).IsInEnum();
            RuleFor(x => x.Origen).IsInEnum();
            RuleFor(x => x.ObjetivosId)
                       .NotNull()
                       .Must(list => list != null && list.Count > 0)
                       .WithMessage("Debe vincular al menos un objetivo.");
        }
    }

    public class UpdateIndicadorCommandDtoValidator : AbstractValidator<UpdateIndicadorCommandDto>
    {
        public UpdateIndicadorCommandDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(250);
            RuleFor(x => x.MetaCumplir).NotEmpty();
            RuleFor(x => x.ProcesoId).GreaterThan(0);
            RuleFor(x => x.Tipo).IsInEnum();
            RuleFor(x => x.Origen).IsInEnum();
        }
    }
}
