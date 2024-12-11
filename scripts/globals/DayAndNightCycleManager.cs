using System;
using Godot;

partial class DayAndNightCycleManager : Node
{
    const int MinutesPerDay = 24 * 60;
    const int MinutesPerHour = 60;
    const float GameMinuteDuration = (float)(2 * Math.PI) / MinutesPerDay;

    public float GameSpeed = 2f;

    int InitialDay = 1;
    int InitialHour = 12;
    int InitialMinutes = 30;

    float Time = 0;
    int CurrentMinute = -1;
    int CurrentDay = 0;

    bool gameStart = false;

    [Signal]
    public delegate void GameTimeEventHandler(float time);
    [Signal]
    public delegate void TimeTickEventHandler(int day, int hour, int minute);
    [Signal]
    public delegate void TimeTickDayEventHandler(int day);

    public static DayAndNightCycleManager Instance { get; private set; }

    public override void _Ready()
    {
        GameStart.Instance.SignalGameStart += CheckGameStart;
        Instance = this;
        SetInitialTime();
    }

    public override void _Process(double delta)
    {
        if (gameStart)
        {
            Time += (float)delta * GameSpeed * GameMinuteDuration;
            EmitSignal(nameof(GameTime), Time);
            
            RecalculateTime();
            GD.Print("fgew");
        }
    }

    private void CheckGameStart(bool value)
    {
        gameStart = value;
    } 

    private void SetInitialTime()
    {
        int initialTotalMinute = InitialDay * MinutesPerDay + (InitialHour * MinutesPerHour) + InitialMinutes;
        Time = initialTotalMinute * GameMinuteDuration;
    }

    private void RecalculateTime()
    {
        int totalMinute = (int)(Time / GameMinuteDuration);
        int day = totalMinute / MinutesPerDay;
        int currentDayMinutes = totalMinute % MinutesPerDay;
        int hour = currentDayMinutes / MinutesPerHour;
        int minute = currentDayMinutes % MinutesPerHour;

        if (CurrentMinute != minute)
        {
            CurrentMinute = minute;
            EmitSignal(nameof(TimeTick), day, hour, minute);
        }

        if (CurrentDay != day)
        {
            CurrentDay = day;
            EmitSignal(nameof(TimeTickDay), day);
        }
    }
}