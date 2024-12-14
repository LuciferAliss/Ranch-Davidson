using Godot;

public partial class ToolsPanel : PanelContainer
{
    Player player;
    Button Tool1;
    Button Tool2;
    Button Tool3;
    Button Tool4;
    Button Tool5;

    public override void _Ready()
    {
        Tool1 = GetNode<Button>("MarginContainer/HBoxContainer/None");
        Tool2 = GetNode<Button>("MarginContainer/HBoxContainer/ToolAxe");
        Tool3 = GetNode<Button>("MarginContainer/HBoxContainer/ToolWateringCan");
        Tool4 = GetNode<Button>("MarginContainer/HBoxContainer/ToolHoe");
        Tool5 = GetNode<Button>("MarginContainer/HBoxContainer/ToolWateringCan4");

        Tool1.Pressed += ChangeTool1;
        Tool2.Pressed += ChangeTool2;
        Tool3.Pressed += ChangeTool3;
        Tool4.Pressed += ChangeTool4;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public override void _Process(double delta)
    {
    }

    private void ChangeTool1()
    {
        player.ChangeTools(ItemsName.ToolNames[0]);
        player.hitComponent.tool = ItemsName.ToolNames[0];
    }

    private void ChangeTool2()
    {
        player.ChangeTools(ItemsName.ToolNames[1]);
        player.hitComponent.tool = ItemsName.ToolNames[1];
    }

    private void ChangeTool3()
    {
        player.ChangeTools(ItemsName.ToolNames[2]);
        player.hitComponent.tool = ItemsName.ToolNames[2];
    }

    private void ChangeTool4()
    {
        player.ChangeTools(ItemsName.ToolNames[3]);
        player.hitComponent.tool = ItemsName.ToolNames[3];
    }
}