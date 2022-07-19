using FirzzApp.Business.Dtos.RequestDto;
using FluentValidation;

namespace FirzzApp.Business.Validators.ProductValidators
{
    class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Nombre).MaximumLength(50);

            RuleFor(x => x.Descripcion).MaximumLength(120);

            RuleFor(x => x.Nota).MaximumLength(150);

            RuleFor(x => x.Presentacion).MaximumLength(20);

            RuleFor(x => x.Precio).GreaterThan(0);
        }
    }
}
