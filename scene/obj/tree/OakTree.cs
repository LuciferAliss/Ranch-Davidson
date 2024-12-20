using Godot;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Xml.Resolvers;

public partial class OakTree : Sprite2D
{
	HurtComponent hurtComponent;
	DamageComponent damageComponent;
	PackedScene logScene = GD.Load<PackedScene>("res://scene//obj//ItemForScene//log.tscn");
	PackedScene appleScene = GD.Load<PackedScene>("res://scene/obj/ItemForScene/Tomato.tscn");

	public override void _Ready()
	{
		hurtComponent = GetNode<HurtComponent>("HurtComponent");
		damageComponent = GetNode<DamageComponent>("DamageComponent");

		hurtComponent.Hurt += OnHurt;
		damageComponent.MaxDamagedReached += OnMaxDamagedReached;
	}

	public override void _Process(double delta)
	{
	}

	public async void OnHurt(int Damage)
	{
		damageComponent.ApplyDamage(Damage);
		((ShaderMaterial)Material).SetShaderParameter("shake_intensity", 0.5);
		await ToSignal(GetTree().CreateTimer(1.0f), "timeout");
		((ShaderMaterial)Material).SetShaderParameter("shake_intensity", 0.0);
	}

	private void OnMaxDamagedReached()
	{
		CallDeferred("AddLogScene");
		GD.Print("Я dead inside");
		QueueFree();
	}

	private void AddLogScene()
	{
		var logInstance = logScene.Instantiate() as Node2D; 
		var appleInstance = appleScene.Instantiate() as Node2D;
		logInstance.GlobalPosition = GlobalPosition;
		Random random = new ();
		int x = random.Next(-15, 15);
		int y = random.Next(-15, 15);
		appleInstance.GlobalPosition = new Vector2(GlobalPosition.X + x, GlobalPosition.Y + y);
		GetParent().AddChild(logInstance);
		GetParent().AddChild(appleInstance);
	}
}
