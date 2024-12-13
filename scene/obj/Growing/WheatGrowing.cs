using Godot;
using System;
using System.Threading.Tasks;

public partial class WheatGrowing : Node2D
{
    PackedScene WheatScene = GD.Load<PackedScene>("res://scene//obj//ItemForScene//Wheat.tscn");
    private Sprite2D sprite2D;
    private GpuParticles2D wateringParticles;
    private GpuParticles2D floweringParticles;
    private GrowthCycleComponent growthCycleComponent;
    private HurtComponent hurtComponent;
    private GrowthStates growthState = GrowthStates.Seed;  

    public override void _Ready()
    {
        sprite2D = GetNode<Sprite2D>("Sprite2D");
        wateringParticles = GetNode<GpuParticles2D>("WateringParticles");
        floweringParticles = GetNode<GpuParticles2D>("FloweringParticles");
        growthCycleComponent = GetNode<GrowthCycleComponent>("GrowthCycleComponent");
        hurtComponent = GetNode<HurtComponent>("HurtComponent");

        floweringParticles.Emitting = false;
        wateringParticles.Emitting = false;
    }

    public override void _Process(double delta)
    {
        growthState = growthCycleComponent.GetCurrentGrowthState();
        sprite2D.Frame = (int)growthState;

        if (growthState == GrowthStates.Maturity)
        {
            floweringParticles.Emitting = true;
        }
    }

    private async void OnHurt(int damage)
    {
        if(!growthCycleComponent.isWatered)
        {
            wateringParticles.Emitting = true;
            await Task.Delay(3000);
            wateringParticles.Emitting = false;
            growthCycleComponent.isWatered = true;
        }
    }

    private void OnWheatMaturity()
    {
        floweringParticles.Emitting = true;
    }
}

