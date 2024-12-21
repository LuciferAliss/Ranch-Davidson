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

    public void ExitInMainMenu()
    {
        player.animationPl.AnimationFinished -= player.FinishedAnimation;
        player.cooldown = true;
        player.uIManager.PauseVisibility();
        player.uIManager.Hide();
        ManagerScene.ChangeScene(player.GetParent().GetTree(), "res://scene//UI//MainMenu//MainMenu.tscn", "VignetteEffect_Open");
    }

    private void SaveGame()
    {
        SaveGameManager.Instance.SaveGame();
    }
}