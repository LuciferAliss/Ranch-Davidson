using Godot;

partial class PlayerDataSource : SceneDataResource
{
    [Export]
    public int satiety;
    [Export]
    InventorySlot[] inventory;
    [Export]
    public bool accessTools; 
    
    public override void SaveData(Node2D node)
    {
        base.SaveData(node);
        if (node is Player player)
        {
            satiety = player.Satiety;
            inventory = player.uIManager.inventory.inventory.slots;
            accessTools = player.accessTools;
        }
    }

    public override void LoadData(Window window)
    {
        UserData.Instance.playerPosition = globalPosition;
        UserData.Instance.satiety = satiety;
        UserData.Instance.inventory = inventory;
        UserData.Instance.accessTools = accessTools;
    }
}