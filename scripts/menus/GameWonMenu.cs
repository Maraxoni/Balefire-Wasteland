using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public partial class GameWonMenu : Control
{
	private Label _titleLabel;
	private Label _descriptionLabel;
	
	private UserInterface _userInterface;

	private Dictionary<string, Ending> _endings = new Dictionary<string, Ending>();  // Zmieniony na Dictionary
	private string _currentEndingKey = "";  // Klucz aktualnie wyświetlanego zakończenia
	
	private GameData _globalGameData;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_userInterface = GetNode<UserInterface>("/root/UserInterface");
		_globalGameData = GetNode<GameData>("/root/GlobalGameData");
		if (_userInterface != null)
		{
			_userInterface.Hide();
			_userInterface.QueueFree();
		}
		else
		{
			GD.PrintErr("UserInterface does not exist.");
		}
		
		// Inicjalizacja Labeli
		_titleLabel = GetNode<Label>("MarginContainer/VBoxContainer/EndingTitleLabel");
		_descriptionLabel = GetNode<Label>("MarginContainer/VBoxContainer/EndingDescriptionLabel");

		// Wczytanie treści z JSON
		LoadEndings("assets/endings/endings.json");
		PrintAllEndings();  // Wywołanie funkcji do wypisania wszystkich zakończeń
		DisplayEnding();  // Wyświetlenie wybranego zakończenia
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_game_won_return_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/menus/MainMenu.tscn");
	}
	
	private void DisplayEnding()
	{
		
		_currentEndingKey = _globalGameData.GetEndingType();
		
		
		if (_endings != null && _endings.Count > 0 && _endings.ContainsKey(_currentEndingKey))
		{
			var ending = _endings[_currentEndingKey];

			if (ending != null)
			{
				_titleLabel.Text = ending.Title;
				_descriptionLabel.Text = ending.Description;
			}
		}
		else
		{
			GD.PrintErr("Brak dostępnych zakończeń do wyświetlenia.");
		}
	}
	
	// Wczytanie zakończeń z pliku JSON
	private void LoadEndings(string filePath)
	{
		GD.Print("Endings loading...");
		try
		{
			string jsonText = File.ReadAllText(filePath);
			_endings = JsonSerializer.Deserialize<Dictionary<string, Ending>>(jsonText);  // Użycie Dictionary
			GD.Print("Endings loaded successfully.");
		}
		catch (JsonException jsonEx)
		{
			GD.PrintErr($"Failed to deserialize endings: {jsonEx.Message}");
		}
		catch (IOException ioEx)
		{
			GD.PrintErr($"I/O error while loading endings: {ioEx.Message}");
		}
		catch (Exception ex)
		{
			GD.PrintErr($"Failed to load endings: {ex.Message}");
		}
	}
	
	// Funkcja do wyświetlania wszystkich zakończeń w konsoli
	private void PrintAllEndings()
	{
		if (_endings != null && _endings.Count > 0)
		{
			foreach (var entry in _endings)
			{
				GD.Print($"Ending key: {entry.Key}:");
				GD.Print($"  Title: {entry.Value.Title}");
				GD.Print($"  Description: {entry.Value.Description}");
			}
		}
		else
		{
			GD.PrintErr("No endings available to print.");
		}
	}
}
