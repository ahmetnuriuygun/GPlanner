namespace GPlanner.Core.Model;

public class User
{
    public int Id;
    public string Username { get; set; }

    public User(string username)
    {
        this.Id = 1;
        this.Username = username;
    }






}