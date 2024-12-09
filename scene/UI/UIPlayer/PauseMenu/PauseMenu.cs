using Godot;
using System;
using System.Xml.Schema;

public partial class PauseMenu : PanelContainer
{
    Player player;
   
    public override void _Ready()
    {
        
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    private void ContinueGame()
    {
        player.ManagerPauseMenu();
    }

    private void OpenSetting()
    {
        player.settings.LoadSetting();   
        player.settings.Show();
    }

    private void ExitGame()
    {
        GetTree().Quit();
    }

    private void ExitInMainMenu()
    {
        player.ManagerPauseMenu();  
        ManagerScene.ChangeScene(GetParent().GetTree(), "res://scene//UI//MainMenu//MainMenu.tscn");
    }
}
