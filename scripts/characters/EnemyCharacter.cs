using Godot;
using System;

public abstract partial class EnemyCharacter : CharacterBase
{
	[Export]
	public int AttackDamage { get; set; } = 10;

	[Export]
	public float AttackRange { get; set; } = 100f;

	[Export]
	public float DetectionRange { get; set; } = 300f;

	[Export]
	public float StopChaseRange { get; set; } = 400f; // When to stop chasing and return to patrol

	[Export]
	public float MoveSpeed { get; set; } = 100f; // Movement speed of the enemy

	private enum EnemyState
	{
		Patrolling,
		Chasing
	}

	private EnemyState _currentState = EnemyState.Patrolling;

	// Common logic for all enemies, like patrolling, can be inherited
	public override void _Ready()
	{
		GD.Print("Enemy ready with common setup.");
	}

	public override void _PhysicsProcess(double delta)
	{
		var player = GetPlayer();

		switch (_currentState)
		{
			case EnemyState.Patrolling:
				Patrol(delta); // Patrol behavior inherited from CharacterBase

				// Transition to Chasing if player is within detection range
				if (player != null && Position.DistanceTo(player.Position) < DetectionRange)
				{
					_currentState = EnemyState.Chasing;
				}
				break;

			case EnemyState.Chasing:
				FollowPlayer(player, delta);

				// Return to patrolling if player is too far
				if (player == null || Position.DistanceTo(player.Position) > StopChaseRange)
				{
					_currentState = EnemyState.Patrolling;
				}

				// If within attack range, attack the player
				if (IsPlayerInAttackRange(player))
				{
					AttackPlayer();
				}
				break;
		}
	}

	// Follow the player when in chase state
	private void FollowPlayer(PlayerCharacter player, double delta)
	{
		if (player != null)
		{
			// Calculate direction towards the player
			Vector2 direction = (player.Position - Position).Normalized();
			Position += direction * (float)(MoveSpeed * delta);
		}
	}

	// Check if the player is within attack range
	protected virtual bool IsPlayerInAttackRange(PlayerCharacter player)
	{
		return player != null && Position.DistanceTo(player.Position) < AttackRange;
	}

	// Abstract attack method to be implemented by specific enemy types
	protected abstract void AttackPlayer();

	// Helper function to find the player
	protected PlayerCharacter GetPlayer()
	{
		return GetNodeOrNull<PlayerCharacter>("../PlayerCharacter");
	}
}
