using FirzzApp.Business.Dtos.RequestDto;
using FluentValidation;

namespace FirzzApp.Business.Validators.PaymentTypeValidators
{
    public class DeletePaymentTypeDtoValidator : AbstractValidator<DeletePaymentTypeDto>
    {
        public DeletePaymentTypeDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
