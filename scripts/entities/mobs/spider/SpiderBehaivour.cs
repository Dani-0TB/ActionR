using Godot;
using System;

public partial class SpiderBehaivour : Area2D
{
  private Timer _MoveTimer;
  private Vector2 Velocity = Vector2.Zero;
  private float Speed = 100;

  private float Friction = 10;
  AreaEnteredEventHandler areaEntered;
  public override void _Ready()
  {
    _MoveTimer = GetNode<Timer>("MoveTimer");
    _MoveTimer.WaitTime = GD.RandRange(2,3);
    _MoveTimer.Timeout += OnMoveTimerTimeout;
    _MoveTimer.Start();
    AreaEntered += _OnSpiderAreaEntered;
  }

  public override void _PhysicsProcess(double delta)
  {
    Velocity.X = Mathf.MoveToward(Velocity.X, 0, Friction);
		Velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Friction);
    Position += Velocity * (float) delta;
  }
  private void OnMoveTimerTimeout()
  {
    int degrees = GD.RandRange(0,365);
    Velocity = Vector2.Left.Rotated(Mathf.DegToRad(degrees)) * Speed;
    _MoveTimer.WaitTime = GD.Randf() * GD.RandRange(2,3);
    _MoveTimer.Start();
  }

  private void _OnSpiderAreaEntered(Area2D area)
  {
    QueueFree();
    area.QueueFree();
  }
}
