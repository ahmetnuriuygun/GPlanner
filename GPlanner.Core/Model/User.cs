namespace GPlanner.Core.Model;

public class User
{
    public int Id;

    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    public string SchoolName { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }

    public bool IsNotified { get; set; }

    public ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();

    public User()
    {

    }






}