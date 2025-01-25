using Godot;
using System;

public partial class BubbleInventory : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	bool isClicked = false;
	public void _on_inventory_input_event(Node vp, InputEvent e, int id)
	{
		if (e is InputEventMouseButton)
		{
			InputEventMouseButton m = e as InputEventMouseButton;
			if (m.ButtonIndex == MouseButton.Left && m.Pressed && !isClicked)
			{
				bubblePlatform bp = GD.Load<PackedScene>("res://entities/platforms/bubblePlatform.tscn").Instantiate() as bubblePlatform;
				GetNode("/root").AddChild(bp);
				Vector2 pos = GetGlobalMousePosition();
				pos.X -= 72;
				pos.Y -= 24;
				bp.GlobalPosition = pos;
				isClicked = true;
			}
			else if(!m.Pressed && m.ButtonIndex == MouseButton.Left && isClicked)
			{
				isClicked = false;
			}
		}
	}
}
