using Api.v1.Models.UserModels;

namespace Api.v1.FiltersAndValidators.UserFilters;

public class AddUserFilter : IEndpointFilter
{
    private readonly IValidator<AddUserRequestModel> _validator;

    public AddUserFilter(IValidator<AddUserRequestModel> validator) => _validator = validator;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var model = context.GetArgument<AddUserRequestModel>(0);
        var validationResult = await _validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }
        var result = await next(context);
        return result;
    }
}
