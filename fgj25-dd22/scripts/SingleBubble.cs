using Godot;
using System;
using System.Threading;
using System.Threading.Tasks;

public partial class SingleBubble : Sprite2D
{
	[Export] private float Speed = 10.0f; // Speed of movement in units per second
	[Export] private Node2D _pointA;
	[Export] private Node2D _pointB;
	
	public enum bubbleType
	{
		Normal,
		Bouncy, 
		Ghost,
		Floating,
		
	}
	[Export]
	public bubbleType Type = bubbleType.Normal;
	private bool _movingToB = true;
	private bool isActive = false;

	public override void _Ready()
	{
		Random rnd = new Random();
		Task.Run(()=>
		{
			Thread.Sleep(rnd.Next(0, 70));
			Start();
		});
		switch (Type)
		{
			case bubbleType.Normal:
				Texture = GD.Load<Texture2D>("res://assets/sprites/Bubble-gray.png");
				break;
			case bubbleType.Bouncy:
				Texture = GD.Load<Texture2D>("res://assets/sprites/Bubble-pink.png");
				break;
		}
		
	}
	
	private void Start()
	{
		isActive = true;
	}

	public override void _Process(double delta)
	{
		if (_pointA == null || _pointB == null || !isActive)
			return;

		// Determine the target point
		Vector2 target = _movingToB ? _pointB.Position : _pointA.Position;

		// Move toward the target point smoothly
		float t = Mathf.Clamp(Position.DistanceTo(target) / _pointA.Position.DistanceTo(_pointB.Position), 0, 1);
		t = Mathf.Ease(t, 0.8f); // Adjust the second parameter for different easing (e.g., 0.5 for smooth)
		
		Position = Position.Lerp(target, t * (float)delta);
		// Check if the mover reached the target
		if ((_movingToB && Position.Y >= target.Y - 2) || (!_movingToB && Position.Y <= target.Y + 2))
		{
			_movingToB = !_movingToB; // Switch direction
		}
	}
}
