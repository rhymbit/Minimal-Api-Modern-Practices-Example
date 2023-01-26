using Api.v1.Models.UserModels;

namespace Api.v1.FiltersAndValidators.UserFilters;

public class PutUserFilter: IEndpointFilter
{
    private readonly IValidator<PutUserRequestModel> _validator;

    public PutUserFilter(IValidator<PutUserRequestModel> validator) => _validator = validator;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var model = context.GetArgument<PutUserRequestModel>(0);
        var validationResult = await _validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }
        var result = await next(context);
        return result;
    }
}
