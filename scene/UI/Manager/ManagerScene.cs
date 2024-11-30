using Godot;

partial class ManagerScene : Node
{
    public static void ChangeScene(SceneTree sceneTree, string scenePath)
	{
		var packedScene = (PackedScene)GD.Load(scenePath);
        var currentScene = sceneTree.CurrentScene;
        var nextSceneInstance = packedScene.Instantiate();

        sceneTree.Root.AddChild(nextSceneInstance);
        sceneTree.CurrentScene = nextSceneInstance;
        currentScene.QueueFree();
	}
}