using Godot;
using System;

public partial class UIManager : CanvasLayer
{
    public Settings settings;
	public InventoryManager inventory;
    public PauseMenu pauseMenu;
	public ColorRect background;
    public ToolsPanel hud;
    public bool inventoryVisib = false;
    public bool pauseVisib = false;

    public override void _Ready()
    {
        settings = GetNode<Settings>("MarginContainer/settings");
		inventory = GetNode<InventoryManager>("MarginContainer/Inventory");
        pauseMenu = GetNode<PauseMenu>("MarginContainer/PauseMenu");
		background = GetNode<ColorRect>("Beckground");
        hud = GetNode<ToolsPanel>("MarginContainer/ToolsPanel");
    }

    public void InventoryVisibility()
    {
        if (inventoryVisib)
		{
			inventory.Hide();
			background.Hide();
			Engine.TimeScale = 1;
		}
		else
		{
			inventory.Show();
			background.Show();
			Engine.TimeScale = 0;
		}

		inventoryVisib = !inventoryVisib;
    }

    public void PauseVisibility()
    {
        if (pauseVisib)
		{
			pauseMenu.Hide();
			background.Hide();
			settings.Hide();
			hud.Show();
			Engine.TimeScale = 1;
		}
		else 
		{
			pauseMenu.Show();
			background.Show();
			hud.Hide();
			Engine.TimeScale = 0;
		}

		pauseVisib = !pauseVisib;
    }
}
