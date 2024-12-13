
using System;
using System.Collections.Generic;

public static class ItemsName
{
    public static Dictionary<int, string> ToolNames = new Dictionary<int, string>
    {
        [0] = "None",
        [1] = "WateringCan",
        [2] = "Axe",
        [3] = "Hoe"
    };
}

public enum GrowthStates 
{
    Seed,
    Germination,
    Vegetative,
    Reproduction,
    Maturity,
    Harvesting
}