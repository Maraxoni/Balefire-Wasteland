using Godot;
using System;

public abstract partial class EnemyCharacter : CharacterBase
{
	[Export]
	public int Health { get; set; } = 100; // Enemy's initial health

	[Export]
	public int AttackDamage { get; set; } = 10;

	[Export]
	public float AttackRange { get; set; } = 100f;

	[Export]
	public float DetectionRange { get; set; } = 300f;

	[Export]
	public float StopChaseRange { get; set; } = 400f;

	[Export]
	public float MoveSpeed { get; set; } = 100f;

	private enum EnemyState
	{
		Patrolling,
		Chasing
	}

	private EnemyState _currentState = EnemyState.Patrolling;

	public override void _Ready()
	{
		GD.Print("Enemy ready with common setup.");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Health <= 0)
		{
			HandleDeath();
			return;
		}

		var player = GetPlayer();

		switch (_currentState)
		{
			case EnemyState.Patrolling:
				Patrol(delta);

				if (player != null && Position.DistanceTo(player.Position) < DetectionRange)
				{
					_currentState = EnemyState.Chasing;
				}
				break;

			case EnemyState.Chasing:
				FollowPlayer(player, delta);

				if (player == null || Position.DistanceTo(player.Position) > StopChaseRange)
				{
					_currentState = EnemyState.Patrolling;
				}

				if (IsPlayerInAttackRange(player))
				{
					AttackPlayer();
				}
				break;
		}
	}

	private void FollowPlayer(PlayerCharacter player, double delta)
	{
		if (player != null)
		{
			Vector2 direction = (player.Position - Position).Normalized();
			Position += direction * (float)(MoveSpeed * delta);
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
		// Handle the enemy's death here, for example:
		GD.Print("Enemy has died.");
		QueueFree(); // Remove the enemy from the scene, or trigger some death animation.
	}

	protected PlayerCharacter GetPlayer()
	{
		return GetNodeOrNull<PlayerCharacter>("../PlayerCharacter");
	}
}
