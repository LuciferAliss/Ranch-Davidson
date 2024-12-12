using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class InventoryManager : PanelContainer
{
    [Signal]
    public delegate void UseItemEventHandler(InventoryItem slot);
    private Inventory inventory = GD.Load<Inventory>("res://scene//obj//Item//Inventory.tres");
    private List<Slot> slots = new();

    public override void _Ready()
    {
        slots = GetNode<GridContainer>("MarginContainer/GridContainer").GetChildren().Select(x => (Slot)x).ToList();
        UpdateInventory();
    }

    public void ConnectCollectable(CollectableComponent collectable)
    {
        collectable.TakeItem += TakeItem;
    }

    private void UpdateInventory()
    {
        for (int i = 0; i <  Math.Min(inventory.slots.Length, slots.Count); i++)
        {
            slots[i].UpDate(inventory.slots[i]);
        }
    }

    private void TakeItem(InventoryItem item)
    {
        foreach (var slot in inventory.slots)
        {
            if (slot.item == item && slot.count < slot.item.maxCount)
            {
                slot.count += 1;
                UpdateInventory();
                return;
            }
        }

        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].item == null)
            {
                PutInInventory(i, item);
                return;
            }
        }
    }

    private void PutInInventory(int position, InventoryItem newItem)
    {
        inventory.slots[position].item = newItem;
        inventory.slots[position].count += 1;
        slots[position].UpDate(inventory.slots[position]);
    }

    private void ClearItem(int position)
    {
        inventory.slots[position].count -= 1;
        slots[position].UpDate(inventory.slots[position]);
    }

    private int FindClickedItemIndex(Vector2 position)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetGlobalRect().HasPoint(position))
            {
                return i;
            }
        }
        return -1;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("use_item_inventory"))
        {
            if (@event is InputEventMouse mouseEvent)
            {
                var findIndex = FindClickedItemIndex(mouseEvent.Position);

                if (findIndex >= 0 && inventory.slots[findIndex].item != null && inventory.slots[findIndex].item.used == true)
                {
                    EmitSignal(nameof(UseItem), inventory.slots[findIndex].item);
                    ClearItem(findIndex);
                }
            }
        }
    }
}