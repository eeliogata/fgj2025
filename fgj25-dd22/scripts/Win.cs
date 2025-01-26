using Godot;
using System;

public partial class Win : Node2D
{
	// Called when the node enters the scene tree for the first time.
	
	[Export] public PackedScene nextLevel = null;
	public override void _Ready()
	{
		if(nextLevel == null)
		{
			GetNode<Button>("Label/Button").Visible = true;
			GetNode<Label>("Label").Text = "THANKS FOR PLAYING!";
		}
	}
	
	public void _on_next()
	{
		GetTree().ChangeSceneToPacked(nextLevel);
		QueueFree();
	}
	
	public void _on_menu()
	{
		GetTree().ChangeSceneToFile("res://menus/MainMenu.tscn");
		QueueFree();
	}
	
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
