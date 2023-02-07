namespace Api.v1.RequestMediator.Queries.User;
public class GetJwtTokenQuery : IRequest<string>
{
    public string Username { get; }
    public GetJwtTokenQuery(string username) => Username = username;
}

