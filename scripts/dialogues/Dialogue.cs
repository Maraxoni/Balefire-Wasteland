using System.Collections.Generic;

public class Dialogue
{
	public string Text { get; set; }
	public List<DialogueChoice> Choices { get; set; }

	public Dialogue(string text)
	{
		Text = text;
		Choices = new List<DialogueChoice>();
	}
}
