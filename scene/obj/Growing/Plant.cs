using Godot;

public partial class Plant : Node2D
{
    public GrowthStates growthState = GrowthStates.Germination;
    public int currentHour = 0;
    public bool isWatered = false;
}