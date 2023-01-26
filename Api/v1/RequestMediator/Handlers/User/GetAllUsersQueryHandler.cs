using Api.v1.ExtensionMethods;
using Api.v1.Models.UserModels;
using Api.v1.RequestsMediator.Queries.User;
using Api.v1.Services;

namespace Api.v1.RequestMediator.Handlers.User;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponseModel>?>
{
    private readonly UserService _userService;
    public GetAllUsersQueryHandler(UserService userService) => 
        _userService = userService;
    
    public async ValueTask<List<UserResponseModel>?> Handle(GetAllUsersQuery request, CancellationToken ctoken)
    {
        var allUsers = await _userService.GetAllUsersAsync();
        var allUserResponses = allUsers.Select(u => u.ToUserResponse()).ToList();
        return allUserResponses;
    }
}
