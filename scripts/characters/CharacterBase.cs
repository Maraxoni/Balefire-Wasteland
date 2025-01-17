using Godot;
using System;
using Godot.Collections;

public partial class CharacterBase : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 100;
	
	protected Vector2 _character_position;
	protected CharacterData _characterData;
	protected CollisionShape2D _selectedShape;
	protected AnimatedSprite2D _animatedSprite;
	
	// Patrol points and patrol logic
	[Export]
	protected Array<Vector2> PatrolPoints = new Array<Vector2>();
	protected int _currentPatrolIndex = 0;
	protected bool _isPatrolling = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NodePath Path = GetPath();
		GD.Print("Path of Character:", Path.ToString());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	public override void _PhysicsProcess(double delta)
	{

	}
	
	protected void Patrol(double delta)
	{
		Vector2 targetPosition = PatrolPoints[_currentPatrolIndex];
		Vector2 direction = Position.DirectionTo(targetPosition);

		// Move towards the target patrol point
		Velocity = direction * Speed;
		MoveAndSlide();

		// If close enough to the target, move to the next patrol point
		if (Position.DistanceTo(targetPosition) < 10)
		{
			_currentPatrolIndex = (_currentPatrolIndex + 1) % PatrolPoints.Count;
		}
	}
	
	protected void PrintInventoryContents()
	{
		GD.Print("Inventory contents:");
		foreach (var item in _characterData.PlayerInventory.GetItems())
		{
			GD.Print($"Item ID: {item.Id}, Name: {item.Name}");
		}
	}
	
	protected void SetInitialPosition(int x, int y)
	{
		this.Position = new Vector2(x, y);
	}
	
	public CharacterData CharacterData
	{
		get { return _characterData; }
		set { _characterData = value; }
	}
}
