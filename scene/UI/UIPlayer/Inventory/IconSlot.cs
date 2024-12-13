using Godot;

public partial class IconSlot : TextureRect
{
    public bool IsDrag = false;

    public override void _Process(double delta)
    {
        if (IsDrag)
        {
            ZIndex = 2;
            GlobalPosition = GetGlobalMousePosition();
        }
        else
        {
            ZIndex = 1;
        }
    }
}
