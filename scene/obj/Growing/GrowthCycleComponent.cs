using Godot;
using System;
using System.ComponentModel.DataAnnotations;

public partial class GrowthCycleComponent : Node
{
    GrowthStates currentGrowthStates = GrowthStates.Germination; 
    [Export(PropertyHint.Range, "5,365")]
    private int daysUntilHarvest = 7;
    private int currentHour = 0;
    public bool isWatered = false;

    [Signal]
    public delegate void WheatHarvestingEventHandler();
    [Signal]
    public delegate void UpdateGrowthStateEventHandler(int state);

    public override void _Ready()
    {
        DayAndNightCycleManager.Instance.Connect(nameof(DayAndNightCycleManager.TimeTickHour), new Callable(this, nameof(OnHourPassed)));
    }

    private void OnHourPassed(int hour)
    {
        if (isWatered)
        {
            currentHour += 1;

            GrowthStatesFunk(currentHour);
            HarvestState(currentHour);
        
            EmitSignal(nameof(UpdateGrowthState), (int)currentGrowthStates);
        
            if (currentHour % 24 == 0)
            {
                isWatered = false;
            }
        }
    }

    private void GrowthStatesFunk(int currentHour)
    {
        if (currentGrowthStates == GrowthStates.Maturity || currentGrowthStates == GrowthStates.Harvesting)
        {
            return;
        }

        int numStates = 4;
        int TicChangeState = daysUntilHarvest * 24 / numStates * (((int)currentGrowthStates) + 1); //42

        if (TicChangeState == currentHour)
        {
            int stateIndex = (int)currentGrowthStates + 1;
            currentGrowthStates = (GrowthStates)stateIndex;
        }  
    }

    private void HarvestState(int currentHour)
    {
        if(currentGrowthStates == GrowthStates.Harvesting)
        {
            return;
        }
        
        if(currentHour == daysUntilHarvest * 24)
        {
            currentGrowthStates = GrowthStates.Harvesting;
            EmitSignal(nameof(UpdateGrowthState));  
            EmitSignal(nameof(WheatHarvesting));
        }
    }
}
