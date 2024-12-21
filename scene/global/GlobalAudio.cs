using Godot;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

public partial class GlobalAudio : Node
{
	public static GlobalAudio Instance { get; private set; }
	public AudioStreamPlayer2D music;  
	private AudioStreamPlayer2D soundEffects;  

	public override void _Ready()
	{
		music = GetNode<AudioStreamPlayer2D>("OnTheFarmMusic");
		Instance = this;
	}

	public override void _Process(double delta)
	{
	}

	public void ChangeVolumeMusic(float volume)
	{
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), volume);
	}

	public void ChangeVolumeSoundEffects(float volume)
	{
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("SFX"), volume);
	}
}
