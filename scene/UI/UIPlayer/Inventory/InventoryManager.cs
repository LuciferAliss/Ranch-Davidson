using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class InventoryManager : PanelContainer
{
    private Inventory inventory = GD.Load<Inventory>("res://scene//obj//Item//Inventory.tres");
    private List<Slot> slots = new();

    public override void _Ready()
    {
        CollectableComponent.Instance.TakeItem += TakeItem;

        slots = GetNode<GridContainer>("MarginContainer/GridContainer").GetChildren().Select(x => (Slot)x).ToList();
        UpdateInventory();
    }

    private void UpdateInventory()
    {
        int itemCount = Math.Min(inventory.items.Length, slots.Count);

        for (int i = 0; i < itemCount; i++)
        {
            slots[i].UpDate(inventory.items[i]);
        }
    }

    private void TakeItem(InventoryItem item)
    {
        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items == null)
            {
                PutInInventory(i, item);
                break;
            }
        }
    }

    private void PutInInventory(int position, InventoryItem newItem)
    {
        inventory.items[position] = newItem;
        slots[position].UpDate(newItem);
    }
}