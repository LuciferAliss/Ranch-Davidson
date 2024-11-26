using Godot;
using System;

public partial class Authorization : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void ShowPsw(bool show)
	{
		var PswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/PswEdit");
		if (show)
		{
			PswEdit.Secret = false;
		}
		else
		{
			PswEdit.Secret = true;
		}
	}
}
