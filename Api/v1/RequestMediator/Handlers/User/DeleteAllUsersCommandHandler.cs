using Api.v1.ExtensionMethods;
using Api.v1.Models.UserModels;
using Api.v1.RequestsMediator.Commands.User;
using Api.v1.Services;

namespace Api.v1.RequestMediator.Handlers.User;

public class DeleteAllUsersCommandHandler : IRequestHandler<DeleteAllUsersCommand, List<UserResponseModel>?>
{
    private readonly UserService _userService;
    public DeleteAllUsersCommandHandler(UserService userService) => _userService = userService;

    public async ValueTask<List<UserResponseModel>?> Handle(DeleteAllUsersCommand request, CancellationToken cancellationToken)
    {
        var users = await _userService.DeleteAllUsersAsync();
        return users?.Select(u => u.ToUserResponse()).ToList();
    }
}
