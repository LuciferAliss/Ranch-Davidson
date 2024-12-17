using Godot;

partial class UserData : Node
{
    public User user { get; private set; }
    public static UserData Instance { get; private set; }
    public Vector2 playerPosition = new();
    public int satiety = 100;

    public override void _Ready()
    {
        Instance = this;
        playerPosition = new Vector2(282, 171);
    }

    public void SetUser(User user)
    {
        this.user = user;
    }
}