using Api.v1.ExtensionMethods;
using Api.v1.Models.UserModels;
using Api.v1.RequestsMediator.Commands.User;
using Api.v1.Services;

namespace Api.v1.RequestMediator.Handlers.User;

public class AddUserCommandHandler: IRequestHandler<AddUserCommand, UserResponseModel?>
{
    private readonly UserService _userService;

    public AddUserCommandHandler(UserService userService) => _userService = userService;
    
    public async ValueTask<UserResponseModel?> Handle(AddUserCommand request, CancellationToken ctoken)
    {
        var user = await _userService.AddUserAsync(request.Model.ToUser());
        return user?.ToUserResponse();
    }
}
