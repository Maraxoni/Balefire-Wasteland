using Godot;
using System;

namespace GameProject
{
	public partial class MilitaryRifle : Weapon
	{
		public override int Id => 3;
		public override string Name => "Military Rifle";
		public override int StackSize => 1;
		public override string IconPath => "res://assets/equipables/weapons/MilitaryRifle.png";
		
		public MilitaryRifle(int quantity = 1) : base(quantity)
		{
			Damage = 50;
			FireRate = 0.2f;
			Range = 50;
			ActionCost = 10;
			IsRanged = true;
			IsEquipped = false;
		}

		public override void Use()
		{
			GD.Print($"{Name} fired! Damage: {Damage}");
		}
	}
}
