using System.Text.Json.Serialization;

namespace Justwatch.ModelsDto;

internal class LoginRequestDto
{
    [JsonPropertyName("Email")]
    public string Email { get; set; }
    [JsonPropertyName("Password")]
    public string Password { get; set; }
}
