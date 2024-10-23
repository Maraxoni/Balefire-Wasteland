using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DialogueCharacter
{
	[JsonPropertyName("dialogues")]
	public Dictionary<string, Dialogue> Dialogues { get; set; }
}
