using System.Collections.Generic;
using Godot;

public struct DataNpc
{
    public bool acquaintance;
    public Vector2 globalPosition;
}

partial class NPCData : Node
{
    public Dictionary<string, DataNpc> npcData = new();
    public static NPCData Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }
}