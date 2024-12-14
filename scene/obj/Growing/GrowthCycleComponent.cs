using Godot;
using System;
using System.ComponentModel.DataAnnotations;

public partial class GrowthCycleComponent : Node
{
    GrowthStates currentGrowthStates = GrowthStates.Seed; 
    [Export(PropertyHint.Range, "5,365")]
    private int daysUntilHarvest = 7;
    private int startingDay = 0;
    private int currentDay = 0;
    public bool isWatered = false;

    [Signal]
    public delegate void WheatMaturityEventHandler();
    [Signal]
    public delegate void WheatHarvestingEventHandler();

    public override void _Ready()
    {
        DayAndNightCycleManager.Instance.Connect(nameof(DayAndNightCycleManager.TimeTickDay), new Callable(this, nameof(OnDayPassed)));
    }

    private void OnDayPassed(int day)
    {
        if (isWatered)
        {
            if ( startingDay == 0)
            {
                startingDay = day;
            }

            GrowthStatesFunk(startingDay, day);
            HarvestState(startingDay, day);
        }
    }

    private void GrowthStatesFunk(int StartingDay, int currentDay)
    {
        if (currentGrowthStates == GrowthStates.Maturity)
        {
            return;
        }

        int numStates = 5;
        int growthDaysPassed = (currentDay - StartingDay) % numStates;
        int stateIndex = growthDaysPassed % numStates + 1;
        currentGrowthStates = (GrowthStates)stateIndex;

        var f = new GrowthStates(); 
        var name = f[currentGrowthStates];

        if (currentGrowthStates == GrowthStates.Maturity)
        {
            EmitSignal(nameof(WheatMaturity));
        }    
    }

    private void HarvestState(int StartingDay, int currentDay)
    {
        if(currentGrowthStates == GrowthStates.Harvesting)
        {
            return;
        }

        int daysPassed = (currentDay - StartingDay) % daysUntilHarvest;

        if(daysPassed == (int)GrowthStates.Harvesting - 1)
        {
            currentGrowthStates = GrowthStates.Harvesting;
            EmitSignal(nameof(WheatHarvesting));
        }
    }

    public GrowthStates GetCurrentGrowthState()
    {
        return currentGrowthStates;
    }
}
