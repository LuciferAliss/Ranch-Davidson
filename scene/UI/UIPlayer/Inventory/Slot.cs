using Godot;
using System;

public partial class Slot : PanelContainer
{
    IconSlot icon;
    Label CountItem;
    TextureRect Border;

    public override void _Ready()
    {
        icon = GetNode<IconSlot>("MarginContainer/icon");
        CountItem = GetNode<Label>("MarginContainer/CountItem");
        Border = GetNode<TextureRect>("MarginContainer/Border");
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

    public void SetDrag(bool drag)
    {
        icon.IsDrag = drag;
        if (!drag)
        {
            CanselDrag();
        }
    }

    private void CanselDrag()
    {
        icon.Position = new Vector2(14, 19);
    }

    public void ShowBorder()
    {
        Border.Visible = true;
    } 

    public void HideBorder()
    {
        Border.Visible = false;
    }
}