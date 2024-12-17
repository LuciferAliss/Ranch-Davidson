using Godot;
using System;

public partial class DayNightCycleComponent : CanvasModulate
{
    public int initialDay = 1;
    public int initialHour = 9;
    public int initialMinute = 0;

    private int InitialDay
    {
        get => initialDay;
        set
        {
            initialDay = value;
            DayAndNightCycleManager.Instance.InitialDay = value;
            DayAndNightCycleManager.Instance.SetInitialTime();
        }
    }

    private int InitialHour
    {
        get => initialHour;
        set
        {
            initialHour = value;
            DayAndNightCycleManager.Instance.InitialHour = value;
            DayAndNightCycleManager.Instance.SetInitialTime();
        }
    }

    private int InitialMinute
    {
        get => initialMinute;
        set
        {
            initialMinute = value;
            DayAndNightCycleManager.Instance.InitialMinute = value;
            DayAndNightCycleManager.Instance.SetInitialTime();
        }
    }

    GradientTexture1D DayNightGradientTexture;

    public override void _Ready()
    {
        DayAndNightCycleManager.Instance.InitialDay = initialDay;
        DayAndNightCycleManager.Instance.InitialHour = initialHour;
        DayAndNightCycleManager.Instance.InitialMinute = initialMinute;
        DayAndNightCycleManager.Instance.SetInitialTime();

        DayAndNightCycleManager.Instance.GameTime += OnGameTime;

        Gradient gradient = new Gradient();
        gradient.AddPoint(0.0f, new Color(0.1f, 0.2f, 0.8f));
        gradient.AddPoint(0.04f, new Color(0, 0.5f, 1));
        gradient.AddPoint(0.23f, new Color(1, 0.6f, 0));
        gradient.AddPoint(0.3f, new Color(1, 0.8f, 0.8f));
        gradient.AddPoint(0.5f, new Color(1, 1, 0.8f));
        gradient.AddPoint(1.0f, new Color(1, 1, 1));

        DayNightGradientTexture = new GradientTexture1D
        {
            Gradient = gradient
        };
    }

    private void OnGameTime(float Time)
    {
        float SampleValue = 0.5f * (float)(Math.Sin(Time - Math.PI * 0.5f) + 1f);
        Color = DayNightGradientTexture.Gradient.Sample(SampleValue);
    }

    public void SetTimeWorld(int[] timeWorld)
    {
        InitialMinute = timeWorld[0];
        InitialHour = timeWorld[1];
        InitialDay = timeWorld[2];
    }

    public override void _ExitTree()
    {
        DayAndNightCycleManager.Instance.GameTime -= OnGameTime;
    }
}
