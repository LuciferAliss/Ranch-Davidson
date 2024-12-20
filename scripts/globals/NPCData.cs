using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class NPCData : Node
{
    public struct DataNpc
    {
        public bool acquaintance;
        public Vector2 globalPosition;
    }

    public enum StateQuest
    {
        NotTaken,
        InProcess,
        Completed
    }

    public class QuestNPC
    {
        public string nameQuest;
        public StateQuest stateQuest;
        
        public QuestNPC(string name, StateQuest state)
        {
            nameQuest = name;
            stateQuest = state;
        }
    }


    public Dictionary<string, DataNpc> npcData = new();
    public static NPCData Instance { get; private set; }


    public override void _Ready()
    {
        Instance = this;
    }
}