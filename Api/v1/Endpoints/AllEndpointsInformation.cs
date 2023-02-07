namespace Api.v1.Endpoints;

public static class AllEndpointsInformation
{
    public static WebApplication MapAllEndpointsInformation(this WebApplication app)
    {
        app.MapGet("/", AllEndpoints)
            .WithName("AllEndpoints");
        
        return app;
    }

    public static IResult AllEndpoints()
    {
        var endpoints = new Dictionary<string, string>
        {
            { "GET: GetJwtTokenWithoutAuth", "api/v1/token" },
            { "GET: GetJwtToken", "api/v1/users/token" },
            { "GET: GetAllUsers", "api/v1/users" },
            { "GET: GetAllUsersDelayed", "api/v1/users/delayed" },
            { "GET: GetUserById", "api/v1/users/{id}" },
            { "POST: AddUser", "api/v1/users" },
            { "PUT: PutUser", "api/v1/users" },
            { "DELETE: DeleteUserById", "api/v1/users/{id}" },
            { "DELETE: DeleteAllUsers", "api/v1/users" }
        };

        return Results.Ok(endpoints);
    }
}
