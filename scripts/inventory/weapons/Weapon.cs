using Godot;
using System;

namespace GameProject
{
	public abstract partial class Weapon : Item
	{
		
		[Export] public int Damage { get; set; }
		[Export] public float FireRate { get; set; }
		[Export] public int Range { get; set; }
		[Export] public int ActionCost { get; set; }
		[Export] public bool IsRanged { get; set; }
		
		[Export] public bool IsEquipped { get; set;}
		
		public Weapon(int quantity = 1)
		{
			Quantity = quantity;
		}

		public override void Use()
		{
			GD.Print($"{Name} used! Damage: {Damage}");
		}

		public override void Equip()
		{
			GD.Print($"{Name} equipped.");
		}
	}
}
