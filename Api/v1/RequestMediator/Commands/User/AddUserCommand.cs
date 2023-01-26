using Api.v1.Models.UserModels;

namespace Api.v1.RequestsMediator.Commands.User;

public class AddUserCommand: IRequest<UserResponseModel>
{
    public AddUserRequestModel Model { get; }

    public AddUserCommand(AddUserRequestModel model) => Model = model;
}
