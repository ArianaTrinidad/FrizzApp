using FirzzApp.Business.Dtos.RequestDto;
using FluentValidation;

namespace FirzzApp.Business.Validators.CategoryValidators
{
    public class DeleteCategoryDtoValidator : AbstractValidator<DeleteCategoryDto>
    {
        public DeleteCategoryDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
