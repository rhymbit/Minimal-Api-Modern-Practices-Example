using Api.v1.Models.UserModels;

namespace Api.v1.FiltersAndValidators.Validators;

public class PutUserValidator: AbstractValidator<PutUserRequestModel>
{
    public PutUserValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();

        RuleFor(x => x.Name)
            .NotNull()
            .MinimumLength(4)
            .MaximumLength(30)
            .Matches(@"^[a-zA-Z0-9\s]+$")
            .WithMessage("Name must be alphanumeric and between 4 and 30 characters");

        RuleFor(x => (int)x.Age)
            .NotNull()
            .InclusiveBetween(10, 99);

        RuleFor(x => x.Email)
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotNull()
            .MinimumLength(8)
            .MaximumLength(30)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,30}$")
            .WithMessage(@"Password must be between 8 and 30 characters and 
                contain at least one uppercase letter, one lowercase letter, 
                one number and one special character");
    }
}
