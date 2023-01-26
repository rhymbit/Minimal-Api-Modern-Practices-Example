using Api.v1.Models.UserModels;

namespace Api.v1.RequestsMediator.Queries.User;

public class GetUserByIdQuery: IRequest<UserResponseModel>
{
    public string UserId { get; }

    public GetUserByIdQuery(string userId) => UserId = userId;
}
