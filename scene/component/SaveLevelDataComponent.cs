using Godot;
using System;
using System.Linq;

public partial class SaveLevelDataComponent : Node
{
    string levelNameScene;
    string saveGameDataPath = "user://game_data/";
    private string saveFileName = "save_{0}_game_data.tres";
    SaveGameDataResource gameDataResource;

    public override void _Ready()
    {
        AddToGroup("SaveLevelDataComponent");
        levelNameScene = GetParent().Name;
    }

    private void SaveNodeData()
    {
        SaveDataComponent[] nodes = GetTree().GetNodesInGroup("SaveDataComponent").Select(u => (SaveDataComponent)u).ToArray();
        gameDataResource = new SaveGameDataResource();

        if (gameDataResource.saveDataNodes == null)
        {
            gameDataResource.saveDataNodes = new Godot.Collections.Array<NodeDataResource>();
        }

        if (nodes != null)
        {
            foreach(SaveDataComponent node in nodes)
            {
                NodeDataResource saveDataResource = (NodeDataResource)node.SaveData();
                var saveFinalResource = saveDataResource.Duplicate();
                gameDataResource.saveDataNodes.Append(saveFinalResource);
            }
        }
    }

    public void SaveGame()
    {
        if (!DirAccess.DirExistsAbsolute(saveGameDataPath))
        {
            DirAccess.MakeDirAbsolute(saveGameDataPath);
        }

        var levelSaveFileName = $"save_{levelNameScene}_game_data.tres";

        SaveNodeData();        

        var result = ResourceSaver.Save(gameDataResource, saveGameDataPath + levelSaveFileName);
        GD.Print($"result: {result}");
    }

    public void LoadGame()
    {
        string levelSaveFileName = $"save_{levelNameScene}_game_data.tres";
        string saveGamepath = saveGameDataPath + levelSaveFileName;

        if (!FileAccess.FileExists(saveGamepath))
        {
            return;
        }

        gameDataResource = (SaveGameDataResource)ResourceLoader.Load(saveGamepath);

        if (gameDataResource == null)
        {
            return;
        }

        Window rootNode = GetTree().Root;

        foreach (var resource in gameDataResource.saveDataNodes)
        {
            if (resource is Resource)
            {
                if (resource is NodeDataResource)
                {
                    resource.LoadData(rootNode);
                }
            }
        } 
    }
}
