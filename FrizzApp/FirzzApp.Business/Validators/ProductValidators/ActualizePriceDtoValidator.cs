using FirzzApp.Business.Dtos.RequestDto;
using FluentValidation;

namespace FirzzApp.Business.Validators.ProductValidators
{
    class ActualizePriceDtoValidator : AbstractValidator<ActualizePriceDto>
    {
        public ActualizePriceDtoValidator()
        {
            RuleFor(x => x.Percentage).NotEmpty();
            RuleFor(x => x.Percentage).GreaterThan(0);
            RuleFor(x => x.Percentage).LessThanOrEqualTo(100);
            RuleFor(x => x.Percentage).GreaterThanOrEqualTo(-100);
        }
    }
}
