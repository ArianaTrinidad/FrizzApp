using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirzzApp.Business.Dtos.RequestDto;
using FrizzApp.Data.Entities;

namespace FirzzApp.Business.Validators.ProductValidators
{
    class ChangeStatusProductDtoValidator : AbstractValidator<ChangeStockStatusProductDto>
    {
        public ChangeStatusProductDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();

        }
    }
}
