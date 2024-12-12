using Godot;

public partial class IconSlot : TextureRect
{
    public bool IsDrag = false;

    public override void _Process(double delta)
    {
        if (IsDrag)
        {
            GlobalPosition = GetGlobalMousePosition();
        }
    }
}
