using Godot;
using System;

public partial class FieldCursorComponent : Node
{
	[Export]
	TileMapLayer grassTileLayer;
	[Export]
	TileMapLayer roadTileLayer;
	[Export]
	TileMapLayer tiledSoilTileMapLayer;
	[Export]
	TileMapLayer objectTileMapLayer;
	[Export]
	TileMapLayer worldCollisionTileLayer;
	[Export]
	int terrainSet = 0;
	[Export]
	int terrain = 3;
	Vector2 mousePosition;
	Vector2I cellPosition;
	Vector2 localCellPosition;
	int roadCellSourceId;
	int cellSourceId;
	int worldCollisionSourceId;
	int objectSourceId;
	float distance;
	Player player;


	public override void _Ready()
	{
		var level = GetParent() as Level;
		player = level.GetNode<Player>("Player");
	}

	public override void _Process(double delta)
	{
	}

    public override void _UnhandledInput(InputEvent @event)
    {
		if (Input.IsActionJustPressed("Action"))
		{
			if (player.currentTools == ItemsName.ToolNames[3])
			{
				GetCellUnderMouse();
				AddTilledSoilCell();
			}
		}
    }

	private void GetCellUnderMouse()
	{
		mousePosition = grassTileLayer.GetLocalMousePosition();
		cellPosition = grassTileLayer.LocalToMap(mousePosition);
		cellSourceId = grassTileLayer.GetCellSourceId(cellPosition);
		roadCellSourceId = roadTileLayer.GetCellSourceId(cellPosition);
		worldCollisionSourceId = worldCollisionTileLayer.GetCellSourceId(cellPosition);
		objectSourceId = objectTileMapLayer.GetCellSourceId(cellPosition);
		localCellPosition = grassTileLayer.MapToLocal(cellPosition);
		distance = player.GlobalPosition.DistanceTo(localCellPosition);

		GD.Print($"mousePosition: {mousePosition}, cellPosition: {cellPosition}, cellSourceId: {cellSourceId}, localCellPosition: {localCellPosition}, distance: {distance}");
	}

	private void AddTilledSoilCell()
	{
		if (distance < 20f && cellSourceId != -1 && roadCellSourceId == -1 && objectSourceId == -1 && worldCollisionSourceId == -1)
		{
			tiledSoilTileMapLayer.SetCellsTerrainConnect(new Godot.Collections.Array<Vector2I>{ cellPosition }, terrainSet, terrain, true);
		}
	}
}
