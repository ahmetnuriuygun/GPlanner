namespace GPlanner.Maui.Services.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string SchoolName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public bool IsNotified { get; set; }
}