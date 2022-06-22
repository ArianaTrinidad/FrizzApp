using FirzzApp.Business.Dtos.RequestDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirzzApp.Business.Validators.ProductValidators
{
    class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().NotNull().MaximumLength(50);

            RuleFor(x => x.Descripcion).NotEmpty().NotNull().MaximumLength(120);

            RuleFor(x => x.Nota).NotEmpty().NotNull().MaximumLength(150);

            RuleFor(x => x.Presentacion).NotEmpty().NotNull().MaximumLength(20);

            RuleFor(x => x.ImagenUrl).NotEmpty().NotNull();

            RuleFor(x => x.Precio).NotEmpty().NotNull().GreaterThan(0);

            RuleFor(x => x.EsPromo).NotEmpty().NotNull();
        }
    }
}
