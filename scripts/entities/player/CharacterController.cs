using Godot;
using System;

public partial class CharacterController : CharacterBody2D
{
	public float Speed = 20;
	public float Friction = 5;
	public float MaxSpeed = 40;
	private AnimatedSprite2D _animatedSprite;
	
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
  public override void _PhysicsProcess(double delta)
	{
		Velocity = HandleMovement(GetInput());
		HandleAnimation();
		MoveAndSlide();
	}

	public Vector2 GetInput()
	{
		return Input.GetVector("left", "right", "up", "down");
	}

	public Vector2 HandleMovement(Vector2 input)
	{
		Vector2 velocity = Velocity;

		if (input.X != 0) 
		{
			velocity.X += input.X * Speed;
			velocity.Y = 0;
		}
		else if (input.Y !=0) 
		{
			velocity.X = 0;
			velocity.Y += input.Y * Speed;
		}

		velocity.X = Mathf.MoveToward(velocity.X, 0, Friction);
		velocity.Y = Mathf.MoveToward(velocity.Y, 0, Friction);

		velocity.X = Mathf.Clamp(velocity.X, -MaxSpeed, MaxSpeed);
		velocity.Y = Mathf.Clamp(velocity.Y, -MaxSpeed, MaxSpeed);

		return velocity;
	}

	public void HandleAnimation()
	{
		if (Velocity.X != 0)
		{
			_animatedSprite.Play("side");
			_animatedSprite.FlipH = Velocity.X < 0;
		}

		else if (Velocity.Y > 0) 
		{
			_animatedSprite.Play("front");
		}

		else if (Velocity.Y < 0)
		{
			_animatedSprite.Play("back");
		}

		else 
		{
			_animatedSprite.Stop();
		}
	}
}
