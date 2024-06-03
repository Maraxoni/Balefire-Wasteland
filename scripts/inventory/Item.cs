using Godot;
using System;

namespace GameProject {
	public partial class Item : Resource
	{
		[Export]
		public int Id { get; set; }
		[Export]
		public string Name { get; set; }
		[Export]
		public string ResourcePath { get; set; }
		[Export]
		public int Quantity { get; set; }
		[Export]
		public int StackSize { get; set; }
		[Export]
		public bool IsStackable { get; set; }

		public Item(string name)
		{
			Name = name;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
