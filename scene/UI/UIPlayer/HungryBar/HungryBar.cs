using Godot;

public partial class HungryBar : Control
{
    Player player;
    TextureProgressBar progressHungryBar;
    Label valueHungryBarLabel;

    public override void _Ready()
    {
        progressHungryBar = GetNode<TextureProgressBar>("MarginContainer/ProgressHungryBar");
        valueHungryBarLabel = GetNode<Label>("MarginContainer/ValueHungryBarLabel");

        DayAndNightCycleManager.Instance.TimeTickHour += UpdateSatiety;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
        UpdateHungryBar();
    }

    private void UpdateSatiety(int hour)
    {
        player.Satiety = Mathf.Max(0, player.Satiety - 1);
        UpdateHungryBar();
    }

    public void UpdateHungryBar()
    {
        progressHungryBar.Value = player.Satiety;
        valueHungryBarLabel.Text = $"{player.Satiety}%";
    }

    public override void _ExitTree()
    {
        DayAndNightCycleManager.Instance.TimeTickHour -= UpdateSatiety;
    }
}
