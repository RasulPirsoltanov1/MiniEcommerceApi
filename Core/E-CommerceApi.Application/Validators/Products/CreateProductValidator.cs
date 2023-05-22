using E_CommerceApi.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Validators.Products
{
    public class CreateProductValidator:AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("name is required.")
                .MaximumLength(150).MinimumLength(5).WithMessage("name must be contain amoung 5 and 150 letters.");
            RuleFor(x=>x.Price).NotEmpty().NotNull().WithMessage("price is required.")
                .Must(x => x >= 0).WithMessage("price must be bigger then 0.");
            RuleFor(x => x.Stock).NotEmpty().NotNull().WithMessage("stock is required.").Must(x => x >= 0).WithMessage("stock must be bigger then 0.");
        }
    }
}
