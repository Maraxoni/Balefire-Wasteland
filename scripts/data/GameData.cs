using Godot;
using System;

public partial class GameData : Node
{
	private bool _dialogueChoice1 = false;
	private bool _dialogueChoice2 = false;
	private bool _dialogueChoice3 = false;
	private bool _dialogueChoice4 = false;
	private bool _dialogueChoice5 = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public bool DialogueChoice1
	{
		get => _dialogueChoice1;
		set => _dialogueChoice1 = value;
	}

	public bool DialogueChoice2
	{
		get => _dialogueChoice2;
		set => _dialogueChoice2 = value;
	}

	public bool DialogueChoice3
	{
		get => _dialogueChoice3;
		set => _dialogueChoice3 = value;
	}

	public bool DialogueChoice4
	{
		get => _dialogueChoice4;
		set => _dialogueChoice4 = value;
	}

	public bool DialogueChoice5
	{
		get => _dialogueChoice5;
		set => _dialogueChoice5 = value;
	}
	
	// Funkcja zliczająca liczbę true i zwracająca typ zakończenia
	public string GetEndingType()
	{
		// Zlicz liczbę wartości true
		int trueCount = 0;
		if (_dialogueChoice1) trueCount++;
		if (_dialogueChoice2) trueCount++;
		if (_dialogueChoice3) trueCount++;
		if (_dialogueChoice4) trueCount++;
		if (_dialogueChoice5) trueCount++;

		// Zwróć zakończenie w zależności od liczby true
		if (trueCount >= 4)
		{
			return "good_ending";
		}
		else if (trueCount == 2 || trueCount == 3)
		{
			return "neutral_ending";
		}
		else
		{
			return "bad_ending";
		}
	}
	
}
