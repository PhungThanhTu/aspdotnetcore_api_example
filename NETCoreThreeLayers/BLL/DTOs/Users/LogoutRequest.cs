namespace BLL.DTOs.Users;

public class LogoutRequest
{
    public string? UserId { get; set; }
    public string? Token { get; set; }
}