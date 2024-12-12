using Godot;
using System;

public partial class Effect : CanvasLayer
{
	public static Effect Instance { get; private set; }
	private AnimationPlayer playerAnim;
	ColorRect rect;		

	public override void _Ready()
	{
		Instance = this;

		playerAnim = GetNode<AnimationPlayer>("AnimationPlayer");
		rect = GetNode<ColorRect>("ColorRect");
		rect.Hide();
		playerAnim.AnimationFinished += AnimationFinished;
	}

	public override void _Process(double delta)
	{
	}

	public void PlayEffect(string Effect)
	{
		if (playerAnim.HasAnimation(Effect))
		{
			rect.Show();
			playerAnim.Play(Effect);
		}
	}

	private void AnimationFinished(StringName animName)
	{
		if  (animName.ToString().Split("_")[1] == "Close")
		{
			rect.Hide();
		}
	}
}
