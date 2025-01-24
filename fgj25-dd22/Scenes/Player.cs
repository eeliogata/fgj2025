using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 600.0f;
	public const float JumpVelocity = -500.0f;
	
	public const float GravityMultiplier = 2f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * GravityMultiplier * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = velocity.MoveToward(new Vector2(direction.X * Speed, 0), 50f).X;
		}
		else
		{
			velocity.X = velocity.MoveToward(Vector2.Zero, 60f).X;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
