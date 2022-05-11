using FirzzApp.Business.Dtos.RequestDto;
using FluentValidation;

namespace FirzzApp.Business.Validators.PaymentTypeValidators
{
    public class CreatePaymentTypeDtoValidator : AbstractValidator<CreatePaymentTypeDto>
    {
        public CreatePaymentTypeDtoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().NotNull().Length(20);
        }
    }
}
