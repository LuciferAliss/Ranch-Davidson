using Godot;
using System;

public partial class DayAndNightPanel : Control
{
	Label TimeLabel;
	Label DayLabel;
	
	public override void _Ready()
	{
		TimeLabel = GetNode<Label>("TimePanel/MarginContainer/TimeLabel");
		DayLabel = GetNode<Label>("DayPanel/MarginContainer/DayLabel");

		DayAndNightCycleManager.Instance.TimeTick += OnTimeTick;
		DayAndNightCycleManager.Instance.GameSpeed = 1000f;
	}

	public override void _Process(double delta)
	{
	}

	private void OnTimeTick(int day, int hour, int minute)
	{
		DayLabel.Text = $"Day:{day}";
		TimeLabel.Text = TimeManager.FormatTimer(hour * 60 + minute);
	}

    public override void _ExitTree()
    {
		DayAndNightCycleManager.Instance.TimeTick -= OnTimeTick;
		DayAndNightCycleManager.Instance.GameSpeed = 0f;
        TimeLabel = null;
		DayLabel= null;
    }
}
