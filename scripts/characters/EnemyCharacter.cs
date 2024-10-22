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

	// Common logic for all enemies, like patrolling, can be inherited
	public override void _Ready()
	{
		GD.Print("Enemy ready with common setup.");
	}

	public override void _PhysicsProcess(double delta)
	{
		Patrol(delta); // Patrol behavior inherited from CharacterBase

		if (IsPlayerInRange())
		{
			AttackPlayer();
		}
	}

	// Check if the player is within detection range (could be adjusted per enemy type)
	protected virtual bool IsPlayerInRange()
	{
		var player = GetPlayer();
		return player != null && Position.DistanceTo(player.Position) < DetectionRange;
	}

	// Abstract attack method to be implemented by specific enemy types
	protected abstract void AttackPlayer();

	// Helper function to find the player (could be expanded based on game needs)
	protected PlayerCharacter GetPlayer()
	{
		return GetNodeOrNull<PlayerCharacter>("/root/PlayerCharacter");
	}
}
