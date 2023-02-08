using Api.v1.Models.ApiKeys;

namespace Api.v1.ExtensionMethods;

public static class ApiKeysExtensionMethods
{
    public static UserApiKeyModel ToUserApiKeyModel(this string? apikey)
    {
        return new()
        {
            ApiKey = apikey
        };
    }
}
