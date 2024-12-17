using Godot;

partial class SaveGameManager : Node
{
    public static SaveGameManager Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("save_game"))
        {
            SaveGame();
        }
    }
    private void SaveGame()
    {
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