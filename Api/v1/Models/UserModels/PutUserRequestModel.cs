namespace Api.v1.Models.UserModels;

public class PutUserRequestModel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public UInt16 Age { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
