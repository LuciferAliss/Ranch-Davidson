using Godot;
using System;
using System.Linq;

public partial class Settings : PanelContainer
{
	MainMenu mainMenu;
	Vector2I[] PermissionSize = new Vector2I[]
	{
		new Vector2I(1920, 1080),
		new Vector2I(1600, 900),
		new Vector2I(1280, 720)
	};

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void SetSettings(MainMenu mainMenu)
    {
        this.mainMenu = mainMenu;
    }

	private void CloseSetting()
	{
		this.Visible = false;
		if (mainMenu != null)
		{
			mainMenu.menu.Visible = true;
		}
	}

	public void LoadSetting()
    {
        using (var context = new SettingContext())
		{
			var buttonChengPermission = GetNode<OptionButton>("MarginContainer/HBoxContainer/VBoxContainer/PermissionContainer/ButtonChengPermission");
			var buttonChengScreenMode = GetNode<CheckBox>("MarginContainer/HBoxContainer/VBoxContainer/ScreenModeContainer/BorderButtonChengScreenMode/ButtonChengScreenMode");
			var sliderChengBrightness = GetNode<HSlider>("MarginContainer/HBoxContainer/VBoxContainer/BrightnessContainer/SliderChengBrightness");
			var SliderChengeMusic = GetNode<HSlider>("MarginContainer/HBoxContainer/VBoxContainer/MusicContainer/SliderChengeMusic");
			var SliderChengeEffects =GetNode<HSlider>("MarginContainer/HBoxContainer/VBoxContainer/EffectsContainer/SliderChengeEffects");

            var setting = context.Settings.FirstOrDefault(u => u.id == UserData.Instance.user.id);

			int indexPermission = Array.FindIndex(PermissionSize, p => 
			p.X == setting.permission.Split("x")[0].ToInt() && 
			p.Y == setting.permission.Split("x")[1].ToInt());

            buttonChengPermission.Selected = indexPermission;
            
			buttonChengScreenMode.ButtonPressed = setting.screenMode;

			sliderChengBrightness.Value = setting.Brightness;

			Vector2I screenSize = DisplayServer.ScreenGetSize();
			Vector2I windowSize = DisplayServer.WindowGetSize();
			Vector2I position = (screenSize - windowSize) / 2;
			DisplayServer.WindowSetPosition(position);

			SliderChengeMusic.Value = setting.music;
			SliderChengeEffects.Value = setting.effects;
		}
    }

	private void PermissionSelected(int id)
	{
		DisplayServer.WindowSetSize(PermissionSize[id]);
		SaveSettings();
	}

	private void ChangeScreenMode(bool Value)
	{	
		var ButtonChengPermission = GetNode<OptionButton>("MarginContainer/HBoxContainer/VBoxContainer/PermissionContainer/ButtonChengPermission");

		if(!Value)
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
			
			Vector2I screenSize = DisplayServer.ScreenGetSize();
			Vector2I windowSize = DisplayServer.WindowGetSize();
			Vector2I position = (screenSize - windowSize) / 2;
			DisplayServer.WindowSetPosition(position);
		}
		else 
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		}
		
		DisplayServer.WindowSetSize(PermissionSize[ButtonChengPermission.Selected]);
		SaveSettings();
	}

	private void ChangeGlobalBrightness(float ValueBrightness)
	{
		GlobalBrightness.Instance.ChangeBrightness(ValueBrightness);
		SaveSettings();
	}

	private void ChangeValueMusic(float valueMusic)
	{
		GlobalAudio.Instance.ChangeVolumeMusic(valueMusic);
		SaveSettings();
	}

	private void ChangeValueSFX(float valueSFX)
	{
		GlobalAudio.Instance.ChangeVolumeSoundEffects(valueSFX);
		SaveSettings();
	}

	private void SaveSettings()
	{
		using (var context = new SettingContext())
		{
			var buttonChengPermission = GetNode<OptionButton>("MarginContainer/HBoxContainer/VBoxContainer/PermissionContainer/ButtonChengPermission");
			var buttonChengScreenMode = GetNode<CheckBox>("MarginContainer/HBoxContainer/VBoxContainer/ScreenModeContainer/BorderButtonChengScreenMode/ButtonChengScreenMode");
			var sliderChengBrightness = GetNode<HSlider>("MarginContainer/HBoxContainer/VBoxContainer/BrightnessContainer/SliderChengBrightness");
			var SliderChengeMusic = GetNode<HSlider>("MarginContainer/HBoxContainer/VBoxContainer/MusicContainer/SliderChengeMusic");
			var SliderChengeEffects =GetNode<HSlider>("MarginContainer/HBoxContainer/VBoxContainer/EffectsContainer/SliderChengeEffects");

			var setting = context.Settings.FirstOrDefault(u => u.id == UserData.Instance.user.id);

			setting.permission = $"{PermissionSize[buttonChengPermission.Selected].X}x{PermissionSize[buttonChengPermission.Selected].Y}";
            setting.screenMode = buttonChengScreenMode.ButtonPressed;
            setting.Brightness = (float)sliderChengBrightness.Value;
            setting.music = (float)SliderChengeMusic.Value;
            setting.effects = (float)SliderChengeEffects.Value;;

            context.SaveChanges();
		}
	}

}
