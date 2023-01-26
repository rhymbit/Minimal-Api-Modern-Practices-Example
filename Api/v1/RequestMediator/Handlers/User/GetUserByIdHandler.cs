using Api.v1.ExtensionMethods;
using Api.v1.Models.UserModels;
using Api.v1.RequestsMediator.Queries.User;
using Api.v1.Services;

namespace Api.v1.RequestMediator.Handlers.User;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponseModel?>
{
    private readonly UserService _userService;

    public GetUserByIdHandler(UserService userService) => _userService = userService;
    
    public async ValueTask<UserResponseModel?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserAsync(request.UserId);
        return user?.ToUserResponse();
    }
}
