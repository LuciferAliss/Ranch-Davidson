using Godot;
using System;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;

public partial class Registration : CanvasLayer
{
	private LineEdit mailEdit;
	private LineEdit loginEdit;
	private LineEdit pswEdit;
	private LineEdit repPswEdit;

	public override void _Ready()
	{
		mailEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/mailEdit");
		loginEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/LoginEdit");
		pswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer4/PswEdit");
		repPswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/RepPswEdit");
		mailEdit.TextChanged += MailFilter;
		loginEdit.TextChanged += LoginFilter;
		pswEdit.TextChanged += PswFilter;
		repPswEdit.TextChanged += RepPswFilter;
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
		var Edit = GetNode<LineEdit>("MarginContainer/VBoxContainer/mailEdit");
		Regex NumberRegex = new Regex(@"[^-A-Za-z0-9.@_]");
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
		Regex NumberRegex = new Regex(@"[^a-zA-Z0-9._-]");
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
		Regex NumberRegex = new Regex(@"[^a-zA-Z0-9!@#\$%\^&\*\(\)_\+\-=\{\}\[\]:;'<>,\.\?\/\\\|\~]");
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
		Regex NumberRegex = new Regex(@"[^a-zA-Z0-9!@#\$%\^&\*\(\)_\+\-=\{\}\[\]:;'<>,\.\?\/\\\|\~]");
		int cursorPosition = Edit.CaretColumn;
        string filteredText = NumberRegex.Replace(newText, "");

		if (filteredText != newText)
        {
            Edit.Text = filteredText;
        }

		Edit.CaretColumn = cursorPosition;
	}

	private void SendsendConfirmationMessages(string toEmail)
	{
		GD.Print(mailEdit.Text);
		try
		{
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
				Body = Random.Shared.Next(100000, 1000000).ToString(),
				IsBodyHtml = false 
			};

			mail.To.Add(toEmail);
			smtpClient.Send(mail);
			GD.Print("Email успешно отправлен!");
		}
		catch (Exception ex)
		{
			GD.PrintErr($"Ошибка при отправке email: {ex.Message}");
		}
	}

	private void CreateAccount()
	{

		string toEmail = mailEdit.Text;
		SendsendConfirmationMessages(toEmail);
	}

}
