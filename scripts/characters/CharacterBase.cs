using Godot;
using System;
using Godot.Collections;

public partial class CharacterBase : CharacterBody2D
{
	[Export]
	public string Name { get; set; } = "CharacterNameHere";
	[Export]
	public int Speed { get; set; } = 200;
	[Export]
	public int Health { get; set; } = 200;
	[Export]
	public int ActionPoints { get; set; } = 200;
	
	protected Vector2 _character_position;
	protected CharacterData _characterData;
	protected CollisionShape2D _selectedShape;
	protected AnimatedSprite2D _animatedSprite;
	
	// Patrol points and patrol logic
	[Export]
	private Array<Vector2> PatrolPoints = new Array<Vector2>();
	private int _currentPatrolIndex = 0;
	private bool _isPatrolling = true;
	
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
		if (_isPatrolling && PatrolPoints.Count > 0)
		{
			Patrol(delta);
		}
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
}
