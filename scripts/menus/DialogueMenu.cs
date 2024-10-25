using Godot;
using System;

public partial class DialogueMenu : Control
{
	private Label _dialogueLabel;
	private VBoxContainer _choicesContainer;

	private DialogueManager _dialogueManager;
	private Dialogue _currentDialogue;
	private string _currentCharacter;
	
	private UserInterface _userInterface;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_userInterface = GetNode<UserInterface>("/root/UserInterface");
		// Initialize UI components
		_dialogueLabel = GetNode<Label>("PanelContainer/VBoxContainer/DialogueLabel");
		_choicesContainer = GetNode<VBoxContainer>("PanelContainer/VBoxContainer/ChoiceButtonList");
	}
	// Call this to start dialogue for a specific character
	public void StartDialogue(string characterName, string startingKey)
	{
		_dialogueManager = GetNode<DialogueManager>("/root/GlobalDialogueManager");
		_currentCharacter = characterName;
		ShowDialogue(startingKey);
	}
	
	// Displays dialogue for a specific key
	private void ShowDialogue(string dialogueKey)
	{
		_currentDialogue = _dialogueManager.GetDialogue(_currentCharacter, dialogueKey);

		if (_currentDialogue != null)
		{
			_dialogueLabel.Text = _currentDialogue.Text;

			// Clear previous choices (remove all child nodes from the VBoxContainer)
			foreach (Node child in _choicesContainer.GetChildren())
			{
				child.QueueFree(); // This safely removes the child node
			}

			// If there are choices, display them
			if (_currentDialogue.Choices != null && _currentDialogue.Choices.Count > 0)
			{
				foreach (var choice in _currentDialogue.Choices)
				{
					Button choiceButton = new Button
					{
						Text = choice.Value.Text
					};
					
					choiceButton.Pressed += () => OnChoiceSelected(choice.Value.Next);

					_choicesContainer.AddChild(choiceButton);
				}
				_choicesContainer.Show();
			}
			else
			{
				// If no choices, show the Next button for a linear dialogue
				_choicesContainer.Hide();
			}
		}
	}
	
	// Called when a choice is selected
	private void OnChoiceSelected(string nextKey)
	{
		ShowDialogue(nextKey);
	}

	
	private void _on_back_dialogue_menu_button_pressed()
	{
		Node parent = GetParent();
		parent.RemoveChild(this);
		_userInterface.IsDialogueVisible = false;
	}
}
