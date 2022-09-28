namespace BLL.DTOs.Users;

public class LoginRequest
{
    public string? Username { get; set; }
    public string? RawPassword { get; set; }
}