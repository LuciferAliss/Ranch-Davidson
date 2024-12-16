using Godot;
using System;

public partial class SaveDataComponent : Node
{
    Node2D parentNode;
    [Export]
    public SaveResource SaveDataResource; 
    public override void _Ready()
    {
        parentNode = GetParent() as Node2D;
        AddToGroup("SaveDataComponent");
    }

    public Resource SaveData()
    {
        if (parentNode == null)
        {
		    return null;
        }
	
        if (SaveDataResource == null)
        {
            return null;
        }
            
        SaveDataResource.SaveData(parentNode);
        
        return SaveDataResource;
    }
}
