using Godot;
using System;

namespace GameProject
{
	public partial class Caps : Item
	{
		// Fixed properties
		public override int Id => 1;
		public override string Name => "Caps";
		public override int StackSize => 999;
		public override string IconPath => "";
		
		public Caps(int quantity)
		{
			Quantity = quantity;
		}
		
		public override void Use()
		{

		}
	}
}
