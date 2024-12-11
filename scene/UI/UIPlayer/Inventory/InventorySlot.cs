using Godot;

[GlobalClass]
public partial class InventorySlot : Resource
{
    [Export]
    public InventoryItem item;
    [Export]
    public int count = 0;
}