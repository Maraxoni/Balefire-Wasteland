using Godot;
using System;

namespace GameProject
{
	public partial class PreWarMoney : Item
	{
		// Fixed properties
		public override int Id => 2;
		public override string Name => "Pre-war Money";
		public override int StackSize => 999;
		public override string IconPath => "";
		
		public PreWarMoney(int quantity)
		{
			Quantity = quantity;
		}
		
		public override void Use()
		{

		}
	}
}
