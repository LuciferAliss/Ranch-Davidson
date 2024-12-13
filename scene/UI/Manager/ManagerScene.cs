using System.Threading.Tasks;
using Godot;

partial class ManagerScene : Node
{
    public async static void ChangeScene(SceneTree sceneTree, string scenePath, string NameEffect)
	{
        Effect.Instance.PlayEffect(NameEffect);
    	await Task.Delay(1500);
        var packedScene = (PackedScene)GD.Load(scenePath);
        var currentScene = sceneTree.CurrentScene;
        var nextSceneInstance = packedScene.Instantiate();

        sceneTree.Root.AddChild(nextSceneInstance);
        sceneTree.CurrentScene = nextSceneInstance;
        currentScene.QueueFree();
    }
}