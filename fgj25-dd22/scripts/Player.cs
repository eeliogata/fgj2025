

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
	
	[Signal] public delegate void DiedEventHandler();

	// State
	private Vector2 _velocity;
	private bool _isJumping;
	public float bounceValue = -1f;
	public float prevFallValue = 0f;
	
	[Export] int amountOfSimple;
	[Export] int amountOfBouncy;
	[Export] int amountOfGhost;
	[Export] int amountOfFloating;
	
	[Export] Vector2 playerPos;

	public override void _Ready()
	{
		BubbleInventory bi = GetNode<BubbleInventory>("./BubbleInventory");
		bi.amountOfBouncy = amountOfBouncy;
		bi.amountOfFloating = amountOfFloating;
		bi.amountOfGhost = amountOfGhost;
		bi.amountOfSimple = amountOfSimple;
		bi.init();
	}
	
	public void _on_died()
	{
		Position = playerPos;
	}

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
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("walk");
		}
		else
		{
			_velocity.X = Mathf.Lerp(_velocity.X, 0, Deceleration * delta);
			if(!_isJumping)GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("default");
		}
		
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = _velocity.X > 0;

		// Jumping
		if (IsOnFloor() && Input.IsActionJustPressed("ui_accept"))
		{
			_velocity.Y = -JumpForce;
			_isJumping = true;
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("jump-up");
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
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("jump-down");
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
