using Godot;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

public partial class GlobalAudio : Node
{
	public static GlobalAudio Instance { get; private set; }
	public AudioStreamPlayer2D music;  
	private AudioStreamPlayer2D soundEffects;  
	private Dictionary<string, string> linkNameSound;

	public override void _Ready()
	{
		music = GetNode<AudioStreamPlayer2D>("OnTheFarmMusic");
		Instance = this;
		linkNameSound = new Dictionary<string, string>()
		{
			{"ButtonMapping", ""},
			{"ButtonPressed", ""},
		};
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

	public void PlaySoundEffects(string NameSoundEffects)
	{
		var audioStream = (AudioStream)GD.Load(linkNameSound[NameSoundEffects]);
        soundEffects.Stream = audioStream;
        soundEffects.Play();
	}

	public void PlayMusic(string NameMusic)
	{
		var audioStream = (AudioStream)GD.Load(linkNameSound[NameMusic]);
        music.Stream = audioStream;
        music.Play();
	}
}
