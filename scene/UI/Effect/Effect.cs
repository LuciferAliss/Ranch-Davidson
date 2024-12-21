using Godot;
using System;

public partial class Effect : CanvasLayer
{
	public static Effect Instance { get; private set; }
	private AnimationPlayer playerAnim;
	private AnimatedSprite2D animSpryte;
	ColorRect rect;		

	public override void _Ready()
	{
		Instance = this;

		animSpryte = GetNode<AnimatedSprite2D>("Control/AnimatedSprite2D");
		animSpryte.Hide();
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
		if (Effect == "Save_")
		{
			animSpryte.Show();
			playerAnim.Play(Effect);
		}
		else if (playerAnim.HasAnimation(Effect))
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
		else if (animName == "Save_")
		{
			animSpryte.Hide();
		}
	}
}
