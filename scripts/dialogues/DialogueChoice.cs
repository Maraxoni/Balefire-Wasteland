using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DialogueChoice
{	
	[JsonPropertyName("text")]
	public string Text { get; set; }

	[JsonPropertyName("next")]
	public string Next { get; set; }
	
	[JsonPropertyName("conditions")]
	public Dictionary<string, DialogueChoice> Conditions { get; set; }
}
