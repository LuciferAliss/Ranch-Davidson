using Godot;

partial class PlantSceneDataResource : SceneDataResource
{
    [Export]
    GrowthStates growthState;
    [Export]
    int currentHour;
    [Export]
    bool isWatered;

    public override void SaveData(Node2D node)
    {
        base.SaveData(node);

        if (node is Plant plant)
        {
            growthState = plant.growthState;
            currentHour = plant.currentHour;
            isWatered = plant.isWatered;
        }
    }

    public override void LoadData(Window window)
    {
        Node2D parentNode = null;
        Plant sceneNode = null;

        if (parentNodePath != null)
        {
            parentNode = (Node2D)window.GetNodeOrNull(parentNodePath);
        }

        if (nodePath != null)
        {
            var sceneFileResource = GD.Load<PackedScene>(sceneFilePath);
            sceneNode = sceneFileResource.Instantiate() as Plant;
        }   

        if (parentNode != null && sceneNode != null)
        {
            sceneNode.GlobalPosition = globalPosition;
            sceneNode.growthState = growthState;
            sceneNode.currentHour = currentHour;
            sceneNode.isWatered = isWatered;
            GD.Print($"parentNode.Name: {parentNode.Name}, sceneNode.Name: {sceneNode.Name}, sceneNode.GlobalPosition {globalPosition}");
            parentNode.AddChild(sceneNode);
        }
    }
}