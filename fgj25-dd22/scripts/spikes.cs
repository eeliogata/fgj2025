using Godot;
using System.Threading;
using System.Threading.Tasks;

public partial class spikes : Node2D
{

	[Export] private int numOfSpikes = 1;

	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
		var b = GD.Load<PackedScene>("res://entities/spikes/single_spike.tscn");
		Node[] spikes = new Node[numOfSpikes];
		CollisionShape2D c = GetNode<CollisionShape2D>("StaticBody2D/CollisionShape2D");
		c.Scale = new Vector2(numOfSpikes*2, 1);
		c.Position = new Vector2(((float)numOfSpikes * 24f)/2f, 10);
		CollisionShape2D a = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
		a.Scale = new Vector2(numOfSpikes*0.9f, 1);
		a.Position = new Vector2(((float)numOfSpikes * 24f)/2f, 0);
		for (int i = 0; i < numOfSpikes; i++)
		{
			Node bi = b.Instantiate();
			AddChild(bi);
			spikes[i] = bi;
			bi.CallDeferred(Node2D.MethodName.SetPosition, new Vector2(i * 24, 0));

		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
		
	public void _on_kill(Node2D body)
	{
		body.Position = new Vector2(96, 62);
	}

}
