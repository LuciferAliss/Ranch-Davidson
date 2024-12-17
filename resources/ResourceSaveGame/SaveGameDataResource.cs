using System.Collections.Generic;
using Godot;

partial class SaveGameDataResource : SaveResource
{
    [Export]
    public Godot.Collections.Array<NodeDataResource> saveDataNodes;
}