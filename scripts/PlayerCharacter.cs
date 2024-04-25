using Godot;
using System;

public partial class PlayerCharacter : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 200;
	
	private Vector2 _target;
	
	public bool is_selected = false;
	public bool is_moving = false;
	
	private Control outline;
	// Reference to the material with the shader
	private ShaderMaterial shaderMaterial;
	
	public override void _Ready()
	{
		_target = Position; // Set initial target position to the character's starting position
		
		// Access the material of the character sprite
		var spriteMaterial = GetNode<Sprite2D>("Sprite2D").Material as ShaderMaterial;
		if (spriteMaterial != null)
		{
			// Store the reference to the shader material
			shaderMaterial = spriteMaterial;
		}
		// Update the shader visibility based on selection
		if (shaderMaterial != null)
		{
			shaderMaterial.SetShaderParameter("line_thickness", is_selected ? 4 : 0);
		}
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
		// Update the shader visibility based on selection
		if (shaderMaterial != null)
		{
			shaderMaterial.SetShaderParameter("line_thickness", is_selected ? 4 : 0);
		}

	}
	

}


