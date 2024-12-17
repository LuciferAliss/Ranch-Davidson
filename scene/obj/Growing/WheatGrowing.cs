using Godot;
using System;
using System.Threading.Tasks;

public partial class WheatGrowing : Plant
{
    PackedScene WheatScene = GD.Load<PackedScene>("res://scene//obj//ItemForScene//Wheat.tscn");
    private Sprite2D sprite2D;
    private GpuParticles2D wateringParticles;
    private GpuParticles2D floweringParticles;
    private GrowthCycleComponent growthCycleComponent;
    private HurtComponent hurtComponent1;
    private HurtComponent hurtComponent2;
    private CollisionShape2D hurtComponent2CollisionShape;

    public override void _Ready()
    {
        sprite2D = GetNode<Sprite2D>("Sprite2D");
        wateringParticles = GetNode<GpuParticles2D>("WateringParticles");
        floweringParticles = GetNode<GpuParticles2D>("FloweringParticles");
        growthCycleComponent = GetNode<GrowthCycleComponent>("GrowthCycleComponent");
        hurtComponent1 = GetNode<HurtComponent>("HurtComponent");
        hurtComponent2 = GetNode<HurtComponent>("HurtComponent2");
        hurtComponent2CollisionShape = GetNode<CollisionShape2D>("HurtComponent2/CollisionShape2D");

        floweringParticles.Emitting = false;
        wateringParticles.Emitting = false;

        growthCycleComponent.WheatHarvesting += OnWheatHarvesting;
        growthCycleComponent.UpdateGrowthState += UpdateGrowthState;
    
        sprite2D.Frame = (int)growthState;
    }

    public override void _Process(double delta)
    {
        if (growthState == GrowthStates.Harvesting)
        {
            floweringParticles.Emitting = true;
            if (floweringParticles.Emitting)
            {
                hurtComponent2CollisionShape.Disabled = false;
            }
        }
    }

    private async void OnHurt(int damage)
    {
        if(!isWatered && GrowthStates.Harvesting != growthState)
        {
            wateringParticles.Emitting = true;
            await Task.Delay(1600);
            wateringParticles.Emitting = false;
            isWatered = true;
        }
    }

    private void OnHurtHoe(int damage)
    {
        if (GrowthStates.Harvesting == growthState)
        {
            CallDeferred("AddWheatScene");
            QueueFree();
        }
    }

    private void AddWheatScene()
    {
        var WheatHarvestInstance = WheatScene.Instantiate() as Node2D;
        WheatHarvestInstance.GlobalPosition = GlobalPosition;
        GetParent().AddChild(WheatHarvestInstance);
    }

    private void OnWheatHarvesting()
    {
        hurtComponent2CollisionShape.Disabled = false;
    }

    private void UpdateGrowthState(int state)
    {
        growthState = (GrowthStates)state;
        sprite2D.Frame = (int)growthState;
    }
}

