using Godot;
using System;

public partial class UIManager : CanvasLayer
{
    public Settings settings;
	public InventoryManager inventory;
    public PauseMenu pauseMenu;
	public ColorRect background;
    public ToolsPanel hud;
	public DayAndNightPanel dayAndNightPanel;
	public HungryBar hungryBar;
    public bool inventoryVisible = false;
    public bool pauseVisible = false;
	public MarginContainer margin;

    public override void _Ready()
    {
        settings = GetNode<Settings>("MarginContainer/settings");
		inventory = GetNode<InventoryManager>("MarginContainer/Inventory");
        pauseMenu = GetNode<PauseMenu>("MarginContainer/PauseMenu");
		background = GetNode<ColorRect>("Beckground");
        hud = GetNode<ToolsPanel>("MarginContainer/ToolsPanel");
		dayAndNightPanel = GetNode<DayAndNightPanel>("MarginContainer/DayAndNightPanel");
		hungryBar = GetNode<HungryBar>("MarginContainer/HungryBar");
		margin = GetNode<MarginContainer>("MarginContainer");
    }

    public void InventoryVisibility()
    {
        if (inventoryVisible)
		{
			inventory.Hide();
			background.Hide();
			dayAndNightPanel.Show();
			Engine.TimeScale = 1;
		}
		else
		{
			inventory.Show();
			background.Show();
			dayAndNightPanel.Hide();
			Engine.TimeScale = 0;
		}

		inventoryVisible = !inventoryVisible;
    }

    public void PauseVisibility()
    {
        if (pauseVisible)
		{
			pauseMenu.Hide();
			background.Hide();
			settings.Hide();
			hungryBar.Show();
			dayAndNightPanel.Show();
			hud.Show();
			Engine.TimeScale = 1;
		}
		else 
		{
			pauseMenu.Show();
			background.Show();
			hungryBar.Hide();
			dayAndNightPanel.Hide();
			hud.Hide();
			Engine.TimeScale = 0;
		}

		pauseVisible = !pauseVisible;
    }
}