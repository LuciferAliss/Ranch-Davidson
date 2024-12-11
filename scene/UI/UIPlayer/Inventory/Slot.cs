using Godot;
using System;

public partial class Slot : PanelContainer
{
    TextureRect icon;
    Label CountItem;
    public override void _Ready()
    {
        icon = GetNode<TextureRect>("icon");
        CountItem = GetNode<Label>("MarginContainer/CountItem");
    }

    public void UpDate(InventorySlot slot)
    {
        if (slot.item != null && slot.count != 0)
        {
            icon.Texture = slot.item.texture;
            CountItem.Text = slot.count.ToString();
            CountItem.Show();
            SetThemeTypeVariation("SlotNotEmpty");
        }
        else
        {
            icon.Texture = null;
            CountItem.Hide();
            SetThemeTypeVariation("SlotEmpty");
        }
    } 
}