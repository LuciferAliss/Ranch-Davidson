using System;

class TimeManager
{
    public static string FormatTimer(double seconds)
	{
		int minutes = (int)Math.Floor(seconds / 60);
		int remainingSeconds = (int)Math.Floor(seconds % 60);
		return $"{minutes:D2}:{remainingSeconds:D2}";
	}
}