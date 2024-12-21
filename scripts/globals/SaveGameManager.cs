using System.Linq;
using Godot;

partial class SaveGameManager : Node
{
    public static SaveGameManager Instance { get; private set; }
    bool startGame = false;

    public override void _Ready()
    {
        Instance = this;
        GameStart.Instance.SignalGameStart += StartGame;
    }

    private void StartGame(bool value)
    {
        startGame = value;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("save_game") && startGame)
        {
            SaveGame();
            Effect.Instance.PlayEffect("Save_");
        }
    }
    public void SaveGame()
    {
        using (var contextStatistics = new StatisticsContext())
        {
            var Statistics = contextStatistics.Statistics.FirstOrDefault(u => UserData.Instance.user.id == u.id);
			Statistics.NumberActions = UserData.Instance.NumberActions;
			Statistics.NumberDays = UserData.Instance.NumberDays;
			Statistics.NumberTreesCutDown = UserData.Instance.NumberTreesCutDown;
			Statistics.AmountWheatHarvested = UserData.Instance.AmountWheatHarvested;
            
            contextStatistics.SaveChanges();
        }

        SaveLevelDataComponent saveLevelDataComponent = (SaveLevelDataComponent)GetTree().GetFirstNodeInGroup("SaveLevelDataComponent");

        if (saveLevelDataComponent != null)
        {
            saveLevelDataComponent.SaveGame();
        }
    }

    public void LoadGame()
    {
        SaveLevelDataComponent saveLevelDataComponent = (SaveLevelDataComponent)GetTree().GetFirstNodeInGroup("SaveLevelDataComponent");

        if (saveLevelDataComponent != null)
        {
            saveLevelDataComponent.LoadGame();
        }
    }
}