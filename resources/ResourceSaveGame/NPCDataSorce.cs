using System.Collections.Generic;
using System.Linq;
using Godot;
using static NPCData;

partial class NPCDataSorce : SceneDataResource
{
    [Export]
    public bool acquaintance;
    [Export]
    public QuestDataResource[] questDatas;
    
    public override void SaveData(Node2D node)
    {
        base.SaveData(node);
        if (node is BasicNpc npc)
        {
            acquaintance = npc.acquaintance;
            GD.Print(npc.questNPCs.Count);
            questDatas = new QuestDataResource[npc.questNPCs.Count];
            for (int i = 0; i < questDatas.Length; i++)
            {
                QuestDataResource quest = new QuestDataResource();
                quest.nameQuest = npc.questNPCs[i].nameQuest;
                quest.stateQuest = (int)npc.questNPCs[i].stateQuest;
                questDatas[i] = quest;
            }
        }
    }

    public override void LoadData(Window window)
    {  
        List<QuestNPC> QuestNpc = new();
        
        foreach (var ques in questDatas)
        {
            QuestNpc.Add(new QuestNPC(ques.nameQuest, (StateQuest)ques.stateQuest));
        }  

        DataNpc dataNpc = new DataNpc
        {
            globalPosition = this.globalPosition,
            acquaintance = this.acquaintance,
            questNPCs = QuestNpc
        };

        Instance.npcData.Clear();
        Instance.npcData.Add(nodeName, dataNpc);
    }   
}