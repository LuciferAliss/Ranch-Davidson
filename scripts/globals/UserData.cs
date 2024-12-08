using Godot;

partial class UserData : Node
{
    public User user { get; private set; }
    public static UserData Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public void SetUser(User user)
    {
        this.user = user;
    }
}