using Godot;
using System.Collections.Generic;

public partial class NonPlayerCharacter : CharacterBase
{
	[Export]
	private string _npcKey = "";
	
	private bool _isHovered = false;
	
	private const float InteractionRange = 100.0f;
	
	private Vector2 _previousPosition;
	
	private enum NPCState
	{
		Idle,
		Patrolling,
		Talking
	}
	
	private NPCState _currentState = NPCState.Patrolling;
	
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		GD.Print("NPC ready with common setup.");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		var player = GetPlayer();
		
		var movementDirection = Position - _previousPosition;
		if (movementDirection.X > 0)
		{
			_animatedSprite.FlipH = true;
		}
		else if (movementDirection.X < 0)
		{
			_animatedSprite.FlipH = false;
		}
		_previousPosition = Position;
		
		switch (_currentState)
		{
			case NPCState.Patrolling:
				if(PatrolPoints.Count > 0)
				{
					Patrol(delta);
					_animatedSprite.Play("walk");
				}
				else
				{
					GD.Print("Npc state entered - Idle.");
					_currentState = NPCState.Idle;
				}
				
				if (Position.DistanceTo(player.Position) < InteractionRange)
				{
					GD.Print("Npc state entered - Talking.");
					_currentState = NPCState.Talking;
				}
				break;

			case NPCState.Talking:
				_animatedSprite.Play("default");
				
				if(Position.DistanceTo(player.Position) > InteractionRange)
				{
					GD.Print("NPC state entered - Patrolling.");
					_currentState = NPCState.Patrolling;
				}
				
				break;

			case NPCState.Idle:
				_animatedSprite.Play("default");
				
				if (Position.DistanceTo(player.Position) < InteractionRange)
				{
					GD.Print("Npc state entered - Talking.");
					_currentState = NPCState.Talking;
				}
				break;
		}
	}

	// Handle input events
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.ButtonIndex == MouseButton.Left && eventMouseButton.Pressed && _isHovered)
			{
				if (IsPlayerInRange())
				{
					GetTree().Root.GetNode<UserInterface>("UserInterface").ToggleDialogueMenu(_npcKey, "start");
				}
				else
				{
					GD.Print("Player is out of range of the NPC.");
				}
			}
		}
	}
	
	public override void _Draw(){
		Color blue = Colors.LightBlue;
		Color transparent = new Color("0000008f");
		
		if(_isHovered){
			DrawCircle(new Vector2(0, 10.0f), 25.0f, blue);
			DrawCircle(new Vector2(0, 10.0f), 20.0f, transparent);
		}
	}
	
	private void _on_area_2d_mouse_entered(){
		_isHovered = true;
		QueueRedraw();
	}

	private void _on_area_2d_mouse_exited(){
		_isHovered = false;
		QueueRedraw();
	}
	
	private bool IsPlayerInRange()
	{
		var player = GetPlayer();
		if (player == null)
		{
			GD.Print("Player not found!");
			return false;
		}

		// Oblicz dystans między graczem a skrzynią
		float distance = GlobalPosition.DistanceTo(player.GlobalPosition);

		// Sprawdź, czy dystans jest w zasięgu
		return distance <= InteractionRange;
	}
	
	protected PlayerCharacter GetPlayer()
	{
		return GetNodeOrNull<PlayerCharacter>("../PlayerCharacter");
	}
	
	public string NPCKey
	{
		get { return _npcKey; }
		set { _npcKey = value; }
	}
	
}
