namespace BackendGGJ.Models;

public record UserBase(Guid Id);

public struct User
{
    public User(Guid id)
    {
        Id = id;
        ClickCount = 0;
        Team = -1;
    }


    public Guid Id { get; set; }
    public int ClickCount { get; set; }
    public int Team { get; set; }
}