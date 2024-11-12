using Godot;
using System;

namespace GameProject
{
	public partial class Knife : Weapon
	{
		public override int Id => 2;
		public override string Name => "Knife";
		public override int StackSize => 1;
		public override string IconPath => "res://assets/equipables/weapons/Knife.png";
		
		// Konstruktor z ustawieniem domyślnych wartości
		public Knife(int quantity = 1) : base(quantity)
		{
			Damage = 25;
			FireRate = 1.0f;
			Range = 5;
			ActionCost = 5;
			IsRanged = false;
			IsEquipped = false;
		}

		public override void Use()
		{
			GD.Print($"{Name} slashed! Damage: {Damage}");
		}
	}
}
