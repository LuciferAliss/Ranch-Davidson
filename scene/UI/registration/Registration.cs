using Godot;
using System;
using System.Text.RegularExpressions;

public partial class Registration : CanvasLayer
{
	public override void _Ready()
	{
		var mailEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/mailEdit");
		var loginEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/LoginEdit");
		var pswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer4/PswEdit");
		var repPswEdit = GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/RepPswEdit");
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
		
		Regex NumberRegex = new Regex(@"^[-\w.]+@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,4}$");
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
}
