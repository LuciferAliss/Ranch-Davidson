using Godot;

partial class SceneDataResource : NodeDataResource
{
    [Export]
    protected string nodeName;
    [Export]
    protected string sceneFilePath;

    public override void SaveData(Node2D node)
    {
        base.SaveData(node);

        nodeName = node.Name;
        sceneFilePath = node.SceneFilePath;
    }

    public override void LoadData(Window window)
    {
        Node2D parentNode = null;
        Node2D sceneNode = null;

        if (parentNodePath != null)
        {
            parentNode = (Node2D)window.GetNodeOrNull(parentNodePath);
        }

        if (nodePath != null)
        {
            var sceneFileResource = GD.Load<PackedScene>(sceneFilePath);
            sceneNode = sceneFileResource.Instantiate() as Node2D;
        }   

        if (parentNode != null && sceneNode != null)
        {
            sceneNode.GlobalPosition = globalPosition;
            parentNode.AddChild(sceneNode);
        }
    }
}