using Godot;

[GlobalClass]
public partial class SaveResource : Resource
{
    public virtual void SaveData(Node2D node)
    {
        GD.Print($"Saving data for node: {node.Name}");
    }
}