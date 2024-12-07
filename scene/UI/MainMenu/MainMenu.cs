using Godot;
using System;

public partial class MainMenu : CanvasLayer
{
    public override void _Ready()
    {
        DateTime nowTime = DateTime.Now;
        Sprite2D BackGround = GetNode<Sprite2D>("BackGround");
        Texture2D texture;
        
        GD.Print(nowTime.Hour + " : "+ nowTime.Minute);

        if (nowTime.Hour > 6 && nowTime.Minute > 0 && nowTime.Hour < 13)
        {
            texture = GD.Load<Texture2D>("res://resources/img/UI/MainMenu/Утро.png");
        }
        else if (nowTime.Hour > 12 && nowTime.Minute > 0 && nowTime.Hour < 17)
        {
            texture = GD.Load<Texture2D>("res://resources/img/UI/MainMenu/День.png");
        }
        else if (nowTime.Hour > 16  && nowTime.Minute > 0 && nowTime.Hour < 23)
        {
            texture = GD.Load<Texture2D>("res://resources/img/UI/MainMenu/Вечер.png");
        }
        else
        {
            texture = GD.Load<Texture2D>("res://resources/img/UI/MainMenu/Ночь.png");
        }
        //17 : 38
        BackGround.Texture = texture;
    }
}
