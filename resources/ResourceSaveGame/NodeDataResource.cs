using Godot;

public partial class NodeDataResource : SaveResource
{
    [Export]
    protected Vector2 globalPosition;
    [Export]
    protected NodePath nodePath;
    [Export]
    protected NodePath parentNodePath;

    public override void SaveData(Node2D node)
    {
        globalPosition = node.GlobalPosition;
        nodePath = node.GetPath();
        
        var parentNode = node.GetParent();

        if (parentNode != null)
        {
            parentNodePath = parentNode.GetPath();
        }
    }

    public virtual void LoadData(Window window)
    {
        GD.Print("fwiw");
        return;
    }
}