using Godot;
using Godot.Collections;

partial class TileMapLayerDataResource : NodeDataResource
{
    [Export]
    public Array<Vector2I> tileMapLayerUsedCells;
    [Export]
    int terrainSet = 0;
    [Export]
    int terrain = 3;

    public override void SaveData(Node2D node)
    {
        base.SaveData(node);

        TileMapLayer tileMapLayer = node as TileMapLayer;
        
        tileMapLayerUsedCells = new Array<Vector2I>();
        
        tileMapLayerUsedCells = tileMapLayer.GetUsedCells();
    }

    public override void LoadData(Window window)
    {
        var sceneNode = window.GetNodeOrNull(nodePath);

        if (sceneNode != null)
        {
            TileMapLayer tileMapLayer = sceneNode as TileMapLayer;
            tileMapLayer.SetCellsTerrainConnect(tileMapLayerUsedCells, terrainSet, terrain, true);
        }   
    }
} 