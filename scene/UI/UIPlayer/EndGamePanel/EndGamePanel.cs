using Godot;
using System;

public partial class EndGamePanel : PanelContainer
{
    Player player;

    public override void _Ready()
    {
    }

    public void Dead()
    {
        player.uIManager.endGamePanel.Show();
        player.uIManager.PauseVisibility();
        player.uIManager.pauseMenu.Hide();
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    private void ExitInMenu()
    {
        player.uIManager.pauseMenu.ExitInMainMenu();
    }
}
