using Godot;
using System;

public abstract partial class EnemyCharacter : CharacterBase
{
	private bool _isHovered = false;
	
	[Export]
	public int Health { get; set; } = 100; // Enemy's initial health

	[Export]
	public int AttackDamage { get; set; } = 10;

	[Export]
	public float AttackRange { get; set; } = 50f;

	[Export]
	public float DetectionRange { get; set; } = 300f;

	[Export]
	public float StopChaseRange { get; set; } = 400f;

	[Export]
	public float MoveSpeed { get; set; } = 100f;
	
	[Export]
	public float ExperiencePoints { get; set; } = 100f;
	
	private Vector2 _previousPosition;
	
	private enum EnemyState
	{
		Idle,
		Patrolling,
		Chasing,
		Attacking
	}

	private EnemyState _currentState = EnemyState.Patrolling;

	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		GD.Print("Enemy ready with common setup.");
	}
	
	public override void _Draw(){
		Color red = Colors.Red;
		Color transparent = new Color("0000008f");
		
		if(_isHovered){
			DrawCircle(new Vector2(0, 10.0f), 25.0f, red);
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

	public override void _PhysicsProcess(double delta)
	{
		if (Health <= 0)
		{
			HandleDeath();
			return;
		}
		
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

		var player = GetPlayer();

		switch (_currentState)
		{
			case EnemyState.Patrolling:
				if(PatrolPoints.Count > 0)
				{
					Patrol(delta);
					_animatedSprite.Play("walk");
				}
				else
				{
					GD.Print("Enemy state entered - Idle.");
					_currentState = EnemyState.Idle;
				}
				
				if (player != null && Position.DistanceTo(player.Position) < DetectionRange)
				{
					GD.Print("Enemy state entered - Chasing.");
					_currentState = EnemyState.Chasing;
				}
				break;

			case EnemyState.Chasing:
				FollowPlayer(player, delta);
				_animatedSprite.Play("walk");
				
				if (player == null || Position.DistanceTo(player.Position) > StopChaseRange)
				{
					GD.Print("Enemy state entered - Patrolling.");
					_currentState = EnemyState.Patrolling;
				} else if(Position.DistanceTo(player.Position) < AttackRange)
				{
					GD.Print("Ghoul state entered - Attacking.");
					_currentState = EnemyState.Attacking;
				}
				
				break;
				
			case EnemyState.Attacking:
				if (player != null && Position.DistanceTo(player.Position) < DetectionRange && Position.DistanceTo(player.Position) > AttackRange)
				{
					GD.Print("Enemy state entered - Chasing.");
					_currentState = EnemyState.Chasing;
				}
				
				if (IsPlayerInAttackRange(player))
				{
					_animatedSprite.Play("attack");
					AttackPlayer();
				}
				
				break;
				
			case EnemyState.Idle:
				_animatedSprite.Play("default");
				
				if (player != null && Position.DistanceTo(player.Position) < DetectionRange && Position.DistanceTo(player.Position) > AttackRange)
				{
					GD.Print("Enemy state entered - Chasing.");
					_currentState = EnemyState.Chasing;
				}
				break;
		}
	}

	private void FollowPlayer(PlayerCharacter player, double delta)
	{
		if (player != null)
		{
			// Calculate direction to the player
			Vector2 direction = (player.Position - Position).Normalized();
			
			// Apply movement using MoveAndSlide
			Velocity = direction * MoveSpeed;
			MoveAndSlide();
		}
	}

	protected virtual bool IsPlayerInAttackRange(PlayerCharacter player)
	{
		return player != null && Position.DistanceTo(player.Position) < AttackRange;
	}

	protected abstract void AttackPlayer();
	
	public void TakeDamage(int damage)
	{
		Health = Health - damage;
		if(Health < 0){
			HandleDeath();
		}
	}

	protected void HandleDeath()
	{
		GD.Print("Enemy has died.");
		
		var player = GetPlayer();
		player.CharacterData.ExperiencePoints =+ ExperiencePoints;
		
		QueueFree(); 
	}

	protected PlayerCharacter GetPlayer()
	{
		return GetNodeOrNull<PlayerCharacter>("../PlayerCharacter");
	}
	
}
