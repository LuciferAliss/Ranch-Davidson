using System.Collections.Generic;
using Godot;

[GlobalClass]
partial class SaveGameDataResource : SaveResource
{
    [Export]
    public Godot.Collections.Array<NodeDataResource> saveDataNodes;


    public override void SaveData(Node2D node)
    {
    }
}