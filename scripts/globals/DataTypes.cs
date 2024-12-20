
using System;
using System.Collections.Generic;
using Godot;

public static class ItemsName
{
    public static Dictionary<int, string> ToolNames = new Dictionary<int, string>
    {
        [0] = "None",
        [1] = "Axe",
        [2] = "WateringCan",
        [3] = "Hoe",
        [4] = "WheatSeeds"
    };

    public static Dictionary<int, string> ItemNames = new Dictionary<int, string>
    {
        [0] = "Wheat",
        [1] = "Apple",
        [2] = "Log"
    };
}

public enum GrowthStates 
{
    Germination,
    Vegetative,
    Reproduction,
    Maturity,
    Harvesting
}