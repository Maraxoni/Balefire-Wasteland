using Godot;
using System.Collections.Generic;

public partial class NonPlayerCharacter : CharacterBase
{
	// Load the DialogueBox scene
	public PackedScene DialogueBoxScene = GD.Load<PackedScene>("res://scenes/menus/DialogueMenu.tscn");
	private Control dialogueBoxInstance;

	// Example choices
	private List<string> dialogueChoices = new List<string>
	{
		"Ask about the quest",
		"Say goodbye"
	};

	private bool _isDialogueVisible; // Track dialogue visibility

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
					ToggleDialogueMenu(); // Show or hide the dialogue menu
				}
			}
		}
	}

	// Check if the mouse position is over this object
	private bool IsMouseOver(Vector2 mousePosition)
	{
		var collisionShape = GetNodeOrNull<CollisionShape2D>("CollisionShape2D");
		if (collisionShape != null)
		{
			if (collisionShape.Shape is RectangleShape2D rectangleShape)
			{
				Rect2 rect = new Rect2(GlobalPosition - rectangleShape.Size / 2, rectangleShape.Size);
				return rect.HasPoint(mousePosition);
			}
		}
		return false;
	}

	// Method to toggle the dialogue menu
	private void ToggleDialogueMenu()
	{
		if (!_isDialogueVisible)
		{
			// If the dialogue menu is not visible, show it
			_isDialogueVisible = true;
			if (dialogueBoxInstance == null)
			{
				// Create a new instance of the dialogue menu
				dialogueBoxInstance = DialogueBoxScene.Instantiate<Control>();
				GetTree().Root.AddChild(dialogueBoxInstance); // Add to the root viewport

				

				// Set up dialogue text in the dialogue box
				Label label = dialogueBoxInstance.GetNode<Label>("Label");
				label.Text = "Hello, I am an NPC! What would you like to do?";

				// Show choices
				ShowChoices(dialogueBoxInstance);
			}
		}
		else
		{
			// If the dialogue menu is already visible, toggle it off
			_isDialogueVisible = false;
			if (dialogueBoxInstance != null)
			{
				dialogueBoxInstance.QueueFree(); // Remove the dialogue menu
				dialogueBoxInstance = null; // Reset the instance
			}
		}
	}

	private void ShowChoices(Control dialogueBox)
	{
		VBoxContainer choicesContainer = dialogueBox.GetNode<VBoxContainer>("ChoicesContainer");

		// Clear existing buttons (in case ShowChoices is called multiple times)
		foreach (Node child in choicesContainer.GetChildren())
		{
			child.QueueFree(); // Free each child node
		}

		foreach (var choiceText in dialogueChoices)
		{
			// Create a new button for each choice
			Button choiceButton = new Button();
			choiceButton.Text = choiceText;

			// Connect the button's pressed signal to the OnChoiceSelected method
			//choiceButton.Connect("pressed", this, nameof(OnChoiceSelected), new Godot.Collections.Array { choiceText });

			choicesContainer.AddChild(choiceButton); // Add button to the container
		}
	}

	private void OnChoiceSelected(string choiceText)
	{
		// Handle the choice based on the button text
		if (choiceText == "Ask about the quest")
		{
			GD.Print("You asked about the quest!");
			// Optionally, provide more dialogue or trigger a quest
		}
		else if (choiceText == "Say goodbye")
		{
			GD.Print("Goodbye!");
		}

		// Remove the dialogue box after choice is made
		ToggleDialogueMenu(); // This will close the dialogue menu
	}
}
