using Godot;

[GlobalClass]
public partial class Inventory : Resource
{
    [Export]
    public InventorySlot[] slots;
}