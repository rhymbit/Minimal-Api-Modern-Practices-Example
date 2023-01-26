using Api.v1.Models.UserModels;

namespace Api.v1.ExtensionMethods;

public static class UserExtensionMethods
{
    public static User ToUser(this AddUserRequestModel model)
    {
        return new()
        {
            Name = model.Name ?? "",
            Age = model.Age,
            Email = model.Email ?? "",
            Password = model.Password ?? ""
        };
    }

    public static User ToUser(this PutUserRequestModel model)
    {
        return new()
        {
            Id = model.Id,
            Name = model.Name ?? "",
            Age = model.Age,
            Email = model.Email ?? "",
            Password = model.Password ?? ""
        };
    }

    public static UserResponseModel ToUserResponse(this User user)
    {
        return new()
        {
            Id = user.Id,
            Name = user.Name,
            Age = user.Age,
            Email = user.Email
        };
    }
}
