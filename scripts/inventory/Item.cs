using Godot;
using System;

namespace GameProject
{
	public abstract partial class Item : Resource
	{
		public abstract int Id { get; }
		public abstract string Name { get; }
		public abstract int StackSize { get; }
		public abstract string IconPath { get; }
		
		[Export] public int Quantity { get; set; }

		// Default constructor (required for Godot to instantiate resources)
		public Item() { }

		// Abstract method for using the item
		public virtual void Equip()
		{
			GD.Print($"{Name} equipped!");
		}
		
		// Abstract method for using the item
		public virtual void Use()
		{
			GD.Print($"{Name} used!");
		}

		public override string ToString()
		{
			return $"Item: {Name}, Quantity: {Quantity}/{StackSize}";
		}
	}
}
