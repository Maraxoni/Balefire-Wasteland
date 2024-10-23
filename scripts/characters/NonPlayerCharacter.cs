using Godot;
using System.Collections.Generic;

public partial class NonPlayerCharacter : CharacterBase
{
	// Load the DialogueBox scene
	private PackedScene _dialogueMenuScene = GD.Load<PackedScene>("res://scenes/menus/DialogueMenu.tscn");
	private DialogueMenu _dialogueMenuInstance; // Instance of the inventory menu
	
	private bool _isDialogueMenuVisible;
	
	public override void _Ready()
	{
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
		if (!_isDialogueMenuVisible)
		{
			// If the inventory menu is not visible, show it
			_isDialogueMenuVisible = true;
			if (_dialogueMenuInstance == null)
			{
				// Create a new instance of the inventory menu
				_dialogueMenuInstance = _dialogueMenuScene.Instantiate<DialogueMenu>();
				GetTree().Root.AddChild(_dialogueMenuInstance); // Add to the root viewport
				_dialogueMenuInstance.Position = GlobalPosition; // Set position relative to global position
				_dialogueMenuInstance.Show(); // Show the menu
			}
		}
		else
		{
			// If the inventory menu is already visible, toggle it off
			_isDialogueMenuVisible = false;
			if (_dialogueMenuInstance != null)
			{
				_dialogueMenuInstance.QueueFree(); // Remove the inventory menu
				_dialogueMenuInstance = null; // Reset the instance
			}
		}
	}

	
}
