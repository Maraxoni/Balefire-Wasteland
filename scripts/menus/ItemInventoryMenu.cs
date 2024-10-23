using Godot;
using System;

public partial class ItemInventoryMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_back_item_inventory_menu_button_pressed()
	{
		Node parent = GetParent();
		parent.RemoveChild(this);
	}
	
}
