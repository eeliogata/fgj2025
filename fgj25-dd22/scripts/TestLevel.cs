using Godot;
using System;

public partial class TestLevel : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void _on_death(Node player)
	{
		GetNode<Node2D>("Player").Position = new Vector2(96, 62);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
