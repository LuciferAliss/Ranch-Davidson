using Godot;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

public partial class Authorization : CanvasLayer
{
	string Login = "";
	private OptionButton optionButton;
	private Tween tween;
	private LineEdit loginEdit;
	private LineEdit loginEditRecoveryForm;
	private LineEdit pswEdit;
	private LineEdit mailEdit;
	private Timer timerEmail;
	private Label timeEmailLabel;
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
		timeEmailLabel = GetNode<Label>("MarginContainer/FormconfirmationEmail/VBoxContainer/HBoxContainer/Time");
		codeInput = GetNode<LineEdit>("MarginContainer/FormconfirmationEmail/VBoxContainer/HBoxContainer/LineEdit");
		timerEmail = GetNode<Timer>("Timer");
		optionButton = GetNode<OptionButton>("MarginContainer/FormEditEmail/VBoxContainer/HBoxContainer3/OptionButton");
		pswEdit1 = GetNode<LineEdit>("MarginContainer/FormChangePsw/VBoxContainer/HBoxContainer4/PswEdit");
		repPswEdit = GetNode<LineEdit>("MarginContainer/FormChangePsw/VBoxContainer/HBoxContainer2/RepPswEdit");

		GetNode<TextureButton>("MarginContainer/VBoxContainer/HBoxContainer2/TextureButton").Pressed += LoginToAccount;
		GetNode<Button>("MarginContainer/FormEditEmail/VBoxContainer/Button3").Pressed += CloasePasswordRecoveryForm;
		GetNode<TextureButton>("MarginContainer/FormChangePsw/VBoxContainer/TextureButton").Pressed += ChangePsw;
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

		timeEmailLabel.Text = TimeManager.FormatTimer(timerEmail.TimeLeft);
	}

	private void TimeConfirmation()
	{
		if (!GetNode<Panel>("MarginContainer/FormChangePsw").Visible)
		{
			codeEmail = "";
			GetNode<Panel>("MarginContainer/FormconfirmationEmail").Visible = false;
			GetNode<Panel>("MarginContainer/FormEditEmail").Visible = true;
			timeEmailLabel.Text = "";
			mailEdit.Text = "";
			loginEditRecoveryForm.Text = "";
			Label labelMassage = GetNode<Label>("MarginContainer/FormEditEmail/VBoxContainer/error");
			ShowMessageManager.ShowMessage("Время для подтверждения электронной почты истекло", tween, labelMassage, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f );
		}
	}

	void ShowPsw(bool show)
	{
		var PswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/PswEdit");
		TextManager.ShowPsw(show, PswEdit);
	}

	void ShowPsw1(bool show)
	{
		var PswEdit = GetNode<LineEdit>("MarginContainer/FormChangePsw/VBoxContainer/HBoxContainer4/PswEdit");
		TextManager.ShowPsw(show, PswEdit);
	}

	void ShowPsw2(bool show)
	{
		var PswEdit = GetNode<LineEdit>("MarginContainer/FormChangePsw/VBoxContainer/HBoxContainer2/RepPswEdit");
		TextManager.ShowPsw(show, PswEdit);
	}
	
	private void CodeFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/FormconfirmationEmail/VBoxContainer/HBoxContainer/LineEdit");
		Regex regex = new Regex(@"[^0-9]");
		TextManager.TextFilter(newText, regex, Edit);
	}

	private void MailFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/FormEditEmail/VBoxContainer/HBoxContainer3/mailEdit");
		Regex regex = new Regex(@"[^a-z-0-9._]");
		TextManager.TextFilter(newText, regex, Edit);
	}

	private void LoginFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/VBoxContainer/LoginEdit");
		Regex regex = new Regex(@"[^a-zA-Z0-9_-]");
		TextManager.TextFilter(newText, regex, Edit);
	}

	private void LoginFilterRecoveryForm(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/FormEditEmail/VBoxContainer/LineEdit");
		Regex regex = new Regex(@"[^a-zA-Z0-9_-]");
		TextManager.TextFilter(newText, regex, Edit);
	}

	private void PswFilter(string newText)
	{
		var Edit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/PswEdit");
		Regex regex = new Regex(@"[^a-zA-Z0-9!@#\$_%&*-]");
		TextManager.TextFilter(newText, regex, Edit);
	}

	private async void LoginToAccount()
	{
		using (var context = new GameContext())
		{
			Label labelMassage = GetNode<Label>("MarginContainer/VBoxContainer/error");
			if (context.Users.Any(u => u.login == loginEdit.Text && u.password == DataProcessingAndConversionManager.ToSHA512(pswEdit.Text)))
			{
				GetNode<TextureButton>("MarginContainer/VBoxContainer/HBoxContainer2/TextureButton").Pressed -= LoginToAccount;
				ShowMessageManager.ShowMessage("Вы удачно вошли", tween, labelMassage, GetTree(),  new Color(0, 1, 0, 0), 1.0f, 3.0f);

				User user = context.Users.FirstOrDefault(u => u.login ==  loginEdit.Text);
				UserData.Instance.SetUser(user);

				await Task.Delay(5000);

				using (var contextSave = new SaveContext())
				{
					var save = contextSave.Save.FirstOrDefault(u => UserData.Instance.user.id == u.id);
					if (save.Save == "" || save.Save == null)
					{
						UserData.Instance.haveSave = false;
					}
					else
					{
						UserData.Instance.haveSave = true;
					}
				}

				using (var contextStatistics = new StatisticsContext())
				{
					var Statistics = contextStatistics.Statistics.FirstOrDefault(u => UserData.Instance.user.id == u.id);
					UserData.Instance.NumberActions = Statistics.NumberActions;
					UserData.Instance.NumberDays = Statistics.NumberDays;
					UserData.Instance.NumberTreesCutDown = Statistics.NumberTreesCutDown;
					UserData.Instance.AmountWheatHarvested = Statistics.AmountWheatHarvested;
				}
				ManagerScene.ChangeScene(GetTree(), "res://scene/UI/MainMenu/MainMenu.tscn", "VignetteEffect_Open");
			}
			else
			{
				ShowMessageManager.ShowMessage("Неправильный логин или пароль", tween, labelMassage, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
			}
		}
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
			Label labelMassage = GetNode<Label>("MarginContainer/FormEditEmail/VBoxContainer/error");
			if (mailEdit.Text == "" || loginEditRecoveryForm.Text == "")
			{
				ShowMessageManager.ShowMessage("Пожалуйста, заполните все поля", tween, labelMassage, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
				return;
			}

			if (!context.Users.Any(u => u.login == loginEditRecoveryForm.Text && u.email == mailEdit.Text + optionButton.Text))
			{
				ShowMessageManager.ShowMessage("Неверный логин или адрес электронной почты", tween, labelMassage, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
				return;
				
			}	
			
			Login = loginEditRecoveryForm.Text;
			GetNode<Panel>("MarginContainer/FormconfirmationEmail").Visible = true;
			codeEmail = Random.Shared.Next(100000, 1000000).ToString();
			EmailManager.SendConfirmationMessages(mailEdit.Text + optionButton.Text, "Потвержденние регистрации", codeEmail);
			
			timerEmail.Start();
		}
	}

	private void OpenFormChangePsw()
	{
		if (codeEmail == codeInput.Text)
		{
			GetNode<Panel>("MarginContainer/FormChangePsw").Visible = true;
			GetNode<Panel>("MarginContainer/FormconfirmationEmail").Visible = false;
			loginEditRecoveryForm.Text = "";
			mailEdit.Text = "";
			timerEmail.Stop();
		}
		else
		{
			Label labelMassage = GetNode<Label>("MarginContainer/FormconfirmationEmail/VBoxContainer/error");
			ShowMessageManager.ShowMessage("Неверный код. Попробуйте снова", tween, labelMassage, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
;
		}
	}

	private async void ChangePsw()
	{
		using (var context = new GameContext())
		{
			Label labelMassage = GetNode<Label>("MarginContainer/FormChangePsw/VBoxContainer/error");
			if (context.Users.Any(u => u.password == DataProcessingAndConversionManager.ToSHA512(pswEdit1.Text)))
			{
				ShowMessageManager.ShowMessage("Пароль совпадант с прошлым", tween, labelMassage, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
				return;
			}
			else if (pswEdit1.Text.Length < 8)
			{
				ShowMessageManager.ShowMessage("Пароль слишком короткий. Минимум 8 символов", tween, labelMassage, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
				return;
			}	
			else if (!TextManager.EvaluatePasswordStrength(pswEdit1.Text))
			{
				ShowMessageManager.ShowMessage("Слабый пароль. Добавьте разные регистры, цифры и спецсимволы", tween, labelMassage, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
				return;
			}
			else if (pswEdit1.Text != repPswEdit.Text)
			{
				ShowMessageManager.ShowMessage("Пароли не совпадают", tween, labelMassage, GetTree(), new Color(0.7f, 0, 0, 0), 1.0f, 10.0f);
				return;
			}

			var user = context.Users.FirstOrDefault(u => u.login == Login);
			user.password = DataProcessingAndConversionManager.ToSHA512(pswEdit1.Text);
			context.SaveChanges();
		
			ShowMessageManager.ShowMessage("Пароль удачно изменен", tween, labelMassage, GetTree(), new Color(0, 1, 0, 0), 1.0f, 3.0f);
			
			GetNode<TextureButton>("MarginContainer/FormChangePsw/VBoxContainer/TextureButton").Pressed -= ChangePsw;
			
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
		loginEditRecoveryForm.Text = "";
		mailEdit.Text = "";
	}

	private void ChangeSceneToRegistration()
	{
		ManagerScene.ChangeScene(GetTree(), "res://scene//UI//registration//registration.tscn", "");
	}
}