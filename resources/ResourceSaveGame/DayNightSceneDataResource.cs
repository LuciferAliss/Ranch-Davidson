using Godot;

partial class DayNightSceneDataResource : SceneDataResource
{
    [Export]
    int currentMinute;
    [Export]
    int currentHour;
    [Export]
    int currentDay;

    public override void SaveData(Node2D node)
    {
        base.SaveData(node);

        int[] timeWorld = DayAndNightCycleManager.Instance.GetTimeWorld();
        currentMinute = timeWorld[0];
        currentHour = timeWorld[1];
        currentDay = timeWorld[2];
    }

    public override void LoadData(Window window)
    {
        UserData.Instance.SetTimeWorld(new int[] { currentMinute, currentHour, currentDay });
    }
}