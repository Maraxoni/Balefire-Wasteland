using Godot;
using System;
using GameProject;

public partial class ChestBody : StaticBody2D
{
	private Inventory inventory; // Store the inventory

	private bool _isHovered = false;
	
	public override void _Ready()
	{
	}

	// Handle input events
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.ButtonIndex == MouseButton.Left && eventMouseButton.Pressed && _isHovered)
			{
				// Call the dialogue menu in UserInterface
				GetTree().Root.GetNode<UserInterface>("UserInterface").ToggleItemInventoryMenu();
			}
		}
	}
	private void _on_area_2d_mouse_entered(){
		_isHovered = true;
	}

	private void _on_area_2d_mouse_exited(){
		_isHovered = false;
	}
	
}
