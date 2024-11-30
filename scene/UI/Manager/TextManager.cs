using Godot;
using System.Text.RegularExpressions;
using System.Linq;

partial class TextManager : Node
{
    public static void TextFilter(string newText, Regex regex, LineEdit Edit)
    {
		int cursorPosition = Edit.CaretColumn;
        string filteredText = regex.Replace(newText, "");

		if (filteredText != newText)
        {
            Edit.Text = filteredText;
        }

		Edit.CaretColumn = cursorPosition;
    }

    public static bool EvaluatePasswordStrength(string password)
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

    public static void ShowPsw(bool show, LineEdit edit)
    {
        if (show)
		{
			edit.Secret = false;
		}
		else
		{
			edit.Secret = true;
		}
    }
}