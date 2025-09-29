using API;
using FluentValidation;

namespace Api.Validators;

public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
{
    public AddProductRequestValidator()
    {
        RuleFor(x => x.Cost).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.CreationDate).NotNull();
        RuleFor(x => x.Weight).GreaterThanOrEqualTo(0);
    }
}