using Godot;
using System;
using GameProject;

public partial class PlayerCharacter : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 200;

	private Inventory _inventory = new Inventory();
	private Stats _stats = new Stats();

	private Vector2 _mousePosition;
	private Vector2 _destinationPosition;

	public bool IsSelected { get; set; } = false;
	public bool IsMoving { get; set; } = false;

	// Shapes
	private CollisionShape2D _selectedShape;
	private AnimatedSprite2D _animatedSprite;

	public override void _Draw()
	{
		Color green = Colors.Green;
		Color blue = new Color("478cbf");
		Color grey = new Color("414042");
		Color transparent = new Color("0000008f");

		if (IsSelected)
		{
			DrawCircle(new Vector2(0, 10.0f), 25.0f, green);
			DrawCircle(new Vector2(0, 10.0f), 20.0f, transparent);
		}

		if (IsMoving)
		{
			DrawCircle(new Vector2(_destinationPosition[0], _destinationPosition[1]), 15.0f, blue);
			DrawCircle(new Vector2(_destinationPosition[0], _destinationPosition[1]), 10.0f, transparent);
		}
	}

	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_selectedShape = GetNode<CollisionShape2D>("CollisionShape");

		_stats.Strength = 1;
		_stats.Perception = 2;
		_stats.Endurance = 3;
		_stats.Charisma = 4;
		_stats.Intelligence = 5;
		_stats.Agility = 6;
		_stats.Luck = 7;

		// Adding three items to the inventory
		_inventory.AddItem(new Item(1, "Sample", "res://sword.png", 1, 1, false));
		_inventory.AddItem(new Item(2, "Banana", "res://shield.png", 1, 1, false));
		_inventory.AddItem(new Item(3, "Rotate", "res://potion.png", 5, 10, true));
		// Print inventory contents to console
		PrintInventoryContents();
		// Accessing and modifying stats
		GD.Print($"Initial Strength: {_stats.Strength}");
		_stats.Strength += 5; // Increase Strength
		GD.Print($"Updated Strength: {_stats.Strength}");

		// Print stats to console
		PrintStats();
		NodePath playerCharacterPath = GetPath();
		GD.Print("Path of playerCharacter:", playerCharacterPath.ToString());
	}

	private void PrintInventoryContents()
	{
		GD.Print("Inventory contents:");
		foreach (var item in _inventory.GetItems())
		{
			GD.Print($"Item ID: {item.Id}, Name: {item.Name}");
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (IsSelected)
		{
			if (@event.IsActionPressed("right_click"))
			{
				_mousePosition = GetGlobalMousePosition();
				_destinationPosition = _mousePosition;

				if (_mousePosition[0] > Position[0])
				{
					_animatedSprite.FlipH = true;
				}
				else
				{
					_animatedSprite.FlipH = false;
				}

				IsMoving = true;
			}
			if (@event.IsActionPressed("left_click"))
			{
				_selectedShape.Visible = false;
				IsSelected = false;
				QueueRedraw();
			}
		}
	}

	private void _on_Area2D_InputEvent(Node viewport, InputEvent @event, long shape_idx)
	{
		if (@event.IsActionPressed("left_click"))
		{
			_selectedShape.Visible = true;
			IsSelected = true;
			QueueRedraw();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (IsMoving)
		{
			Vector2 direction = Position.DirectionTo(_mousePosition);
			Velocity = direction * Speed;
			MoveAndSlide();

			if (Position.DistanceTo(_mousePosition) <= 10)
			{
				IsMoving = false;
				Velocity = Vector2.Zero;
			}

			QueueRedraw();
		}

		if (IsMoving)
		{
			_animatedSprite.Play("walk");
		}
		else
		{
			_animatedSprite.Play("default");
		}
	}

	public Inventory GetInventory()
	{
		return _inventory;
	}

	public Stats GetStats()
	{
		return _stats;
	}

	private void PrintStats()
	{
		GD.Print("Current Stats:");
		GD.Print($"Strength: {_stats.Strength}");
		GD.Print($"Perception: {_stats.Perception}");
		GD.Print($"Endurance: {_stats.Endurance}");
		GD.Print($"Charisma: {_stats.Charisma}");
		GD.Print($"Intelligence: {_stats.Intelligence}");
		GD.Print($"Agility: {_stats.Agility}");
		GD.Print($"Luck: {_stats.Luck}");
	}
}
