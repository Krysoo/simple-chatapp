namespace ChatApp;

public class User
{
    public string Name { get; set; }
    public string IP { get; set; }
    public string PCName { get; }

    public User(string Name)
    {
        this.Name = Name;
        this.PCName = System.Environment.MachineName;
    }
    
}