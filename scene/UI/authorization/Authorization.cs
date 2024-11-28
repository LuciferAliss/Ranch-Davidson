using Godot;
using System;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

public partial class Authorization : CanvasLayer
{
	
	private Tween currentTween1;
	private LineEdit loginEdit;
	private LineEdit pswEdit;
	private GameContext context = new GameContext();
	
	public override void _Ready()
	{
		loginEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/LoginEdit");
		pswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/PswEdit");
		GetNode<TextureButton>("MarginContainer/VBoxContainer/HBoxContainer2/TextureButton").Pressed += LoginToAccount;
	}

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

	private void LoginFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/VBoxContainer/LoginEdit");
		Regex NumberRegex = new Regex(@"[^a-zA-Z0-9_-]");
		int cursorPosition = Edit.CaretColumn;
        string filteredText = NumberRegex.Replace(newText, "");

		if (filteredText != newText)
        {
            Edit.Text = filteredText;
        }

		Edit.CaretColumn = cursorPosition;
	}

	private void PswFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer4/PswEdit");
		Regex NumberRegex = new Regex(@"[^a-zA-Z0-9!@#\$_-]");
		int cursorPosition = Edit.CaretColumn;
        string filteredText = NumberRegex.Replace(newText, "");

		if (filteredText != newText)
        {
            Edit.Text = filteredText;
        }

		Edit.CaretColumn = cursorPosition;
	}

	private void ChangeSceneToRegistration()
	{
		var newScene = (PackedScene)GD.Load("res://scene//UI//registration//Registration.tscn");
		var currentScene = GetTree().CurrentScene;
		var nextSceneInstance = newScene.Instantiate();

        GetTree().Root.AddChild(nextSceneInstance);
		GetTree().CurrentScene = nextSceneInstance;
        currentScene.QueueFree();
	}

	private async void LoginToAccount()
	{
		if (context.Users.Any(u => u.login == loginEdit.Text && u.password == SHA512Hash.ToSHA512(pswEdit.Text)))
		{
			GetNode<TextureButton>("MarginContainer/VBoxContainer/HBoxContainer2/TextureButton").Pressed -= LoginToAccount;
			ShowInfRegist("Вы удачно вошли");

			await Task.Delay(5000);
		}
		else
		{
			ShowError("Неправильный логин или пароль");
		}
	}

	private void ShowError(string errorText)
	{
		if (currentTween1 != null && IsInstanceValid(currentTween1))
		{
			currentTween1.Kill();
			currentTween1 = null;
		}

		var error = GetNode<Label>("MarginContainer/VBoxContainer/error");
		error.Text = errorText;
		error.Modulate = new Color(0.7f, 0, 0, 0);

		currentTween1 = GetTree().CreateTween();
		currentTween1.TweenProperty(error, "modulate", new Color(0.7f, 0, 0, 1), 1.0f);
		currentTween1.TweenProperty(error, "modulate", new Color(0.7f, 0, 0, 0f), 10.0f);
	}  

	private void ShowInfRegist(string Text)
	{
		var error = GetNode<Label>("MarginContainer/VBoxContainer/error");
		error.Text = Text;
		error.Modulate = new Color(0, 1, 0, 0);

		currentTween1 = GetTree().CreateTween();
		currentTween1.TweenProperty(error, "modulate", new Color(0, 1, 0, 1), 1.0f);
		currentTween1.TweenProperty(error, "modulate", new Color(0, 1, 0, 0f), 3.0f);
	}  
}
