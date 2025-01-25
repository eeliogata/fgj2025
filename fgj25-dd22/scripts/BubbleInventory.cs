using Godot;
using System;
using System.Collections.Generic;

public partial class BubbleInventory : Node2D
{
	// Called when the node enters the scene tree for the first time.

	[Export] public int amountOfSimple = 0;
	[Export] public int amountOfBouncy = 0;
	[Export] public int amountOfGhost = 0;
	[Export] public int amountOfFloating = 0;

	struct InventoryItem
	{
		public Button button;
		public Label label;
		public InventoryItem(string node, BubbleInventory t)
		{
			label = t.GetNode<Label>(node + "/Label");
			button = t.GetNode<Button>(node + "/Button");
		}
	}


	InventoryItem[] items = new InventoryItem[4];
	int[] nums = new int[4];

	List<Node> placedBubbles = new();

	public override void _Ready()
	{
		items[0] = new InventoryItem("Container/GridContainer/Basic", this);
		items[1] = new InventoryItem("Container/GridContainer/Bouncy", this);
		items[2] = new InventoryItem("Container/GridContainer/Ghost", this);
		items[3] = new InventoryItem("Container/GridContainer/Floating", this);
	}
	
	public void init()
	{
		nums[0] = amountOfSimple;
		nums[1] = amountOfBouncy;
		nums[2] = amountOfGhost;
		nums[3] = amountOfFloating;

		for (int i = 0; i < 4; i++)
		{
			items[i].label.Text = nums[i].ToString();
			items[i].button.Disabled = nums[i] == 0;

		}
	}
	
	public void _on_player_died()
	{
		placedBubbles.ForEach((b)=>
		{
			GetNode("/root").RemoveChild(b);
		});
		placedBubbles.Clear();
		init();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	int getSelectedIndex()
	{
		for (int i = 0; i < 4; i++)
		{
			if (items[i].button.ButtonPressed) return i;
		}
		return -1;
	}

	bool isClicked = false;

	public void _on_inventory_input_event(Node vp, InputEvent e, int id)
	{
		if (e is InputEventMouseButton)
		{
			InputEventMouseButton m = e as InputEventMouseButton;
			if (m.ButtonIndex == MouseButton.Left && m.Pressed && !isClicked)
			{
				int bubbId = getSelectedIndex();
				if(nums[bubbId] <= 0) return;
				bubblePlatform bp = GD.Load<PackedScene>("res://entities/platforms/bubblePlatform.tscn").Instantiate() as bubblePlatform;;
				bp.bubbleType = (SingleBubble.bubbleType)bubbId;
				GetNode("/root").AddChild(bp);
				placedBubbles.Add(bp);
				Vector2 pos = GetGlobalMousePosition();
				pos.X -= 72;
				pos.Y -= 24;
				bp.GlobalPosition = pos;
				nums[bubbId] -= 1;
				if(nums[bubbId] == 0)
				{
					items[bubbId].button.Disabled = true;
				}
				items[bubbId].label.Text = nums[bubbId].ToString();
				isClicked = true;
			}
			else if (!m.Pressed && m.ButtonIndex == MouseButton.Left && isClicked)
			{
				isClicked = false;
			}
		}
	}
}
