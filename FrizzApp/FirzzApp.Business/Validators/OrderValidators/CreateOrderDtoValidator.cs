using FirzzApp.Business.Dtos.RequestDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirzzApp.Business.Validators.OrderValidators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.PrecioTotal).NotEmpty().NotNull().GreaterThan(0);

            RuleFor(x => x.EsDelivery).NotEmpty().NotNull();

            RuleFor(x => x.DireccionDelivery).NotEmpty().NotNull();

            RuleFor(x => x.MedioPagoId).NotEmpty().NotNull();

            RuleFor(x => x.ProductosId).NotEmpty().NotNull();
        }

    }
}
