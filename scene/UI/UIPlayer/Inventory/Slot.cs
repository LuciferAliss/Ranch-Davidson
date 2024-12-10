using Godot;
using System;

public partial class Slot : PanelContainer
{
    TextureRect icon;
    public override void _Ready()
    {
        icon = GetNode<TextureRect>("icon");
    }

    public void UpDate(InventoryItem item)
    {
        if (item != null)
        {
            icon.Texture = item.texture;
            SetThemeTypeVariation("SlotNotEmpty");
        }
        else
        {
            icon.Texture = null;
            SetThemeTypeVariation("SlotEmpty");
        }
    } 
}
