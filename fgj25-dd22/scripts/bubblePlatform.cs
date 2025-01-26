using Godot;
using System;
using System.Threading;
using System.Threading.Tasks;

public partial class bubblePlatform : Node2D
{

	[Export] private int numOfBubbles = 1;
	// Called when the node enters the scene tree for the first time.

	[Export]
	public SingleBubble.bubbleType bubbleType = SingleBubble.bubbleType.Normal;

	private Vector2 initPos;

	private bool reset = false;

	public override void _Ready()
	{
		var b = GD.Load<PackedScene>("res://entities/platforms/single_bubble.tscn");
		initPos = Position;
		Node[] bubbles = new Node[numOfBubbles];
		CollisionShape2D c = GetNode<CollisionShape2D>("StaticBody2D/CollisionShape2D");
		c.Scale = new Vector2(numOfBubbles * 2, 1);
		c.Position = new Vector2(((float)numOfBubbles * 24f) / 2f, 12);
		CollisionShape2D a = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
		a.Scale = new Vector2(numOfBubbles, 1);
		a.Position = new Vector2(((float)numOfBubbles * 24f) / 2f, 0);
		for (int i = 0; i < numOfBubbles; i++)
		{
			Node bi = b.Instantiate();
			(bi.GetChild(0) as SingleBubble).Type = bubbleType;
			AddChild(bi);
			bubbles[i] = bi;
			bi.CallDeferred(Node2D.MethodName.SetPosition, new Vector2(i * 24, 0));

		}
		if (bubbleType == SingleBubble.bubbleType.Ghost || bubbleType == SingleBubble.bubbleType.Floating)
		{
			try
			{
				(GetNode("../Player") as Player).Died += delegate ()
				{
					reset = true;
				};
			}
			catch (Exception) { }
		}
	}

	float opacity = 1f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (bubbleType == SingleBubble.bubbleType.Ghost)
		{
			if (disappear)
			{
				Modulate = new Color(255, 255, 255, opacity);
				opacity -= 0.06f;
			}
			if (opacity <= 0f)
			{
				CollisionShape2D c = GetNode<CollisionShape2D>("StaticBody2D/CollisionShape2D");
				CollisionShape2D a = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
				c.Disabled = a.Disabled = true;
				Modulate = new Color(255, 255, 255, 0);
				disappear = false;
				opacity = 1f;
			}
		}
		if (reset)
		{
			CollisionShape2D c = GetNode<CollisionShape2D>("StaticBody2D/CollisionShape2D");
			CollisionShape2D a = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
			c.Disabled = a.Disabled = false;
			Modulate = new Color(1, 1, 1, 1);
			disappear = false;
			Position = initPos;
			reset = false;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (bubbleType != SingleBubble.bubbleType.Floating) return;
		Position = new Vector2(Position[0], Position[1] - 1f);
	}

	bool hasBeenBoosted = false;
	bool disappear = false;

	public void _on_bounce(Node2D body)
	{
		Player b = body as Player;
		if (bubbleType == SingleBubble.bubbleType.Bouncy)
		{
			if (hasBeenBoosted)
			{
				b.bounceValue = b.prevFallValue * 0.8f;
			}
			if (!hasBeenBoosted)
			{
				b.bounceValue = b.prevFallValue * 1.5f;
				hasBeenBoosted = true;
			}
			if (b.bounceValue <= 50)
			{
				hasBeenBoosted = false;
			}
		}
		else if (body is Player && bubbleType == SingleBubble.bubbleType.Ghost)
		{
			disappear = true;
		}
	}
}
