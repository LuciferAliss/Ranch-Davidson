using Godot;

partial class NPCDataSorce : SceneDataResource
{
    [Export]
    public bool acquaintance;
    
    public override void SaveData(Node2D node)
    {
        base.SaveData(node);
        if (node is BasicNpc player)
        {
            acquaintance = player.acquaintance;
        }
    }

    public override void LoadData(Window window)
    {  
        DataNpc dataNpc = new DataNpc
        {
            globalPosition = this.globalPosition,
            acquaintance = this.acquaintance
        };
        NPCData.Instance.npcData.Add(nodeName, dataNpc);
    }   
}