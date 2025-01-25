using Godot;
using System;

public partial class WordBoundary : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] Vector2 playerPos;
	public override void _Ready()
	{
	}
	
	public void _on_player_died(Node2D node)
	{
		node.EmitSignal("Died");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
