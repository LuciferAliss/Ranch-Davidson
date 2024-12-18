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

    public override void _ExitTree()
    {
        gameDataResource = null;
        RemoveFromGroup("SaveLevelDataComponent");
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
                gameDataResource.saveDataNodes.Add((NodeDataResource)saveFinalResource);
            }
        }
    }

    public void SaveGame()
    {
        if (!DirAccess.DirExistsAbsolute(saveGameDataPath))
        {
            DirAccess.MakeDirAbsolute(saveGameDataPath);
        }

        string levelSaveFileName = $"save_{levelNameScene}_game_data.tres";
        string saveGamePath = saveGameDataPath + levelSaveFileName;

        SaveNodeData();

        var result = ResourceSaver.Save(gameDataResource, saveGamePath);

        using (var context = new SaveContext())
        {
            var save = context.Save.FirstOrDefault(u => u.id == UserData.Instance.user.id);
                
            FileAccess file = FileAccess.Open(saveGamePath, FileAccess.ModeFlags.Read);
            save.Save = file.GetAsText();
        
            context.SaveChanges();
            file.Close();
        }

        DirAccess.RemoveAbsolute(saveGamePath);
    }

    public void LoadGame()
    {
        string save;
        using (var context = new SaveContext())
        {
            var dbSave = context.Save.FirstOrDefault(u => u.id == UserData.Instance.user.id);
            save = dbSave.Save;
        }

        string levelSaveFileName = $"save_{levelNameScene + UserData.Instance.count++.ToString()}_game_data.tres";

        string saveGamePath = saveGameDataPath + levelSaveFileName;
        
        using var file = FileAccess.Open(saveGamePath, FileAccess.ModeFlags.Write);
        file.StoreString(save);
        file.Close();

        if (!FileAccess.FileExists(saveGamePath))
        {
            return;
        }

        gameDataResource = (SaveGameDataResource)ResourceLoader.Load(saveGamePath);

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

        DirAccess.RemoveAbsolute(saveGamePath);
        gameDataResource = null;
    }
}
