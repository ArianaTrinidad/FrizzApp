using FirzzApp.Business.Dtos.RequestDto;
using FluentValidation;

namespace FirzzApp.Business.Validators.ProductValidators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().NotNull().Length(50);

            RuleFor(x => x.Descripcion).NotEmpty().NotNull().Length(120);

            RuleFor(x => x.Nota).NotEmpty().NotNull().Length(150);

            RuleFor(x => x.Presentacion).NotEmpty().NotNull().Length(20);

            RuleFor(x => x.ImagenUrl).NotEmpty().NotNull();

            RuleFor(x => x.Precio).NotEmpty().NotNull().NotEqual(0).LessThan(0);

            RuleFor(x => x.EsPromo).NotEmpty().NotNull();

            RuleFor(x => x.Categoria).NotEmpty().NotNull();
        }

    }
}