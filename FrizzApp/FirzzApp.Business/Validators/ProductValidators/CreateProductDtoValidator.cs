using FirzzApp.Business.Dtos.RequestDto;
using FluentValidation;

namespace FirzzApp.Business.Validators.ProductValidators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().NotNull().MaximumLength(50);

            RuleFor(x => x.Descripcion).NotEmpty().NotNull().MaximumLength(120);

            RuleFor(x => x.Nota).NotEmpty().NotNull().MaximumLength(150);

            RuleFor(x => x.Presentacion).NotEmpty().NotNull().MaximumLength(20);

            RuleFor(x => x.Precio).NotEmpty().NotNull().GreaterThan(0);
        }

    }
}