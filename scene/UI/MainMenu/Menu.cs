using Godot;
using System;
using System.Net.Http.Headers;

public partial class Menu : PanelContainer
{
	MainMenu mainMenu;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
	
	public void SetMenu(MainMenu mainMenu)
    {
        this.mainMenu = mainMenu;
    }

	private void OpenSetting()
	{
		this.Visible = false;
		mainMenu.settings.LoadSetting();
		mainMenu.settings.Visible = true;
	}

	private void NewGame()
	{
		ManagerScene.ChangeScene(mainMenu.GetTree(), "res://scene//level//level.tscn");
	}
}