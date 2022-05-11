using FirzzApp.Business.Dtos.RequestDto;
using FluentValidation;

namespace FirzzApp.Business.Validators.ProductValidators
{
    public class DeleteProductDtoValidator : AbstractValidator<DeleteProductDto>
    {
        public DeleteProductDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();

        }
    }
}
