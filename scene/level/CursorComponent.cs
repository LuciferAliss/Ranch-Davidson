using Godot;

public partial class CursorComponent : Node
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
	int terrain = 2;
	Vector2 mousePosition;
	Vector2I cellPosition;
	Vector2 localCellPosition;
	int roadCellSourceId;
	int cellSourceId;
	int worldCollisionSourceId;
	int objectSourceId;
	float distance;
	Player player;
	PackedScene WheatScene = GD.Load<PackedScene>("res://scene//obj//Growing//WheatGrowing.tscn"); 

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
		if (!player.cooldown)
		{
			if (Input.IsActionJustPressed("remove_dirt"))
			{
				player.cooldown = true;
				if (player.currentTools == ItemsName.ToolNames[3])
				{
					GetSoilCellUnderMouse();
					RemoveTilledSoilCell();
					RemoveCrop();
				}
			}
			else if (Input.IsActionJustPressed("Action"))
			{
				player.cooldown = true;
				if (player.currentTools == ItemsName.ToolNames[3])
				{
					GetSoilCellUnderMouse();
					AddTilledSoilCell();
				}
				else if (player.currentTools == ItemsName.ToolNames[4])
				{
					GetCropCellUnderMouse();
					AddCrop();
				}
			}
		}
    }

	private void GetSoilCellUnderMouse()
	{
		mousePosition = grassTileLayer.GetLocalMousePosition();
		cellPosition = grassTileLayer.LocalToMap(mousePosition);
		cellSourceId = grassTileLayer.GetCellSourceId(cellPosition);
		roadCellSourceId = roadTileLayer.GetCellSourceId(cellPosition);
		worldCollisionSourceId = worldCollisionTileLayer.GetCellSourceId(cellPosition);
		objectSourceId = objectTileMapLayer.GetCellSourceId(cellPosition);
		localCellPosition = grassTileLayer.MapToLocal(cellPosition);
		distance = player.GlobalPosition.DistanceTo(localCellPosition);
		GD.Print($"roadCellSourceId: {roadCellSourceId}, worldCollisionSourceId: {worldCollisionSourceId}, objectSourceId: {objectSourceId}, cellSourceId: {cellSourceId}");
	}
 
	private void GetCropCellUnderMouse()
	{
		mousePosition = tiledSoilTileMapLayer.GetLocalMousePosition();
		cellPosition = tiledSoilTileMapLayer.LocalToMap(mousePosition);
		cellSourceId = tiledSoilTileMapLayer.GetCellSourceId(cellPosition);
		localCellPosition = tiledSoilTileMapLayer.MapToLocal(cellPosition);
		roadCellSourceId = roadTileLayer.GetCellSourceId(cellPosition);
		worldCollisionSourceId = worldCollisionTileLayer.GetCellSourceId(cellPosition);
		objectSourceId = objectTileMapLayer.GetCellSourceId(cellPosition);
		distance = player.GlobalPosition.DistanceTo(localCellPosition);
		GD.Print($"roadCellSourceId: {roadCellSourceId}, worldCollisionSourceId: {worldCollisionSourceId}, objectSourceId: {objectSourceId}, cellSourceId: {cellSourceId}");
	}

	private void AddTilledSoilCell()
	{
		if (distance < 20f && cellSourceId != -1 && roadCellSourceId == -1 && objectSourceId == -1 && worldCollisionSourceId == -1)
		{
			tiledSoilTileMapLayer.SetCellsTerrainConnect(new Godot.Collections.Array<Vector2I>{ cellPosition }, terrainSet, terrain, true);
		}
	}

	private void RemoveTilledSoilCell()
	{
		if (distance < 20f)
		{
			bool CheckCrop = false;
			var crop_nodes = GetParent().FindChild("CropFields").GetChildren();
			foreach (Node2D node in crop_nodes)
			{
				if (node.GlobalPosition == localCellPosition)
				{
					CheckCrop = true;
					break;
				}
			}
			if (!CheckCrop)
			{
				tiledSoilTileMapLayer.SetCellsTerrainConnect(new Godot.Collections.Array<Vector2I>{ cellPosition }, 0, -1, true);
			}
		}
	}

	private void AddCrop()
	{
		if (distance < 20f && cellSourceId == 0 && roadCellSourceId == -1 && objectSourceId == -1 && worldCollisionSourceId == -1)
		{
			if (player.currentTools == ItemsName.ToolNames[4])
			{
				var WheatInstance = WheatScene.Instantiate() as Node2D;
				WheatInstance.GlobalPosition = localCellPosition;
				GetParent().FindChild("CropFields").AddChild(WheatInstance);
			}
		}
	}

	private void RemoveCrop()
	{
		if (distance < 20f)
		{
			var crop_nodes = GetParent().FindChild("CropFields").GetChildren();
			foreach (Node2D node in crop_nodes)
			{
				if (node.GlobalPosition == localCellPosition)
				{
					node.QueueFree();
					break;
				}
			}
		}
	}
}
