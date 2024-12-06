using Godot;
using System;
using System.Net.Http.Headers;

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
        Tool1 = GetNode<Button>("MarginContainer/HBoxContainer/ToolAxe");
        Tool2 = GetNode<Button>("MarginContainer/HBoxContainer/ToolWateringCan");
        Tool3 = GetNode<Button>("MarginContainer/HBoxContainer/ToolWateringCan2");
        Tool4 = GetNode<Button>("MarginContainer/HBoxContainer/ToolWateringCan3");
        Tool5 = GetNode<Button>("MarginContainer/HBoxContainer/ToolWateringCan4");

        Tool1.Pressed += ChangeTool1;
        Tool2.Pressed += ChangeTool2;
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
        GD.Print(ItemsName.ToolNames[2].GetType());
        player.ChangeTools(ItemsName.ToolNames[2]);
    }

    private void ChangeTool2()
    {
        player.ChangeTools(ItemsName.ToolNames[1]);
    }
}