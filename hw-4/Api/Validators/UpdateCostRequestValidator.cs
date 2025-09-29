using API;
using FluentValidation;

namespace Api.Validators;

public class UpdateCostRequestValidator :  AbstractValidator<UpdateCostRequest>
{
    public UpdateCostRequestValidator()
    {
        RuleFor(x => x.NewCost).GreaterThanOrEqualTo(0);
    }
}