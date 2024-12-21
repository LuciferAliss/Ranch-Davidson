using Godot;
using System;
using System.Linq;

public partial class Statistics : PanelContainer
{
	MainMenu mainMenu;
	Label NumberActions;
	Label NumberTreesCutDown;
	Label NumberDays;
	Label AmountWheatHarvested;

	public override void _Ready()
	{
		NumberActions = GetNode<Label>("MarginContainer/HBoxContainer/VBoxContainer/NumberActions");
		NumberTreesCutDown = GetNode<Label>("MarginContainer/HBoxContainer/VBoxContainer/NumberTreesCutDown");
		NumberDays = GetNode<Label>("MarginContainer/HBoxContainer/VBoxContainer2/NumberDays");
		AmountWheatHarvested = GetNode<Label>("MarginContainer/HBoxContainer/VBoxContainer2/AmountWheatHarvested");

		using (var contextStatistics = new StatisticsContext())
		{
			var Statistics = contextStatistics.Statistics.FirstOrDefault(u => UserData.Instance.user.id == u.id);
			NumberActions.Text = $"Количество действий: {Statistics.NumberActions}";
			NumberDays.Text = $"Количество дней: {Statistics.NumberDays}";
			NumberTreesCutDown.Text = $"Количество срубленых деревьев: {Statistics.NumberTreesCutDown}";
			AmountWheatHarvested.Text = $"Количество собранной пшеницы: {Statistics.AmountWheatHarvested}";
		}
	}

	public override void _Process(double delta)
	{
	}

	private void Close()
	{
		this.Visible = false;
		if (mainMenu != null)
		{
			mainMenu.menu.Visible = true;
		}
	}

	public void SetStatistics(MainMenu mainMenu)
    {
        this.mainMenu = mainMenu;
    }
}
