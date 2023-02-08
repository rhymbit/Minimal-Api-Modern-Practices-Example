using Api.v1.ExtensionMethods;
using Api.v1.Models.ApiKeys;

namespace Api.v1.FiltersAndValidators.ApiKeyFilters;

public class UserApiKeyFilter : IEndpointFilter
{
    private const string ApiKeyHeaderName = "X-API-KEY";

    private readonly IValidator<UserApiKeyModel> _validator;

    public UserApiKeyFilter(IValidator<UserApiKeyModel> validator) => _validator = validator;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        string? apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName];
        // validate api key
        var validationResult = await _validator.ValidateAsync(apiKey.ToUserApiKeyModel());
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }
        var result = await next(context);
        return result;
    }
}
