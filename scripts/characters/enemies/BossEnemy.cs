using Godot;
using System;
using System.Threading.Tasks;

public partial class BossEnemy : EnemyCharacter
{
	[Export]
	public int MeleeDamage { get; set; } = 20;
	
	[Export]
	public float AttackCooldown { get; set; } = 2.0f;
	
	private bool _isAttackOnCooldown = false;
	
	public override void _Ready()
	{
		base._Ready();
		GD.Print("Melee enemy ready.");
		Speed = 70; // Specific speed for melee enemies
		AttackRange = 70;
	}
	
	protected override async void AttackPlayer()
	{
		if (IsPlayerInMeleeRange() && _isAttackOnCooldown == false)
		{
			GD.Print("Melee attack!");
			var player = GetPlayer();
			if (player != null)
			{
				player.TakeDamage(MeleeDamage); // Player takes damage
				_isAttackOnCooldown = true;
				await Task.Delay((int)(AttackCooldown * 1000)); // Cooldown delay
				_isAttackOnCooldown = false;
			}
		}
	}

	private bool IsPlayerInMeleeRange()
	{
		var player = GetPlayer();
		return player != null && Position.DistanceTo(player.Position) < AttackRange;
	}
	
}
