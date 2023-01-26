using Api.v1.ExtensionMethods;
using Api.v1.Models.UserModels;
using Api.v1.RequestsMediator.Commands.User;
using Api.v1.Services;

namespace Api.v1.RequestMediator.Handlers.User;

public class DeleteUserByIdHandler : IRequestHandler<DeleteUserByIdCommand, UserResponseModel?>
{
    private readonly UserService _userService;

    public DeleteUserByIdHandler(UserService userService) => _userService = userService;

    public async ValueTask<UserResponseModel?> Handle(DeleteUserByIdCommand request, CancellationToken ctoken)
    {
        var user = await _userService.DeleteUserAsync(request.UserId);
        return user?.ToUserResponse();
    }
}
