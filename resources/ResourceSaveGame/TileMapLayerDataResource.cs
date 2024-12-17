using Godot;
using Godot.Collections;

partial class TileMapLayerDataResource : NodeDataResource
{
    [Export]
    public Array<Vector2I> tileMapLayerUsedCells;
    [Export]
    int terrainSet = 0;
    [Export]
    int terrain = 2;

    public override void SaveData(Node2D node)
    {
        base.SaveData(node);

        if (node is TileMapLayer tileMapLayer)
        {
            tileMapLayerUsedCells = tileMapLayer.GetUsedCells();
        }
    }

    public override void LoadData(Window window)
    {
        var sceneNode = window.GetNodeOrNull(nodePath);

        if (sceneNode is TileMapLayer tileMapLayer)
        {
            GD.Print($"{sceneNode.Name}\n{tileMapLayerUsedCells}");
            tileMapLayer.Clear();
            tileMapLayer.SetCellsTerrainConnect(tileMapLayerUsedCells, terrainSet, terrain, true);
        }
    }
} 