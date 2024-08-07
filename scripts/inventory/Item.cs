using Godot;
using System;

namespace GameProject {
	public partial class Item : Resource
	{
		[Export]
		public int Id { get; set; }
		[Export]
		public string Name { get; private set; }
		[Export]
		public string ResourcePath { get; set; }
		[Export]
		public int Quantity { get; set; }
		[Export]
		public int StackSize { get; set; }
		[Export]
		public bool IsStackable { get; set; }

		public Item(int id, string name, string resourcePath, int quantity, int stackSize, bool isStackable)
		{
			Id = id;
			Name = name;
			ResourcePath = resourcePath;
			Quantity = quantity;
			StackSize = stackSize;
			IsStackable = isStackable;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
