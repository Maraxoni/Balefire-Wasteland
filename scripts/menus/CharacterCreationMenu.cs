using Godot;
using System;

public partial class CharacterCreationMenu : Control
{
	private int _remainingPoints = 0;
	
	private CharacterData _creationCharacterData;
	
	private Label _strengthLabel;
	private Label _perceptionLabel;
	private Label _enduranceLabel;
	private Label _charismaLabel;
	private Label _intelligenceLabel;
	private Label _agilityLabel;
	private Label _luckLabel;
	private Label _remainingPointsLabel;
	private TextEdit _nameTextEdit;
	
	//private Button _strengthButtonIncrease;
	//private Button _perceptionButtonIncrease;
	//private Button _enduranceButtonIncrease;
	//private Button _charismaButtonIncrease;
	//private Button _intelligenceButtonIncrease;
	//private Button _agilityButtonIncrease;
	//private Button _luckButtonIncrease;
	//
	//private Button _strengthButtonDecrease;
	//private Button _perceptionButtonDecrease;
	//private Button _enduranceButtonDecrease;
	//private Button _charismaButtonDecrease;
	//private Button _intelligenceButtonDecrease;
	//private Button _agilityButtonDecrease;
	//private Button _luckButtonDecrease;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_creationCharacterData = GetNode<CharacterData>("/root/CharacterData");
		
		NodePath Path = GetPath();
		GD.Print("Path of CharacterCreation:", Path.ToString());
		
		_strengthLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer/Label");
		_perceptionLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer2/Label");
		_enduranceLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer3/Label");
		_charismaLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer4/Label");
		_intelligenceLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer5/Label");
		_agilityLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer6/Label");
		_luckLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer7/Label");
		_remainingPointsLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/Label9");
		_nameTextEdit = GetNode<TextEdit>("VBoxContainer/HBoxContainer2/VBoxContainer/TextEdit");
		
		UpdateLabels();
	}
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _Input(InputEvent @event)
	{
		
		if (@event.IsActionPressed("esc_key"))
		{
			GetTree().ChangeSceneToFile("res://scenes/menus/MainMenu.tscn");
		}
	}
	
	private void _on_text_edit_text_changed()
	{
		_creationCharacterData.PlayerStats.Name = _nameTextEdit.Text;
	}

	
	private void _on_back_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/menus/MainMenu.tscn");
	}

	private void _on_create_char_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/maps/MapVault.tscn");
	}

	private void _on_button_strength_decrease_pressed()
	{
		if (_creationCharacterData.PlayerStats.Strength > 0) _creationCharacterData.PlayerStats.Strength--;
		UpdateLabels();
	}

	private void _on_button_strength_increase_pressed()
	{
		if (_creationCharacterData.PlayerStats.Strength < 10 && _remainingPoints > 0) _creationCharacterData.PlayerStats.Strength++;
		UpdateLabels();
	}

	private void _on_button_perception_decrease_pressed()
	{
		if (_creationCharacterData.PlayerStats.Perception > 0) _creationCharacterData.PlayerStats.Perception--;
		UpdateLabels();
	}

	private void _on_button_perception_increase_pressed()
	{
		if (_creationCharacterData.PlayerStats.Perception < 10 && _remainingPoints > 0) _creationCharacterData.PlayerStats.Perception++;
		UpdateLabels();
	}

	private void _on_button_endurance_decrease_pressed()
	{
		if (_creationCharacterData.PlayerStats.Endurance > 0) _creationCharacterData.PlayerStats.Endurance--;
		UpdateLabels();
	}

	private void _on_button_endurance_increase_pressed()
	{
		if (_creationCharacterData.PlayerStats.Endurance < 10 && _remainingPoints > 0) _creationCharacterData.PlayerStats.Endurance++;
		UpdateLabels();
	}

	private void _on_button_charisma_decrease_pressed()
	{
		if (_creationCharacterData.PlayerStats.Charisma > 0) _creationCharacterData.PlayerStats.Charisma--;
		UpdateLabels();
	}

	private void _on_button_charisma_increase_pressed()
	{
		if (_creationCharacterData.PlayerStats.Charisma < 10 && _remainingPoints > 0) _creationCharacterData.PlayerStats.Charisma++;
		UpdateLabels();
	}

	private void _on_button_intelligence_decrease_pressed()
	{
		if (_creationCharacterData.PlayerStats.Intelligence > 0) _creationCharacterData.PlayerStats.Intelligence--;
		UpdateLabels();
	}

	private void _on_button_intelligence_increase_pressed()
	{
		if (_creationCharacterData.PlayerStats.Intelligence < 10 && _remainingPoints > 0) _creationCharacterData.PlayerStats.Intelligence++;
		UpdateLabels();
	}

	private void _on_button_agility_decrease_pressed()
	{
		if (_creationCharacterData.PlayerStats.Agility > 0) _creationCharacterData.PlayerStats.Agility--;
		UpdateLabels();
	}

	private void _on_button_agility_increase_pressed()
	{
		if (_creationCharacterData.PlayerStats.Agility < 10 && _remainingPoints > 0) _creationCharacterData.PlayerStats.Agility++;
		UpdateLabels();
	}

	private void _on_button_luck_decrease_pressed()
	{
		if (_creationCharacterData.PlayerStats.Luck > 0) _creationCharacterData.PlayerStats.Luck--;
		UpdateLabels();
	}

	private void _on_button_luck_increase_pressed()
	{
		if (_creationCharacterData.PlayerStats.Luck < 10 && _remainingPoints > 0) _creationCharacterData.PlayerStats.Luck++;
		UpdateLabels();
	}
	
	private void UpdateLabels()
	{
		_remainingPoints = 35 - _creationCharacterData.PlayerStats.TotalStats();
		_strengthLabel.Text = $"{_creationCharacterData.PlayerStats.Strength}";
		_perceptionLabel.Text = $"{_creationCharacterData.PlayerStats.Perception}";
		_enduranceLabel.Text = $"{_creationCharacterData.PlayerStats.Endurance}";
		_charismaLabel.Text = $"{_creationCharacterData.PlayerStats.Charisma}";
		_intelligenceLabel.Text = $"{_creationCharacterData.PlayerStats.Intelligence}";
		_agilityLabel.Text = $"{_creationCharacterData.PlayerStats.Agility}";
		_luckLabel.Text = $"{_creationCharacterData.PlayerStats.Luck}";
		_remainingPointsLabel.Text = $"{_remainingPoints}";
	}
	
	//public Stats CreationStats
	//{
		//get { return _creationStats; }
		//set { _creationStats = value; }
	//}
	
}







