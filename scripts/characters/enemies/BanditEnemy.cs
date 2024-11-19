using Godot;
using System;

public partial class BanditEnemy : EnemyCharacter
{
	[Export]
	public int MeleeDamage { get; set; } = 20;

	public override void _Ready()
	{
		base._Ready();
		GD.Print("Melee enemy ready.");
		Speed = 50; // Specific speed for melee enemies
	}

	protected override async void AttackPlayer()
	{
		if (IsPlayerInMeleeRange())
		{
			GD.Print("Melee attack!");
			var player = GetPlayer();
			if (player != null)
			{
				// Implement melee attack logic, like reducing player health
				player.TakeDamage(MeleeDamage); // Player takes damage
			}
		}
	}

	private bool IsPlayerInMeleeRange()
	{
		var player = GetPlayer();
		return player != null && Position.DistanceTo(player.Position) < AttackRange;
	}
}
