using Godot;
using System;
using System.Collections.Generic;

namespace GameProject {
	public partial class Inventory : Resource
	{
		
		private List<Item> items;

		public Inventory()
		{
			items = new List<Item>();
		}

		public void AddItem(Item item)
		{
			items.Add(item);
		}

		public void RemoveItem(Item item)
		{
			items.Remove(item);
		}

		public Item GetItem(int index)
		{
			if (index < 0 || index >= items.Count)
			{
				throw new IndexOutOfRangeException("Invalid index");
			}
			return items[index];
		}

		public Item[] GetAllItems()
		{
			return items.ToArray();
		}

		public override string ToString()
		{
			return string.Join(", ", items);
		}
	}
}
