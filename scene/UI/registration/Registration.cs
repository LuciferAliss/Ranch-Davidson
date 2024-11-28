using Godot;
using System;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

public partial class Registration : CanvasLayer
{
	private LineEdit mailEdit;
	private LineEdit loginEdit;
	private LineEdit pswEdit;
	private LineEdit repPswEdit;
	private OptionButton optionButton;
	private GameContext context = new GameContext();
	private Tween currentTween1;
	private Tween currentTween2;
	private LineEdit codeInput;
	private Label messageLabel;
	private bool CheckingEmail = false;
	private string codeEmail = "";
	private System.Timers.Timer timer;

	public override void _Ready()
	{
		mailEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer3/mailEdit");
		loginEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/LoginEdit");
		pswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer4/PswEdit");
		repPswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/RepPswEdit");
		optionButton = GetNode<OptionButton>("MarginContainer/VBoxContainer/HBoxContainer3/OptionButton");
		codeInput = GetNode<LineEdit>("MarginContainer/Panel/VBoxContainer/LineEdit");
		GetNode<TextureButton>("MarginContainer/Panel/VBoxContainer/TextureButton").Pressed += CheckCode;
		mailEdit.TextChanged += MailFilter;
		loginEdit.TextChanged += LoginFilter;
		pswEdit.TextChanged += PswFilter;
		repPswEdit.TextChanged += RepPswFilter;

		optionButton.Select(0);
		var error = GetNode<Label>("MarginContainer/VBoxContainer/error");
		error.Modulate = new Color(0.588f, 0, 0, 0);
		var errorCode = GetNode<Label>("MarginContainer/Panel/VBoxContainer/error");
		errorCode.Modulate = new Color(0.588f, 0, 0, 0);
    }

	public override void _Process(double delta)
	{
	}

	void ShowPsw(bool show)
	{
		var PswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer4/PswEdit");
		if (show)
		{
			PswEdit.Secret = false;
		}
		else
		{
			PswEdit.Secret = true;
		}
	}

	void ShowRepPsw(bool show)
	{
		var PswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/RepPswEdit");
		if (show)
		{
			PswEdit.Secret = false;
		}
		else
		{
			PswEdit.Secret = true;
		}
	}

	private void MailFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer3/mailEdit");
		Regex NumberRegex = new Regex(@"[^a-z-0-9._]");
		int cursorPosition = Edit.CaretColumn;
        string filteredText = NumberRegex.Replace(newText, "");

		if (filteredText != newText)
        {
            Edit.Text = filteredText;
        }

		Edit.CaretColumn = cursorPosition;
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

	private void RepPswFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/RepPswEdit");
		Regex NumberRegex = new Regex(@"[^a-zA-Z0-9!@#\$_-]");
		int cursorPosition = Edit.CaretColumn;
        string filteredText = NumberRegex.Replace(newText, "");

		if (filteredText != newText)
        {
            Edit.Text = filteredText;
        }

		Edit.CaretColumn = cursorPosition;
	}

	private bool SendConfirmationMessages(string toEmail)
	{
		try
		{
			if (CheckingEmail == true)
			{
				return true;
			}
			codeEmail = Random.Shared.Next(100000, 1000000).ToString();
			SmtpClient smtpClient = new SmtpClient("smtp.mail.ru")
			{
				Port = 587,
				Credentials = new NetworkCredential("alissentertainment@mail.ru", "ate5yzgzyP4bMB7KfJ0K"),
				EnableSsl = true,
			};

			MailMessage mail = new MailMessage
			{
				From = new MailAddress("alissentertainment@mail.ru", "Admin"),
				Subject = "Потвержденние регистрации",
				Body = codeEmail,
				IsBodyHtml = false 
			};

			mail.To.Add(toEmail);
			smtpClient.Send(mail);
			
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	private bool CheckPasswords()
	{
		return repPswEdit.Text == pswEdit.Text;
	}

	private async void CreateAccount()
	{ 
		if (!CheckFidelity())
		{
			return;
		}

		GD.Print(ConvertImageToBlob("res://resources//img//UI//authorization//pfp.png"));
		var currentUser = new User
		{
			id = ToSHA512(loginEdit.Text + pswEdit.Text),
			login = loginEdit.Text,
			password = ToSHA512(pswEdit.Text),
			email = mailEdit.Text,
			pfp = ConvertImageToBlob("res://resources//img//UI//authorization//pfp.png")
		};
		
		context.Users.Add(currentUser);
		context.SaveChanges();

		 await Task.Delay(5000);

		ChangeSceneToAuthorization();
	}

	public bool CheckFidelity()
	{
		if (mailEdit.Text == "" || loginEdit.Text == "" || pswEdit.Text == "" || repPswEdit.Text == "")
		{
			ShowError("Пожалуйста, заполните все поля");		
			return false;
		}
		else if (!CheckPasswords())
		{
			ShowError("Пароли не совпадают");		
			return false;
		}
		else if (context.Users.Any(u => u.login == loginEdit.Text))
		{
			ShowError("Этот логин уже занята");		
			return false;
		}
		else if (context.Users.Any(e => e.email == mailEdit.Text + optionButton.Text))
		{
			ShowError("Эта почта уже занята");		
			return false;
		}
		else if (!SendConfirmationMessages(mailEdit.Text + optionButton.Text))
		{
			ShowError("Неверный адрес электронной почты");		
			return false;
		}

		GetNode<Panel>("MarginContainer/Panel").Visible = true;

		if (CheckingEmail)
		{
			timer.Stop();
			return true;
		}
		timer = new System.Timers.Timer(120000);
		timer.Elapsed += (sender, e) =>
		{
			timer.Stop();
			GD.Print("Таймер завершён");
		};
		timer.AutoReset = false;
		timer.Start();
		
		
		return false;
	}

	private static string ToSHA512(string inputMessage)
	{
		string outputMessage = "";
		using (SHA512 sha512 = SHA512.Create())
		{
			byte[] toBytes = Encoding.ASCII.GetBytes(inputMessage);
			byte[] hash = sha512.ComputeHash(toBytes);

			foreach (byte b in hash)
			{
				outputMessage += b.ToString("x2");
			}

			return outputMessage;
		}
	}

	private static byte[] ConvertImageToBlob(string filePath)
    {
        string extension = filePath.GetExtension().ToLower();
        Image image = new Image();
		image.Load(filePath);

        switch (extension)
        {
            case "png":
                return image.SavePngToBuffer();

            case "jpg":
            case "jpeg":
                return image.SaveJpgToBuffer(90);

            case "webp":
                return image.SaveWebpToBuffer(false);

            default:
                GD.PrintErr($"Формат {extension} не поддерживается.");
                return null;
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
		error.Modulate = new Color(0.588f, 0, 0, 0);

		currentTween1 = GetTree().CreateTween();
		currentTween1.TweenProperty(error, "modulate", new Color(0.588f, 0, 0, 1), 1.0f);
		currentTween1.TweenProperty(error, "modulate", new Color(0.588f, 0, 0, 0f), 10.0f);
	}  

	private void ShowErrorCode(string errorText)
	{
		if (currentTween2 != null && IsInstanceValid(currentTween2))
		{
			currentTween2.Kill();
			currentTween2 = null;
		}

		var error = GetNode<Label>("MarginContainer/Panel/VBoxContainer/error");
		error.Text = errorText;
		error.Modulate = new Color(0.588f, 0, 0, 0);

		currentTween2 = GetTree().CreateTween();
		currentTween2.TweenProperty(error, "modulate", new Color(0.588f, 0, 0, 1), 1.0f);
		currentTween2.TweenProperty(error, "modulate", new Color(0.588f, 0, 0, 0f), 10.0f);
	}  

	private void ShowInfRegist(string Text)
	{
		var error = GetNode<Label>("MarginContainer/Panel/VBoxContainer/error");
		error.Text = Text;
		error.Modulate = new Color(0, 1, 0, 0);

		currentTween2 = GetTree().CreateTween();
		currentTween2.TweenProperty(error, "modulate", new Color(0, 1, 0, 1), 1.0f);
		currentTween2.TweenProperty(error, "modulate", new Color(0, 1, 0, 0f), 3.0f);
	}  

	private void CheckCode()
    {
        string code = codeInput.Text;

        if (code == codeEmail)
        {
			ShowInfRegist("Аккаунт был создан");
			GetNode<TextureButton>("MarginContainer/Panel/VBoxContainer/TextureButton").Pressed -= CheckCode;
			CheckingEmail = true;
			CreateAccount();
		}
        else
        {
            ShowErrorCode("Неверный код. Попробуйте снова");
        }
    }

	private void ChangeSceneToAuthorization()
	{
		GetTree().ChangeSceneToFile("res://scene//UI//authorization//Authorization.tscn");
	}
}