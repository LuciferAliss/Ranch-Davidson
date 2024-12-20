using Godot;

public partial class Menu : PanelContainer
{
	MainMenu mainMenu;
	Button ContinuationGameButton;

	public override void _Ready()
	{
		using (var contextSave = new SaveContext())
		{
			
		}
		ContinuationGameButton = GetNode<Button>("MarginContainer/VBoxContainer/LoadGame");
		if (UserData.Instance.haveSave)
		{
			ContinuationGameButton.Show()	;
		}
		else
		{
			ContinuationGameButton.Hide();
		}
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
		ManagerScene.ChangeScene(mainMenu.GetTree(), "res://scene//level//level.tscn", "VignetteEffect_Open");
		GameStart.Instance.newGame = true;
		UserData.Instance.haveSave = true;
	}

	private void ContinuationGame()
	{
		ManagerScene.ChangeScene(mainMenu.GetTree(), "res://scene//level//level.tscn", "VignetteEffect_Open");
	}

	private void ExitFromGame()
	{
		GetTree().Quit();
	}
}