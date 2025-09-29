using API;
using FluentValidation;

namespace Api.Validators;

public class GetProductsRequestValidator : AbstractValidator<GetProductsRequest>
{
    public GetProductsRequestValidator()
    {
        RuleFor(x => x.Pages.PageNumber).GreaterThan(0).When(x => x.Pages is not null);
        RuleFor(x => x.Pages.AmountPerPage).GreaterThan(0).When(x => x.Pages is not null);
    }
}