using Godot;
using System;
using System.Linq;

public partial class MainMenu : Control
{
    public Settings settings { get; private set; }
    public Menu menu { get; private set; }

    public override void _Ready()
    {
        Effect.Instance.PlayEffect("VignetteEffect_Close");

        settings = GetNode<Settings>("MarginContainer/settings");
        menu = GetNode<Menu>("MarginContainer/menu");

        menu.SetMenu(this);
        settings.SetSettings(this);

        menu.UpdateButton();

        LoadBGMainMenu();
        LoadSetting();
    }

    private void LoadBGMainMenu()
    {
        DateTime nowTime = DateTime.Now;
        TextureRect BackGround = GetNode<TextureRect>("BackGround");
        Texture2D texture;
        GD.Print(nowTime);

        if (nowTime.Hour > 5 && nowTime.Hour < 13)
        {
            texture = GD.Load<Texture2D>("res://resources/img/UI/MainMenu/Утро.png");
        }
        else if (nowTime.Hour > 12 && nowTime.Hour < 17)
        {
            texture = GD.Load<Texture2D>("res://resources/img/UI/MainMenu/День.png");
        }
        else if (nowTime.Hour > 16 && nowTime.Hour < 23)
        {
            texture = GD.Load<Texture2D>("res://resources/img/UI/MainMenu/Вечер.png");
        }
        else
        {
            texture = GD.Load<Texture2D>("res://resources/img/UI/MainMenu/Ночь.png");
        }

        BackGround.Texture = texture;
    }

    private void LoadSetting()
    {
        using (var context = new SettingContext())
		{
            var setting = context.Settings.FirstOrDefault(u => u.id == UserData.Instance.user.id);

            if(!setting.screenMode)
            {
			    DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
            }
            else 
            {
                DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
            }

            DisplayServer.WindowSetSize(new Vector2I(setting.permission.Split("x")[0].ToInt(), setting.permission.Split("x")[1].ToInt()));
            
            GlobalBrightness.Instance.ChangeBrightness(setting.Brightness);

            Vector2I screenSize = DisplayServer.ScreenGetSize();
            Vector2I windowSize = DisplayServer.WindowGetSize();
            Vector2I position = (screenSize - windowSize) / 2;
            DisplayServer.WindowSetPosition(position);
		}
    }
}
