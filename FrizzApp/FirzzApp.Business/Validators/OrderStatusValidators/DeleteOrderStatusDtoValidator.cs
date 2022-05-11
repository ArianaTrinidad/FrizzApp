using FirzzApp.Business.Dtos.RequestDto;
using FluentValidation;

namespace FirzzApp.Business.Validators.OrderStatusValidators
{
    public class DeleteOrderStatusDtoValidator : AbstractValidator<DeleteOrderStatusDto>
    {
        public DeleteOrderStatusDtoValidator()
        {
            RuleFor(x => x.EstadoId).NotEmpty().NotNull();
        }
    }
}
