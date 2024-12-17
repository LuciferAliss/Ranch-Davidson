using Godot;

partial class PlayerDataSource : SceneDataResource
{
    [Export]
    public int satiety;

    public override void SaveData(Node2D node)
    {
        base.SaveData(node);
        if (node is Player player)
        {
            satiety = player.Satiety;
        }
    }

    public override void LoadData(Window window)
    {
        UserData.Instance.playerPosition = globalPosition;
        UserData.Instance.satiety = satiety;
    }
}