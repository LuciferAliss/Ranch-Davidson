using Godot;
using System;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Linq;

public partial class Authorization : CanvasLayer
{
	string Login = "";
	private OptionButton optionButton;
	private Tween currentTween1;
	private LineEdit loginEdit;
	private LineEdit loginEditRecoveryForm;
	private LineEdit pswEdit;
	private LineEdit mailEdit;
	private Timer timerEmail;
	private Label timeEmailLable;
	private string codeEmail = "";
	private LineEdit codeInput;
	private LineEdit pswEdit1;
	private LineEdit repPswEdit;

	public override void _Ready()
	{
		loginEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/LoginEdit");
		pswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/PswEdit");
		loginEditRecoveryForm = GetNode<LineEdit>("MarginContainer/FormEditEmail/VBoxContainer/LineEdit");
		mailEdit = GetNode<LineEdit>("MarginContainer/FormEditEmail/VBoxContainer/HBoxContainer3/mailEdit");
		timeEmailLable = GetNode<Label>("MarginContainer/FormconfirmationEmail/VBoxContainer/HBoxContainer/Time");
		codeInput = GetNode<LineEdit>("MarginContainer/FormconfirmationEmail/VBoxContainer/HBoxContainer/LineEdit");
		timerEmail = GetNode<Timer>("Timer");
		optionButton = GetNode<OptionButton>("MarginContainer/FormEditEmail/VBoxContainer/HBoxContainer3/OptionButton");
		pswEdit1 = GetNode<LineEdit>("MarginContainer/FormChangePsw/VBoxContainer/HBoxContainer4/PswEdit");
		repPswEdit = GetNode<LineEdit>("MarginContainer/FormChangePsw/VBoxContainer/HBoxContainer2/RepPswEdit");

		GetNode<TextureButton>("MarginContainer/VBoxContainer/HBoxContainer2/TextureButton").Pressed += LoginToAccount;
		GetNode<Button>("MarginContainer/FormEditEmail/VBoxContainer/Button3").Pressed += CloasePasswordRecoveryForm;
		loginEdit.TextChanged += LoginFilter;
		pswEdit.TextChanged += PswFilter;
		loginEditRecoveryForm.TextChanged += LoginFilterRecoveryForm;
		mailEdit.TextChanged += MailFilter;
		codeInput.TextChanged += CodeFilter;

		GetNode<Label>("MarginContainer/FormEditEmail/VBoxContainer/error").Modulate = new Color(0.7f, 0, 0, 0);
	}

	public override void _Process(double delta)
	{
		if (timerEmail.IsStopped())
		{
			return;
		}

		timeEmailLable.Text = FormatTimer(timerEmail.TimeLeft);
	}

	private void TimeConfirmation()
	{
		if (!GetNode<Panel>("MarginContainer/FormChangePsw").Visible)
		{
			codeEmail = "";
			GetNode<Panel>("MarginContainer/FormconfirmationEmail").Visible = false;
			GetNode<Panel>("MarginContainer/FormEditEmail").Visible = true;
			timeEmailLable.Text = "";
			mailEdit.Text = "";
			loginEditRecoveryForm.Text = "";
			ShowError("Время для подтверждения электронной почты истекло", "MarginContainer/FormconfirmationEmail/VBoxContainer/error");
		}
	}

	public string FormatTimer(double seconds)
	{
		int minutes = (int)Math.Floor(seconds / 60);
		int remainingSeconds = (int)Math.Floor(seconds % 60);
		return $"{minutes:D2}:{remainingSeconds:D2}";
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

	void ShowPsw1(bool show)
	{
		var PswEdit = GetNode<LineEdit>("MarginContainer/FormChangePsw/VBoxContainer/HBoxContainer4/PswEdit");
		if (show)
		{
			PswEdit.Secret = false;
		}
		else
		{
			PswEdit.Secret = true;
		}
	}

	void ShowPsw2(bool show)
	{
		var PswEdit = GetNode<LineEdit>("MarginContainer/FormChangePsw/VBoxContainer/HBoxContainer2/RepPswEdit");
		if (show)
		{
			PswEdit.Secret = false;
		}
		else
		{
			PswEdit.Secret = true;
		}
	}
	
	private void CodeFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/FormconfirmationEmail/VBoxContainer/HBoxContainer/LineEdit");
		Regex NumberRegex = new Regex(@"[^0-9]");
		int cursorPosition = Edit.CaretColumn;
        string filteredText = NumberRegex.Replace(newText, "");

		if (filteredText != newText)
        {
            Edit.Text = filteredText;
        }

		Edit.CaretColumn = cursorPosition;
	}

	private void MailFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/FormEditEmail/VBoxContainer/HBoxContainer3/mailEdit");
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

	private void LoginFilterRecoveryForm(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/FormEditEmail/VBoxContainer/LineEdit");
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
		var Edit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/PswEdit");
		Regex NumberRegex = new Regex(@"[^a-zA-Z0-9!@#\$_%&*-]");
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
		var newScene = (PackedScene)GD.Load("res://scene//UI//registration//registration.tscn");
		var currentScene = GetTree().CurrentScene;
		var nextSceneInstance = newScene.Instantiate();

        GetTree().Root.AddChild(nextSceneInstance);
		GetTree().CurrentScene = nextSceneInstance;
        currentScene.QueueFree();
	}

	private async void LoginToAccount()
	{
		using (var context = new GameContext())
		{
			if (context.Users.Any(u => u.login == loginEdit.Text && u.password == SHA512Hash.ToSHA512(pswEdit.Text)))
			{
				GetNode<TextureButton>("MarginContainer/VBoxContainer/HBoxContainer2/TextureButton").Pressed -= LoginToAccount;
				ShowInfRegist("Вы удачно вошли", "MarginContainer/VBoxContainer/error");

				await Task.Delay(5000);
			}
			else
			{
				ShowError("Неправильный логин или пароль", "MarginContainer/VBoxContainer/error");
			}
		}
	}

	private void ShowError(string errorText, string path)
	{
		if (currentTween1 != null && IsInstanceValid(currentTween1))
		{
			currentTween1.Kill();
			currentTween1 = null;
		}

		var error = GetNode<Label>(path);
		error.Text = errorText;
		error.Modulate = new Color(0.7f, 0, 0, 0);

		currentTween1 = GetTree().CreateTween();
		currentTween1.TweenProperty(error, "modulate", new Color(0.7f, 0, 0, 1), 1.0f);
		currentTween1.TweenProperty(error, "modulate", new Color(0.7f, 0, 0, 0), 10.0f);
	}  

	private void ShowInfRegist(string Text, string path)
	{
		if (currentTween1 != null && IsInstanceValid(currentTween1))
		{
			currentTween1.Kill();
			currentTween1 = null;
		}

		var error = GetNode<Label>(path);
		error.Text = Text;
		error.Modulate = new Color(0, 1, 0, 0);

		currentTween1 = GetTree().CreateTween();
		currentTween1.TweenProperty(error, "modulate", new Color(0, 1, 0, 1), 1.0f);
		currentTween1.TweenProperty(error, "modulate", new Color(0, 1, 0, 0f), 3.0f);
	}  

	private void OpenPasswordRecoveryForm()
	{
		GetNode<Panel>("MarginContainer/FormEditEmail").Visible = true;
		GetNode<Panel>("MarginContainer/FormconfirmationEmail").Visible = false;
		loginEdit.Text = "";
		pswEdit.Text = "";
		codeEmail = "";
	}

	private void CloasePasswordRecoveryForm()
	{
		GetNode<Panel>("MarginContainer/FormEditEmail").Visible = false;
		loginEditRecoveryForm.Text = "";
		mailEdit.Text = "";
	}

	private void OpenEmailConfirmationWindow()
	{	
		using (var context = new GameContext())
		{
			if (mailEdit.Text == "" || loginEditRecoveryForm.Text == "")
			{
				ShowError("Пожалуйста, заполните все поля", "MarginContainer/FormEditEmail/VBoxContainer/error");		
				return;
			}

			if (context.Users.Any(u => u.login == loginEditRecoveryForm.Text && u.email == mailEdit.Text + optionButton.Text))
			{
				Login = loginEditRecoveryForm.Text;
				GetNode<Panel>("MarginContainer/FormconfirmationEmail").Visible = true;
				SendConfirmationMessages(mailEdit.Text + optionButton.Text);
				timerEmail.Start();
			}	
			else
			{
				ShowError("Неверный логин или адрес электронной почты, ", "MarginContainer/FormEditEmail/VBoxContainer/error");		
				return;
			}
		}
	}

	private bool SendConfirmationMessages(string toEmail)
	{
		try
		{
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

	private void OpenFormChangePsw()
	{
		if (codeEmail == codeInput.Text)
		{
			GetNode<Panel>("MarginContainer/FormChangePsw").Visible = true;
			GetNode<Panel>("MarginContainer/FormconfirmationEmail").Visible = false;
			timerEmail.Stop();
		}
		else
		{
			ShowError("Неверный код. Попробуйте снова", "MarginContainer/FormconfirmationEmail/VBoxContainer/error");
		}
	}

	public bool EvaluatePasswordStrength(string password)
	{
		int score = 0;

		if (password.Any(char.IsLower)) score++;
		if (password.Any(char.IsUpper)) score++;
		if (password.Any(char.IsDigit)) score++; 
		if (password.Any(ch => !char.IsLetterOrDigit(ch))) score++; 

		if (score == 4)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private async void ChangePsw()
	{
		using (var context = new GameContext())
		{
			if (context.Users.Any(u => u.password == SHA512Hash.ToSHA512(pswEdit1.Text)))
			{
				ShowError("Пароль совпадант с прошлым", "MarginContainer/FormChangePsw/VBoxContainer/error");	
				return;
			}
			else if (pswEdit1.Text.Length < 8)
			{
				ShowError("Пароль слишком короткий. Минимум 8 символов", "MarginContainer/FormChangePsw/VBoxContainer/error");	
				return;
			}	
			else if (!EvaluatePasswordStrength(pswEdit1.Text))
			{
				ShowError("Слабый пароль. Добавьте разные регистры, цифры и спецсимволы", "MarginContainer/FormChangePsw/VBoxContainer/error");
				return;
			}
			else if (pswEdit1.Text != repPswEdit.Text)
			{
				ShowError("Пароли не совпадают", "MarginContainer/FormChangePsw/VBoxContainer/error");
				return;
			}

			var user = context.Users.FirstOrDefault(u => u.login == Login);
			user.password = SHA512Hash.ToSHA512(pswEdit1.Text);
			context.SaveChanges();
		
			ShowInfRegist("Пароль удачно изменен", "MarginContainer/FormChangePsw/VBoxContainer/error");

			await Task.Delay(5000);

			HidePanels();
		}
	}

	void HidePanels()
	{
		GetNode<Panel>("MarginContainer/FormChangePsw").Visible = false;
		GetNode<Panel>("MarginContainer/FormconfirmationEmail").Visible = false;
		GetNode<Panel>("MarginContainer/FormEditEmail").Visible = false;
		pswEdit1.Text = "";
		repPswEdit.Text = "";
	}
}
