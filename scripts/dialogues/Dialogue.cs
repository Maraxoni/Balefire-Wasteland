using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Dialogue
{
	[JsonPropertyName("text")]
	public string Text { get; set; }

	[JsonPropertyName("choices")]
	public Dictionary<string, DialogueChoice> Choices { get; set; }
	
	[JsonPropertyName("conditions")]
	public Dictionary<string, DialogueChoice> Conditions { get; set; }
}
