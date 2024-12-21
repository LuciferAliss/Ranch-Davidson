using Godot;

public partial class UserData : Node
{
    public User user { get; private set; }
    public string login = "";
    public static UserData Instance { get; private set; }
    public Vector2 playerPosition = new Vector2(1008, 80);
    public int satiety = 100; 
    public InventorySlot[] inventory = GD.Load<Inventory>("res://resources//ResourceSaveGame//Inventory.tres").slots;
    public int[] timeWorld = new int[] { 30, 13, 1 };
    public int count = 0;
    public bool accessTools = false;
    public bool haveSave = false;
    public int NumberActions = 0;
    public int NumberDays = 0;
    public int NumberTreesCutDown = 0; 
    public int AmountWheatHarvested = 0;
    
    public override void _Ready()
    {
        Instance = this;
    }

    public void SetUser(User user)
    {
        this.user = user;
        login = user.login;
    }

    public void SetTimeWorld(int[] timeWorld)
    {
        this.timeWorld = timeWorld;
    }
}