

using Godot;

public partial class Player : CharacterBody2D
{
	// Movement
	[Export] public float Speed = 200f;
	[Export] public float Acceleration = 8f;
	[Export] public float Deceleration = 6f;

	// Jumping
	[Export] public float JumpForce = 400f;
	[Export] public float MaxFallSpeed = 800f;
	[Export] public float Gravity = 1200f;

	// Variable Jump
	[Export] public float JumpCutMultiplier = 0.5f;

	// State
	private Vector2 _velocity;
	private bool _isJumping;
	public float bounceValue = -1f;
	public float prevFallValue = 0f;

	public override void _PhysicsProcess(double delta)
	{
		// Process movement
		ProcessMovement((float)delta);
		// Apply gravity
		ApplyGravity((float)delta);
		// Move the character
		Velocity = _velocity;
		MoveAndSlide();
	}

	private void ProcessMovement(float delta)
	{
		// Input for left/right movement
		float input = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");

		// Accelerate or decelerate
		if (input != 0)
		{
			_velocity.X = Mathf.Lerp(_velocity.X, input * Speed, Acceleration * delta);
		}
		else
		{
			_velocity.X = Mathf.Lerp(_velocity.X, 0, Deceleration * delta);
		}
		
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = _velocity.X > 0;

		// Jumping
		if (IsOnFloor() && Input.IsActionJustPressed("ui_accept"))
		{
			_velocity.Y = -JumpForce;
			_isJumping = true;
		}
		
		if(bounceValue > 0.1f)
		{
			_velocity.Y = -bounceValue;
			bounceValue -= 20f;
		}

		// Variable jump cut
		if (_isJumping && Input.IsActionJustReleased("ui_accept"))
		{
			if (_velocity.Y < 0)
			{
				_velocity.Y *= JumpCutMultiplier;
			}
			_isJumping = false;
		}
	}

	private void ApplyGravity(float delta)
	{
		// Apply gravity if not on the floor
		if (!IsOnFloor())
		{
			_velocity.Y += Gravity * delta;

			// Clamp fall speed
			if (_velocity.Y > MaxFallSpeed)
			{
				_velocity.Y = MaxFallSpeed;
			}
			prevFallValue = _velocity.Y;
		}
		else
		{
			// Reset vertical velocity when on the floor
			if (_velocity.Y > 0)
			{
				_velocity.Y = 0;
			}
		}
	}
}
