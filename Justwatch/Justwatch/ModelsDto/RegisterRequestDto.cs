using System.Text.Json.Serialization;

namespace Justwatch.ModelsDto;

internal class RegisterRequestDto
{
    public string Email { get; set; }
    public string Name { get; set; }
 
    public string DateOfBirth { get; set; }
    public string Password { get; set; }
}
