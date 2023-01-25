namespace Api.v1.Models.UserModels;

public class AddUserRequestModel
{
    public string? Name { get; set; }
    public UInt16 Age { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
