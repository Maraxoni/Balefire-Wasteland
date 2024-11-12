using Godot;
using System;

namespace GameProject
{
	public partial class MediumHealingPotion : Item
	{
		// Fixed properties
		public override int Id => 5;
		public override string Name => "Military Rifle";
		public override int StackSize => 10;
		public override string IconPath => "";
		
		[Export] public int Healing { get; set; } = 20;
		
		public MediumHealingPotion(int quantity)
		{
			Quantity = quantity;
		}
		
		public override void Use()
		{

		}
	}
}
