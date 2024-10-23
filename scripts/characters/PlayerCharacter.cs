using Godot;
using System;
using GameProject;

public partial class PlayerCharacter : CharacterBase
{
	private Vector2 _mouse_position;
	private Vector2 _destination_position;

	public bool is_selected = false;
	public bool is_moving = false;

	public override void _Draw()
	{
		Color green = Colors.Green;
		Color blue = new Color("478cbf");
		Color grey = new Color("414042");
		Color transparent = new Color("0000008f");
		
		if(is_selected){
			DrawCircle(new Vector2(0, 10.0f), 25.0f, green);
			DrawCircle(new Vector2(0, 10.0f), 20.0f, transparent);
		}
		if(is_moving){
			DrawCircle(new Vector2(_destination_position[0], _destination_position[1]), 15.0f, blue);
			DrawCircle(new Vector2(_destination_position[0], _destination_position[1]), 10.0f, transparent);
		}
	}

	public override void _Ready()
	{
		NodePath Path = GetPath();
		GD.Print("Path of PlayerCharacter:", Path.ToString());
			
		_characterData = GetNode<CharacterData>("/root/GlobalCharacterData");
		
		_character_position = Position; // Set initial character position
		
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_selectedShape = GetNode<CollisionShape2D>("CollisionShape");
		SetInitialPosition(200, 200);

		// Adding three items to the inventory
		_characterData.PlayerInventory.AddItem(new Item(1, "Sample", "res://sword.png", 1, 1, false));
		_characterData.PlayerInventory.AddItem(new Item(2, "Banana", "res://shield.png", 1, 1, false));
		_characterData.PlayerInventory.AddItem(new Item(3, "Rotate", "res://potion.png", 5, 10, true));
		// Print inventory contents to console
		PrintInventoryContents();
	}



	public override void _Input(InputEvent @event)
	{
		
		if (is_selected)
		{
			if (@event.IsActionPressed("right_click"))
			{
				_mouse_position = GetGlobalMousePosition();
				_character_position = Position; // Initialize _character_position here
				
				if (_mouse_position[0] > _character_position[0])
				{
					_animatedSprite.FlipH = true;
				}
				else
				{
					_animatedSprite.FlipH = false;
				}
				
				is_moving = true;
			}
			if (@event.IsActionPressed("left_click"))
			{
				_selectedShape.Visible = false;
				is_selected = false;
				QueueRedraw();
			}
		}	
	}

	private void _on_area_2d_input_event(Node viewport, InputEvent @event, long shape_idx)
	{
		if (@event.IsActionPressed("left_click"))
		{
			_selectedShape.Visible = true;
			is_selected = true;
			QueueRedraw();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		//very not effective
		_character_position = Position;
		_destination_position = _mouse_position-_character_position;
		QueueRedraw();
		
		Velocity = Position.DirectionTo(_mouse_position) * Speed;
		if (is_moving)
		{
			QueueRedraw();
			Velocity = Position.DirectionTo(_mouse_position) * Speed;
			// LookAt(_mouse_position);
			if (Position.DistanceTo(_mouse_position) > 10)
			{
				MoveAndSlide();
			}
			else
			{
				is_moving = false;
			}
		}

		if (is_moving == true)
		{
			_animatedSprite.Play("walk");
		}
		else
		{
			_animatedSprite.Play("default");
		}

	}

	

}
