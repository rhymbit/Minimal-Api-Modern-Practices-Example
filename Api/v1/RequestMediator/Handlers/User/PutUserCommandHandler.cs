using Api.v1.ExtensionMethods;
using Api.v1.Models.UserModels;
using Api.v1.RequestsMediator.Commands.User;
using Api.v1.Services;

namespace Api.v1.RequestMediator.Handlers.User;

public class PutUserCommandHandler : IRequestHandler<PutUserCommand, UserResponseModel?>
{
    private readonly UserService _userService;

    public PutUserCommandHandler(UserService userService)
    {
        _userService = userService;
    }

    public async ValueTask<UserResponseModel?> Handle(PutUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.PutUserAsync(request.Model.ToUser());
        return user?.ToUserResponse();
    }
}
