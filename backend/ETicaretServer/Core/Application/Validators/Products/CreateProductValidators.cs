using Application.Features.Commands.Product.CreateProduct;
using FluentValidation;

namespace Application.Validators.Products
{
    public class CreateProductValidators : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductValidators()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Product name can't be empty").MaximumLength(150).MinimumLength(2).WithMessage("Product Name can only be between 2 and 150 charachters");
            RuleFor(x => x.stock).NotEmpty().NotNull().WithMessage("Stock name can't be empty").Must(s => s >= 0).WithMessage("Stock can't be negative");
            RuleFor(x => x.price).NotEmpty().NotNull().WithMessage("Price name can't be empty").Must(s => s >= 0).WithMessage("Price can't be negative");
        }
    }
}
