using Godot;
using System.Threading;
using System.Threading.Tasks;

public partial class bubblePlatform : Node2D
{

	[Export] private int numOfBubbles = 1;

	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
		var b = GD.Load<PackedScene>("res://entities/platforms/single_bubble.tscn");
		Node[] bubbles = new Node[numOfBubbles];
		CollisionShape2D c = GetNode<CollisionShape2D>("StaticBody2D/CollisionShape2D");
		c.Scale = new Vector2(numOfBubbles * 24, 1);
		c.Position = new Vector2(((float)numOfBubbles * 24f)/2f, 12);
		for (int i = 0; i < numOfBubbles; i++)
		{
			Node bi = b.Instantiate();
			AddChild(bi);
			bubbles[i] = bi;
			bi.CallDeferred(Node2D.MethodName.SetPosition, new Vector2(i * 24, 0));

		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
