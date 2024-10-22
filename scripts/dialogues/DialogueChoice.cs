using System.Collections.Generic;

public class DialogueChoice
{
	public string ChoiceText { get; set; }
	public Dialogue NextDialogue { get; set; }
	public bool IsAvailable { get; set; } // This will determine if a choice is available

	public DialogueChoice(string choiceText, Dialogue nextDialogue, bool isAvailable = true)
	{
		ChoiceText = choiceText;
		NextDialogue = nextDialogue;
		IsAvailable = isAvailable;
	}
}
