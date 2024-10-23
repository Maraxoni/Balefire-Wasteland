using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public partial class DialogueManager : Node
{
	private Dictionary<string, DialogueCharacter> allCharacterDialogues = new Dictionary<string, DialogueCharacter>();

	public override void _Ready()
	{
		GD.Print($"Dialogues initialized.");
		LoadDialogueFromFile("assets/dialogues/dialogues.json");
		PrintAllDialogues();
	}

	// Load dialogue from a JSON file
	private void LoadDialogueFromFile(string filePath)
	{
		GD.Print("Dialogues loading...");
		try
		{
			//// Ensure the file exists before reading
			//if (!File.Exists(filePath))
			//{
				//GD.PrintErr($"File not found: {filePath}");
				//return;
			//}
			// Read the JSON file as text
			string jsonText = File.ReadAllText(filePath);
			// Deserialize the JSON data into Dictionary<string, DialogueCharacter>
			allCharacterDialogues = JsonSerializer.Deserialize<Dictionary<string, DialogueCharacter>>(jsonText);
			GD.Print("Dialogues loaded successfully.");
		}
		catch (JsonException jsonEx)
		{
			GD.PrintErr($"Failed to deserialize dialogues: {jsonEx.Message}");
		}
		catch (IOException ioEx)
		{
			GD.PrintErr($"I/O error while loading dialogues: {ioEx.Message}");
		}
		catch (Exception ex)
		{
			GD.PrintErr($"Failed to load dialogues: {ex.Message}");
		}
	}

	// Get dialogue for a specific character and key
	public Dialogue GetDialogue(string character, string key)
	{
		if (allCharacterDialogues.ContainsKey(character))
		{
			DialogueCharacter dialogueCharacter = allCharacterDialogues[character];
			if (dialogueCharacter.Dialogues.ContainsKey(key))
			{
				return dialogueCharacter.Dialogues[key];
			}
			else
			{
				GD.PrintErr($"Dialogue key '{key}' not found for character '{character}'.");
			}
		}
		else
		{
			GD.PrintErr($"Character '{character}' not found.");
		}

		return null;
	}

	// Print all loaded dialogues to the console
	public void PrintAllDialogues()
	{
		foreach (var character in allCharacterDialogues)
		{
			GD.Print($"Character: {character.Key}");
			foreach (var dialogue in character.Value.Dialogues)
			{
				GD.Print($"  Dialogue Key: {dialogue.Key}");
				GD.Print($"    Text: {dialogue.Value.Text}");

				if (dialogue.Value.Choices != null)
				{
					GD.Print("    Choices:");
					foreach (var choice in dialogue.Value.Choices)
					{
						GD.Print($"      {choice.Key}: {choice.Value.Text} -> Next: {choice.Value.Next}");
					}
				}

				if (dialogue.Value.Conditions != null)
				{
					GD.Print("    Conditions:");
					foreach (var condition in dialogue.Value.Conditions)
					{
						GD.Print($"      {condition.Key}: {condition.Value.Text}");
					}
				}
			}
		}
	}
}
