using Api.v1.FiltersAndValidators.UserFilters;
using Api.v1.Models.UserModels;
using Api.v1.RequestsMediator.Commands.User;
using Api.v1.RequestsMediator.Queries.User;

namespace Api.v1.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpointsV1(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllUsers).WithName("GetAllUsers");
        group.MapGet("/delayed", GetAllUsersDelayed).WithName("GetUsersDelayed");
        group.MapGet("/{id}", GetUser).WithName("GetUserById");
        group.MapPost("/", AddUser)
            .AddEndpointFilter<AddUserFilter>()
            .WithName("AddUser");
        group.MapPut("/", PutUser)
            .AddEndpointFilter<PutUserFilter>()
            .WithName("PutUser");
        group.MapDelete("/{id}", DeleteUser).WithName("DeleteUserById");
        group.MapDelete("/", DeleteAllUsers).WithName("DeleteAllUsers");

        return group;
    }

    public static async Task<IResult> GetAllUsers(IMediator _mediator, CancellationToken ctoken)
    {
        var query = new GetAllUsersQuery();
        var result = await _mediator.Send(query, ctoken);
        return result != null ? Results.Ok(result) : Results.Ok(new());
    }

    public static async Task<IResult> GetAllUsersDelayed(IMediator _mediator, CancellationToken ctoken)
    {
        var query = new GetAllUsersQuery();
        var result = await _mediator.Send(query, ctoken);
        await Task.Delay(5_000, ctoken); // creating 5 seconds of delay
        return result != null ? Results.Ok(result) : Results.Ok(new());
    }

    public static async Task<IResult> GetUser(string id, IMediator _mediator, CancellationToken ctoken)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query, ctoken);
        return result != null ? Results.Ok(result) : Results.NotFound();
    }

    public static async Task<IResult> AddUser(AddUserRequestModel user, IMediator _mediator, CancellationToken ctoken)
    {
        var command = new AddUserCommand(user);
        var results = await _mediator.Send(command, ctoken);
        return results != null ?
            Results.Created($"/users/{results.Id}", results) :
            Results.Json(statusCode: 500, data: new
            {
                message = "Something went wrong at server side",
                data = results
            });
    }

    public static async Task<IResult> PutUser(PutUserRequestModel user, IMediator _mediator, CancellationToken ctoken)
    {
        var command = new PutUserCommand(user);
        var results = await _mediator.Send(command, ctoken);
        return results != null ? Results.Ok(results) : Results.NotFound();
    }

    public static async Task<IResult> DeleteUser(string id, IMediator _mediator, CancellationToken ctoken)
    {
        var command = new DeleteUserByIdCommand(id);
        var results = await _mediator.Send(command, ctoken);
        return results != null ? Results.NoContent() : Results.NotFound();
    }

    public static async Task<IResult> DeleteAllUsers(IMediator _mediator, CancellationToken ctoken)
    {
        var command = new DeleteAllUsersCommand();
        var results = await _mediator.Send(command, ctoken);
        return results != null ?
            Results.NoContent() :
            Results.Json(statusCode: 500, data: new
            {
                message = "Something went wrong at server side",
                data = results
            });
    }
}
