using Godot;
using System;

public partial class ArrowBehaivour : Area2D
{
  public Vector2 Direction = Vector2.Right;
  public float Speed = 100;
  public Vector2 Velocity = Vector2.Zero;
  public override void _PhysicsProcess(double delta)
  {
    if (Input.IsPhysicalKeyPressed(Key.Space))
    {
      QueueFree();
    }
    Velocity = Direction * Speed;
    Position += Velocity * (float) delta;
  }

  private void _OnLifeTimeout()
  {
    QueueFree();
  }
}
