using Godot;

partial class UserData : Node
{
    public User user { get; private set; }
    public static UserData Instance { get; private set; }
    public Vector2 playerPosition = new Vector2(282, 171);
    public int satiety = 100;
    public InventorySlot[] inventory = GD.Load<Inventory>("res://resources//ResourceSaveGame//Inventory.tres").slots;
    public int[] timeWorld = new int[] { 30, 13, 1 };
    public int count = 0;
    
    public override void _Ready()
    {
        Instance = this;
    }

    public void SetUser(User user)
    {
        this.user = user;
    }

    public void SetTimeWorld(int[] timeWorld)
    {
        this.timeWorld = timeWorld;
    }

    public string GetLogin()
    {
        return user.login;
    }
}