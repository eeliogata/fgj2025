using Godot;

public partial class PlatformBouncy : Node2D
{
	[Export] public float BaseBounceStrength = 8000f; // Base upward bounce strength
	[Export] public Vector2 BounceDirection = Vector2.Up; // Default bounce direction
	[Export] public bool ScaleWithVelocity = true; // Whether the bounce scales with the player's impact velocity

	public override void _Ready()
	{
		// Normalize the bounce direction for consistent results
		BounceDirection = BounceDirection.Normalized();
	}

	private void _body_entered(Node2D body)
	{
		// Check if the body is a CharacterBody2D (or similar player object)
		if (body is CharacterBody2D player)
		{
			GD.Print("alma");
			// Get the player's current velocity
			Vector2 playerVelocity = player.Velocity;

			// Calculate the bounce strength
			float adjustedBounceStrength = BaseBounceStrength;

			if (ScaleWithVelocity)
			{
				// Scale the bounce based on the player's downward velocity
				float impactFactor = Mathf.Max(1.0f, -playerVelocity.Y / BaseBounceStrength);
				adjustedBounceStrength *= impactFactor;
			}

			// Apply the bounce in the desired direction
			Vector2 bounceVelocity = BounceDirection * adjustedBounceStrength;

			// Retain any horizontal velocity the player had, if the bounce direction is vertical
			if (BounceDirection == Vector2.Up || BounceDirection == Vector2.Down)
			{
				bounceVelocity.X = playerVelocity.X;
			}

			// Update the player's velocity
			player.Velocity = bounceVelocity;
		}
	}
}
