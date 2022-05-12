using FirzzApp.Business.Dtos.RequestDto;
using FluentValidation;

namespace FirzzApp.Business.Validators.OrderStatusValidators
{
    public class CreateOrderStatusDtoValidator : AbstractValidator<CreateOrderStatusDto>
    {
        public CreateOrderStatusDtoValidator()
        {
            RuleFor(x => x.Estado).NotEmpty().NotNull().Length(20);
        }
    }
}
