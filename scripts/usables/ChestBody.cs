using Godot;
using System;
using GameProject;

public partial class ChestBody : StaticBody2D
{
	private Inventory inventory; // Store the inventory
	private bool _isItemInventoryVisible; // Track inventory visibility

	private PackedScene itemInventoryMenuScene = GD.Load<PackedScene>("res://scenes/menus/ItemInventoryMenu.tscn"); // Loaded path for the inventory menu
	private ItemInventoryMenu _itemInventoryMenuInstance; // Instance of the inventory menu

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Enable input processing to handle clicks
		SetProcessInput(true);
	}

	// Handle input events (for example, mouse clicks)
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			// Check if the left mouse button was pressed
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				// Get the global position of the mouse click
				Vector2 mousePosition = GetGlobalMousePosition();

				// Check if the click happened on this object
				if (IsMouseOver(mousePosition))
				{
					ToggleItemInventoryMenu(); // Open the inventory menu
				}
			}
		}
	}

	// Check if the mouse position is over this object
	private bool IsMouseOver(Vector2 mousePosition)
	{
		// Use the CollisionShape2D to check if the mouse is over this object
		var collisionShape = GetNodeOrNull<CollisionShape2D>("CollisionShape2D"); // Ensure this path is correct
		if (collisionShape != null)
		{
			// Get the shape and calculate the local rectangle based on the collision shape's global position and size
			if (collisionShape.Shape is RectangleShape2D rectangleShape) // Assuming a rectangle shape; adjust if needed
			{
				// Calculate the rectangle for the collision shape
				Rect2 rect = new Rect2(GlobalPosition - rectangleShape.Size / 2, rectangleShape.Size);
				return rect.HasPoint(mousePosition);
			}
		}
		return false;
	}

	// Method to open the ItemInventoryMenu
	private void ToggleItemInventoryMenu()
	{
		if (!_isItemInventoryVisible)
		{
			// If the inventory menu is not visible, show it
			_isItemInventoryVisible = true;
			if (_itemInventoryMenuInstance == null)
			{
				// Create a new instance of the inventory menu
				_itemInventoryMenuInstance = itemInventoryMenuScene.Instantiate<ItemInventoryMenu>();
				GetTree().Root.AddChild(_itemInventoryMenuInstance); // Add to the root viewport
				_itemInventoryMenuInstance.Position = GlobalPosition; // Set position relative to global position
				_itemInventoryMenuInstance.Show(); // Show the menu
			}
		}
		else
		{
			// If the inventory menu is already visible, toggle it off
			_isItemInventoryVisible = false;
			if (_itemInventoryMenuInstance != null)
			{
				_itemInventoryMenuInstance.QueueFree(); // Remove the inventory menu
				_itemInventoryMenuInstance = null; // Reset the instance
			}
		}
	}

}
