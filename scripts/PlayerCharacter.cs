using Godot;
using System;

public partial class PlayerCharacter : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 200;
	
	private Vector2 _target;
	
	public bool is_selected = false;
	public bool is_moving = false;
	
	public override void _Ready()
	{
		_target = Position; // Set initial target position to the character's starting position
	}
	
	public override void _Input(InputEvent @event)
	{
		if (is_selected)
		{
			if (@event.IsActionPressed("right_click"))
			{
				_target = GetGlobalMousePosition();
				is_moving = true;
			}
			if (@event.IsActionPressed("left_click"))
			{
				is_selected = false;
			}
		}	
	}
	private void _on_area_2d_input_event(Node viewport, InputEvent @event, long shape_idx)
	{
		if (@event.IsActionPressed("left_click"))
		{
			is_selected = true;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = Position.DirectionTo(_target) * Speed;
		// LookAt(_target);
		if (is_moving)
		{
			Velocity = Position.DirectionTo(_target) * Speed;
			// LookAt(_target);
			if (Position.DistanceTo(_target) > 10)
			{
				MoveAndSlide();
			}
			else
			{
				is_moving = false;
			}
		}
	}
}


