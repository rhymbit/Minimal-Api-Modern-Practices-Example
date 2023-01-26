using Api.v1.Models.UserModels;

namespace Api.v1.RequestsMediator.Commands.User;

public class PutUserCommand: IRequest<UserResponseModel>
{
    public PutUserRequestModel Model { get; }
    public PutUserCommand(PutUserRequestModel model) => Model = model;
}
