using Godot;
using System;

namespace GameProject
{
	public abstract partial class Armor : Item
	{
		[Export] public int Defense { get; set; }
		
		[Export] public bool IsEquipped { get; set;}
		
		public Armor(int quantity = 1)
		{
			Quantity = quantity;
		}

		public override void Use()
		{
			GD.Print($"{Name} used!");
		}

		public override void Equip()
		{
			GD.Print($"{Name} equipped.");
		}
	}
}
