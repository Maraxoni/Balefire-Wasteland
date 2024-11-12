using Godot;
using System;

public partial class GhoulEnemy : EnemyCharacter
{
	[Export]
	public int MeleeDamage { get; set; } = 20;
	
	[Export]
	public float AttackCooldown { get; set; } = 1.5f;

	private float _lastAttackTime = 0f;
	
	public override void _Ready()
	{
		base._Ready();
		GD.Print("Melee enemy ready.");
		Speed = 50; // Specific speed for melee enemies
	}

	protected override void AttackPlayer()
	{
		if (IsPlayerInMeleeRange())
		{
			GD.Print("Melee attack!");
			var player = GetPlayer();
			if (player != null)
			{
				// Implement melee attack logic, like reducing player health
				player.TakeDamage(MeleeDamage); // Player takes damage
				//_lastAttackTime = OS.GetTicks() / 1000.0f;
			}
		}
	}

	private bool IsPlayerInMeleeRange()
	{
		var player = GetPlayer();
		return player != null && Position.DistanceTo(player.Position) < AttackRange;
	}
	
	//private float TimeSinceLastAttack()
	//{
		//// Return the time passed since the last attack
		//return (OS.GetTicks() / 1000.0f) - _lastAttackTime;
	//}
}
