using Godot;

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

    public SaveResource SaveData()
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
