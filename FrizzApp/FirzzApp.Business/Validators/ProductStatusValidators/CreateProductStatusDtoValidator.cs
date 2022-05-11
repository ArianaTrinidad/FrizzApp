using FirzzApp.Business.Dtos.RequestDto;
using FluentValidation;

namespace FirzzApp.Business.Validators.ProductStatusValidators
{
    public class CreateProductStatusDtoValidator : AbstractValidator<CreateProductStatusDto>
    {
        public CreateProductStatusDtoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().NotNull().Length(20);
        }
    }
}
