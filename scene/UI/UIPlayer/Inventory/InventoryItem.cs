using Godot;
using System;

[GlobalClass]
public partial class InventoryItem : Resource
{
    [Export]
    public string name { get; private set; }
    [Export]
    public int maxCount;
    [Export]
    public bool used;
    [Export]
    public Texture2D texture { get; private set; } 
}