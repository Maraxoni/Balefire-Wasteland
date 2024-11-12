using Godot;
using System;

namespace GameProject
{
	public partial class MediumArmor : Armor
	{
		public override int Id => 4;
		public override string Name => "Medium Armor";
		public override int StackSize => 1;
		public override string IconPath => "res://assets/MediumArmor.png";
		
		public MediumArmor(int quantity = 1) : base(quantity)
		{
			Defense = 5;
			IsEquipped = false;
		}

		public override void Use()
		{
			GD.Print($"{Name} fired!");
		}
	}
}
