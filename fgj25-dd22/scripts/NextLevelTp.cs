using Godot;
using System;

public partial class NextLevelTp : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	
	[Export] PackedScene nextLevel = null;
	
	public void _on_area_2d_body_entered(Node2D body)
	{
		
		if (body is Player)
		{
			Win win = GD.Load<PackedScene>("res://entities/common level things/win.tscn").Instantiate() as Win;
			win.nextLevel = nextLevel;
			foreach(Node n in GetNode("/root").GetChildren())
			{
				n.QueueFree();
			}
			GetNode("/root").AddChild(win);
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
