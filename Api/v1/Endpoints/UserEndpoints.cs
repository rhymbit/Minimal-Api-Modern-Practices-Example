
namespace Api.v1.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllUsers);

        return group;
    }

    public static IResult GetAllUsers()
    {
        return Results.Ok("All Users");
    }
    
}
