using Api.v1.Models.UserModels;

namespace Api.v1.RequestsMediator.Commands.User;

public class DeleteUserByIdCommand: IRequest<UserResponseModel>
{
    public string UserId { get; }
    public DeleteUserByIdCommand(string id) => UserId = id;
}
