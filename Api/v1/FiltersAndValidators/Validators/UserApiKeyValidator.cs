using Api.v1.Models.ApiKeys;

namespace Api.v1.FiltersAndValidators.Validators;

public class UserApiKeyValidator : AbstractValidator<UserApiKeyModel>
{
    public UserApiKeyValidator(MyDbContext dbContext)
    {
        RuleFor(x => x.ApiKey)
            .NotNull()
            .WithMessage("Api key is required. Send it in the header as X-API-KEY");
        
        // validate if api-key matches the api key in the user's row in the db
        // I'm printing this to check if the dbContext was injected in the validator or not
        Console.WriteLine(dbContext.Users.Count());
    }
}
