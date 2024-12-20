using Godot;
using System.Linq;

public partial class Menu : PanelContainer
{
	MainMenu mainMenu;
	Button ContinuationGameButton;

	public override void _Ready()
	{
		ContinuationGameButton = GetNode<Button>("MarginContainer/VBoxContainer/LoadGame");
	}

	public override void _Process(double delta)
	{
	}
	
	public void UpdateButton()
	{
		if (UserData.Instance.haveSave)
		{
			ContinuationGameButton.Show();
		}
		else
		{
			ContinuationGameButton.Hide();
		}
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
		ManagerScene.ChangeScene(mainMenu.GetTree(), "res://scene//level//level.tscn", "VignetteEffect_Open");
		GameStart.Instance.newGame = true;
	}

	private void ContinuationGame()
	{
		ManagerScene.ChangeScene(mainMenu.GetTree(), "res://scene//level//level.tscn", "VignetteEffect_Open");
		GameStart.Instance.newGame = false;
	}

	private void ExitFromGame()
	{
		GetTree().Quit();
	}
}