using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class InventoryManager : PanelContainer
{
    [Signal]
    public delegate void UseItemEventHandler(InventoryItem slot);
    [Signal]
    public delegate void DestroyItemEventHandler();
    private Inventory inventory = GD.Load<Inventory>("res://scene//obj//Item//Inventory.tres");
    private List<Slot> slots = new();
    private int DraggedSlotIndex = -1;
    private int ChooseSlot = -1;

    public override void _Ready()
    {
        slots = GetNode<GridContainer>("MarginContainer/GridContainer").GetChildren().Select(x => (Slot)x).ToList();
        UpdateInventory();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("inventory"))
        {
            CancelDrag();
        }
    }

    public void ConnectCollectable(CollectableComponent collectable)
    {
        collectable.TakeItem += TakeItem;
        DestroyItem += collectable.DestroyItem;
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
        var emptySlot = inventory.slots.FirstOrDefault(slot => slot.item == null);

        if (emptySlot == null)
        {
            return;
        }

        var existingSlot = inventory.slots.FirstOrDefault(slot => slot.item == item && slot.count < slot.item.maxCount);

        if (existingSlot != null)
        {
            existingSlot.count++;
        }
        else
        {
            emptySlot.item = item;
            emptySlot.count = 1;
        }

        UpdateInventory();
        EmitSignal(nameof(DestroyItem));
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
        if (inventory.slots[position].count == 0)
        {
            inventory.slots[position].item = null;
        }
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

    private void ReplaceSlot(int slotIndex)
    {
        if (slotIndex != DraggedSlotIndex)
        {
            var firstSlot = inventory.slots[slotIndex];
            var secondSlot = inventory.slots[DraggedSlotIndex];
            
            inventory.slots[slotIndex] = secondSlot;
            inventory.slots[DraggedSlotIndex] = firstSlot;
            
            slots[slotIndex].UpDate(inventory.slots[slotIndex]);
            slots[DraggedSlotIndex].UpDate(inventory.slots[DraggedSlotIndex]);
        }
    }

    private bool IsDrag()
    {
        return DraggedSlotIndex > -1;
    }

    private void CancelDrag()
    {
        if (DraggedSlotIndex == -1) return;
        
        slots[DraggedSlotIndex].SetDrag(false);
        DraggedSlotIndex = -1;
    }

    public override void _Input(InputEvent @event)
    {   
        if (@event is InputEventMouseButton mouseEvent)
        {
            var findIndex = FindClickedItemIndex(mouseEvent.Position);
            if (Input.IsActionPressed("use_item_inventory") && Visible && !IsDrag())
            {    
                GD.Print("1");
                if (findIndex >= 0 && inventory.slots[findIndex].item != null && inventory.slots[findIndex].item.used == true)
                {
                    EmitSignal(nameof(UseItem), inventory.slots[findIndex].item);
                    ClearItem(findIndex);
                }
            }
            else if (Visible && !IsDrag() && mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.IsPressed())
            {
                GD.Print("2");
                if (findIndex >= 0 && inventory.slots[findIndex].item != null)
                {
                    DraggedSlotIndex = findIndex;
                    slots[DraggedSlotIndex].SetDrag(true);
                }
            }
            else if (Visible && IsDrag() && Input.IsActionJustPressed("use_item_inventory"))
            {
                GD.Print("3");
                CancelDrag();
            }
            else if (Visible && IsDrag() && mouseEvent.ButtonIndex == MouseButton.Left &&  !mouseEvent.IsPressed())
            {
                if (findIndex > -1)
                {
                    ReplaceSlot(findIndex);
                }
                CancelDrag();
            }
        }
        if (@event is InputEventMouseMotion mouseMotion)
        {
            var findIndex = FindClickedItemIndex(mouseMotion.Position);
            if (findIndex > -1 && ChooseSlot != findIndex) 
			{   
                if (ChooseSlot != -1) slots[ChooseSlot].HideBorder();
                ChooseSlot = findIndex;
                slots[ChooseSlot].ShowBorder();
            }
            else if (findIndex == -1)
            {
                if (ChooseSlot != -1) slots[ChooseSlot].HideBorder();
                ChooseSlot = -1;
            }
        }
    }
}