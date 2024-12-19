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
        Tool5 = GetNode<Button>("MarginContainer/HBoxContainer/WheatSeeds");

        Tool1.Pressed += ChangeTool1;
        Tool2.Pressed += ChangeTool2;
        Tool3.Pressed += ChangeTool3;
        Tool4.Pressed += ChangeTool4;
        Tool5.Pressed += ChangeTool5;

        Tool1.SetPressed(true);
        Tool2.Disabled = true;
        Tool3.Disabled = true;
        Tool4.Disabled = true;
        Tool5.Disabled = true;
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
        if (!Tool2.Disabled)
        {
            player.ChangeTools(ItemsName.ToolNames[1]);
            player.hitComponent.tool = ItemsName.ToolNames[1];
        }
    }

    private void ChangeTool3()
    {
        if (!Tool3.Disabled)
        {
            player.ChangeTools(ItemsName.ToolNames[2]);
            player.hitComponent.tool = ItemsName.ToolNames[2];
        }
    }

    private void ChangeTool4()
    {
        if (!Tool4.Disabled)
        {
            player.ChangeTools(ItemsName.ToolNames[3]);
            player.hitComponent.tool = ItemsName.ToolNames[3];
        }
    }

    private void ChangeTool5()
    {
        if (!Tool5.Disabled)
        {
            player.ChangeTools(ItemsName.ToolNames[4]);
            player.hitComponent.tool = ItemsName.ToolNames[4];
        }
    }
}