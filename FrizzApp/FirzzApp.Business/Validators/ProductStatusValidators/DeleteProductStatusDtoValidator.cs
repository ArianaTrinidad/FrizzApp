using FirzzApp.Business.Dtos.RequestDto;
using FluentValidation;

namespace FirzzApp.Business.Validators.ProductStatusValidators
{
    public class DeleteProductStatusDtoValidator : AbstractValidator<DeleteProductStatusDto>
    {
        public DeleteProductStatusDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
