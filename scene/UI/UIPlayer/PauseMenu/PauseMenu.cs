using Godot;

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
        player.uIManager.PauseVisibility();
    }

    private void OpenSetting()
    {
        player.uIManager.settings.LoadSetting();   
        player.uIManager.settings.Show();
    }

    private void ExitGame()
    {
        GetTree().Quit();
    }

    private void ExitInMainMenu()
    {
        player.uIManager.PauseVisibility();
        player.uIManager.Hide();
        player.cooldown = true;
        ManagerScene.ChangeScene(player.GetParent().GetTree(), "res://scene//UI//MainMenu//MainMenu.tscn", "VignetteEffect_Open");
    }
}