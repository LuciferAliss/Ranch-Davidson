using Godot;
using System;

public partial class DeathPanel : PanelContainer
{
    Player player;

    public override void _Ready()
    {
    }

    public void Dead()
    {
        player.uIManager.deathPanel.Show();
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
