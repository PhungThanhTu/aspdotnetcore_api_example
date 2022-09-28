namespace BLL.DTOs.Users;

public class UserDTO
{
    public Guid Id { get; set; }
    public string? Username { get; set; }

    public string? Email { get; set; }
    public string? Fullname { get; set; }
}