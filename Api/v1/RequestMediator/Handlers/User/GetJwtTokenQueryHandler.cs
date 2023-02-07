using Api.v1.RequestMediator.Queries.User;
using Api.v1.Services;

namespace Api.v1.RequestMediator.Handlers.User;

public class GetJwtTokenQueryHandler : IRequestHandler<GetJwtTokenQuery, string>
{
    private readonly JwtService _jwtService;
    
    public GetJwtTokenQueryHandler(JwtService jwtService) => _jwtService = jwtService;

    public ValueTask<string> Handle(GetJwtTokenQuery request, CancellationToken ctoken)
    {
        var token = _jwtService.GenerateJwtToken(request.Username);
        return ValueTask.FromResult(token);
    }
}
