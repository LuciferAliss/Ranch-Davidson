using Godot;

partial class ShowMessageManager : Node
{
    public static Tween ShowMessage(string text, Tween tween, Label label, SceneTree scene, Color color, float fadeInTime, float fadeOutTime)
    {
        if (tween != null && IsInstanceValid(tween))
        {
            tween.Kill();
            tween = null;
        }

        label.Text = text;
        label.Modulate = color; 

        tween = scene.CreateTween();
        tween.TweenProperty(label, "modulate", new Color(color.R , color.G, color.B, 1), fadeInTime);
        tween.TweenProperty(label, "modulate", new Color(color.R, color.G, color.B, 0), fadeOutTime);

        return tween;
    }
}