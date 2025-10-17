using FluentValidation;
using TI_API.Application.Dtos;

namespace TI_API.Application.Common.Validations
{
    public class CreateIndicadorDeAreaDtoValidator : AbstractValidator<IndicadorDeAreaCreateDTO>
    {
        public CreateIndicadorDeAreaDtoValidator()
        {
            RuleFor(x => x.AreaId).GreaterThan(0);
            RuleFor(x => x.MetaCumplir).NotEmpty().NotNull();
        }
    }
}
