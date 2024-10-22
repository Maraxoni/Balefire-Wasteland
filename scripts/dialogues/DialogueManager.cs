using Godot;
using System;
using System.Collections.Generic;

public partial class DialogueManager : Control
{
	[Export]
	private Label dialogueLabel;

	[Export]
	private VBoxContainer choiceContainer;

	private Dialogue currentDialogue;
	private Dictionary<string, bool> dialogueStates;

	public override void _Ready()
	{
		dialogueStates = new Dictionary<string, bool>
		{
			{ "talked_to_npc", false },
			{ "item_acquired", false }
		};
		StartDialogue();
	}

	private void StartDialogue()
	{
		var secondDialogue = new Dialogue("What would you like to do next?");
		secondDialogue.Choices.Add(new DialogueChoice("Go left", null));
		secondDialogue.Choices.Add(new DialogueChoice("Go right", null));

		var firstDialogue = new Dialogue("Hello! What do you want to do?");
		firstDialogue.Choices.Add(new DialogueChoice("Talk", secondDialogue, !dialogueStates["talked_to_npc"]));
		firstDialogue.Choices.Add(new DialogueChoice("Leave", null));
		firstDialogue.Choices.Add(new DialogueChoice("Ask about the item", null, dialogueStates["item_acquired"]));

		currentDialogue = firstDialogue;

		DisplayDialogue(currentDialogue);
	}

	private void DisplayDialogue(Dialogue dialogue)
	{
		dialogueLabel.Text = dialogue.Text;

		// Remove each child manually
		foreach (Node child in choiceContainer.GetChildren())
		{
			child.QueueFree();
		}

		foreach (var choice in dialogue.Choices)
		{
			if (choice.IsAvailable) // Only add available choices
			{
				var button = new Button { Text = choice.ChoiceText };
				button.Pressed += () => OnChoiceSelected(choice);
				choiceContainer.AddChild(button);
			}
		}
	}

	private void OnChoiceSelected(DialogueChoice choice)
	{
		// Update the states based on choices made
		if (choice.ChoiceText == "Talk")
		{
			dialogueStates["talked_to_npc"] = true;
		}
		else if (choice.ChoiceText == "Ask about the item")
		{
			// Assume some logic to show item info, etc.
			GD.Print("The item was acquired!");
			dialogueStates["item_acquired"] = true;
		}

		if (choice.NextDialogue != null)
		{
			currentDialogue = choice.NextDialogue;
			DisplayDialogue(currentDialogue);
		}
		else
		{
			// Handle the end of the dialogue or exit logic
			GD.Print("Dialogue ended.");
			Hide();
		}
	}
}
