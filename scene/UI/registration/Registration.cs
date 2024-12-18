using Godot;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
public partial class Registration : CanvasLayer
{
	private LineEdit mailEdit;
	private LineEdit loginEdit;
	private LineEdit pswEdit;
	private LineEdit repPswEdit;
	private OptionButton optionButton;
	private Tween tween;
	private LineEdit codeInput;
	private bool CheckingEmail = false;
	private string codeEmail = "";
	private Timer timerEmail;
	private Label timeEmailLabel;

	public override void _Ready()
	{
		mailEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer3/mailEdit");
		loginEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/LoginEdit");
		pswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer4/PswEdit");
		repPswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/RepPswEdit");
		optionButton = GetNode<OptionButton>("MarginContainer/VBoxContainer/HBoxContainer3/OptionButton");
		codeInput = GetNode<LineEdit>("MarginContainer/Panel/VBoxContainer/HBoxContainer/LineEdit");
		timerEmail = GetNode<Timer>("Timer");
		timeEmailLabel = GetNode<Label>("MarginContainer/Panel/VBoxContainer/HBoxContainer/Time");
		GetNode<TextureButton>("MarginContainer/Panel/VBoxContainer/TextureButton").Pressed += CheckCode;
		mailEdit.TextChanged += MailFilter;
		loginEdit.TextChanged += LoginFilter;
		pswEdit.TextChanged += PswFilter;
		repPswEdit.TextChanged += RepPswFilter;

		optionButton.Select(0);
		var error = GetNode<Label>("MarginContainer/VBoxContainer/error");
		error.Modulate = new Color(0.7f, 0, 0, 0);
		var errorCode = GetNode<Label>("MarginContainer/Panel/VBoxContainer/error");
		errorCode.Modulate = new Color(0.7f, 0, 0, 0);
    }

	public override void _Process(double delta)
	{
		if (timerEmail.IsStopped())
		{
			return;
		}

		timeEmailLabel.Text = TimeManager.FormatTimer(timerEmail.TimeLeft);
	}

	private void CloseConfirmationEmail()
	{
		GetNode<Panel>("MarginContainer/Panel").Visible = false;
	}

	private void TimeConfirmation()
	{
		codeEmail = "";
		GetNode<Panel>("MarginContainer/Panel").Visible = false;
		timeEmailLabel.Text = "";
		mailEdit.Text = "";
		loginEdit.Text = "";
		pswEdit.Text = "";
		repPswEdit.Text = "";
		var errorLabel = GetNode<Label>("MarginContainer/VBoxContainer/error");
		ShowMessageManager.ShowMessage("Время для подтверждения электронной почты истекло", tween, errorLabel, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
	}

	void ShowPsw(bool show)
	{
		var PswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer4/PswEdit");
		TextManager.ShowPsw(show, PswEdit);
	}

	void ShowRepPsw(bool show)
	{
		var PswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/RepPswEdit");
		TextManager.ShowPsw(show, PswEdit);
	}

	private void MailFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer3/mailEdit");
		Regex regex = new Regex(@"[^a-z-0-9._]");
		TextManager.TextFilter(newText, regex, Edit);
	}

	private void LoginFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/VBoxContainer/LoginEdit");
		Regex regex = new Regex(@"[^a-zA-Z0-9_-]");
		TextManager.TextFilter(newText, regex, Edit);
	}

	private void PswFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer4/PswEdit");
		Regex regex = new Regex(@"[^a-zA-Z0-9!@#\$_-]");
		TextManager.TextFilter(newText, regex, Edit);
	}

	private void RepPswFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/RepPswEdit");
		Regex regex = new Regex(@"[^a-zA-Z0-9!@#\$_%&*-]");
		TextManager.TextFilter(newText, regex, Edit);
	}

	private void CodeFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/Panel/VBoxContainer/HBoxContainer/LineEdit");
		Regex regex = new Regex(@"[^0-9]");
		TextManager.TextFilter(newText, regex, Edit);
	}

	private async void CreateAccount()
	{ 
		using (var context = new GameContext())
		{
			if (!CheckFidelity())
			{
				return;
			}

			var currentUser = new User
			{
				id = DataProcessingAndConversionManager.ToSHA512(loginEdit.Text + pswEdit.Text),
				login = loginEdit.Text,
				password = DataProcessingAndConversionManager.ToSHA512(pswEdit.Text),
				email = mailEdit.Text + optionButton.Text,
				pfp = DataProcessingAndConversionManager.ConvertImageToBlob("res://resources//img//UI//authorization//pfp.png")
			};

			using (var contextSetting = new SettingContext())
			{
				var settingCurrent = new SettingModel
				{
					id = DataProcessingAndConversionManager.ToSHA512(loginEdit.Text + pswEdit.Text),
					permission = "1280x720",
					screenMode = true,
					Brightness = 1.1f,
					music = 0,
					effects = 0
				};

				contextSetting.Settings.Add(settingCurrent);
				contextSetting.SaveChanges();
			}

			using (var contextSave = new SaveContext())
			{
				FileAccess file = FileAccess.Open("res://resources//db//baseSave.txt", FileAccess.ModeFlags.Read);
				var saveCurrent = new SaveModel
				{
					id = DataProcessingAndConversionManager.ToSHA512(loginEdit.Text + pswEdit.Text),
					Save = file.GetAsText()
				};

				contextSave.Save.Add(saveCurrent);
				contextSave.SaveChanges();
			}

			context.Users.Add(currentUser);
			context.SaveChanges();

			await Task.Delay(5000);

			ChangeSceneToAuthorization();
		}
	}

    public bool CheckFidelity()
	{
		using (var context = new GameContext())
		{
			var errorLabel = GetNode<Label>("MarginContainer/VBoxContainer/error");

			if (mailEdit.Text == "" || loginEdit.Text == "" || pswEdit.Text == "" || repPswEdit.Text == "")
			{
				ShowMessageManager.ShowMessage("Пожалуйста, заполните все поля", tween, errorLabel, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
				return false;
			}
			else if (loginEdit.Text.Length < 3)
			{
				ShowMessageManager.ShowMessage("Логин слишком короткий. Минимум 3 символов", tween, errorLabel, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
				return false;
			}
			else if (pswEdit.Text.Length < 8)
			{
				ShowMessageManager.ShowMessage("Пароль слишком короткий. Минимум 8 символов", tween, errorLabel, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
				return false;
			}	
			else if (context.Users.Any(u => u.login == loginEdit.Text))
			{
				ShowMessageManager.ShowMessage("Этот логин уже занята", tween, errorLabel, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
				return false;
			}
			else if (context.Users.Any(e => e.email == mailEdit.Text + optionButton.Text))
			{
				ShowMessageManager.ShowMessage("Эта почта уже занята", tween, errorLabel, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
				return false;
			}
			else if (!TextManager.EvaluatePasswordStrength(pswEdit.Text))
			{
				ShowMessageManager.ShowMessage("Слабый пароль. Добавьте разные регистры, цифры и спецсимволы", tween, errorLabel, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
				return false;
			}
			else if (!(repPswEdit.Text == pswEdit.Text))
			{
				ShowMessageManager.ShowMessage("Пароли не совпадают", tween, errorLabel, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
				return false;
			}
			else if (!EmailManager.IsDomainValid(mailEdit.Text + optionButton.Text))
			{
				ShowMessageManager.ShowMessage("Неверный адрес электронной почты", tween, errorLabel, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
				return false;
			}

			GetNode<Panel>("MarginContainer/Panel").Visible = true;

			if (CheckingEmail)
			{
				return true;
			}
			
			timerEmail.Start();

			codeEmail = Random.Shared.Next(100000, 1000000).ToString();
			EmailManager.SendConfirmationMessages(mailEdit.Text + optionButton.Text, "Потвержденние регистрации", codeEmail);
			
			return false;
		}
	}

	private void CheckCode()
    {
        string code = codeInput.Text;
		var LabelMessage = GetNode<Label>("MarginContainer/Panel/VBoxContainer/error");
        
		if (code == codeEmail)
        {
			ShowMessageManager.ShowMessage("Аккаунт был создан", tween, LabelMessage, GetTree(), new Color(0, 1, 0, 0), 1.0f, 3.0f);
			GetNode<TextureButton>("MarginContainer/Panel/VBoxContainer/TextureButton").Pressed -= CheckCode;
			CheckingEmail = true;
			CreateAccount();
		}
        else
        {
            ShowMessageManager.ShowMessage("Неверный код. Попробуйте снова", tween, LabelMessage, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
        }
    }

	private void ChangeSceneToAuthorization()
	{
		ManagerScene.ChangeScene(GetTree(), "res://scene//UI//authorization//authorization.tscn", "");	
	}
}