using Godot;
using System;

public partial class CharacterCreationMenu : Control
{
	private Label _strengthLabel;
	private Label _perceptionLabel;
	private Label _enduranceLabel;
	private Label _charismaLabel;
	private Label _intelligenceLabel;
	private Label _agilityLabel;
	private Label _luckLabel;
	private Label _remainingPointsLabel;
	
	private Button _strengthButtonIncrease;
	private Button _perceptionButtonIncrease;
	private Button _enduranceButtonIncrease;
	private Button _charismaButtonIncrease;
	private Button _intelligenceButtonIncrease;
	private Button _agilityButtonIncrease;
	private Button _luckButtonIncrease;
	
	private Button _strengthButtonDecrease;
	private Button _perceptionButtonDecrease;
	private Button _enduranceButtonDecrease;
	private Button _charismaButtonDecrease;
	private Button _intelligenceButtonDecrease;
	private Button _agilityButtonDecrease;
	private Button _luckButtonDecrease;
	
	private int _strength = 5;
	private int _perception = 5;
	private int _endurance = 5;
	private int _charisma = 5;
	private int _intelligence = 5;
	private int _agility = 5;
	private int _luck = 5;
	private int _remainingPoints = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_strengthLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer/Label");
		_perceptionLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer2/Label");
		_enduranceLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer3/Label");
		_charismaLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer4/Label");
		_intelligenceLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer5/Label");
		_agilityLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer6/Label");
		_luckLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer7/Label");
		_remainingPointsLabel = GetNode<Label>("VBoxContainer/HBoxContainer/VBoxContainer2/Label9");
		//_strengthButtonIncrease = GetNode<Button>("");
		//_perceptionButtonIncrease = GetNode<Button>("");
		//_enduranceButtonIncrease = GetNode<Button>("");
		//_charismaButtonIncrease = GetNode<Button>("");
		//_intelligenceButtonIncrease = GetNode<Button>("");
		//_agilityButtonIncrease = GetNode<Button>("$VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer6/Button4");
		//_luckButtonIncrease = GetNode<Button>("$VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer7/Button4");
		//_strengthButtonDecrease = GetNode<Button>("");
		//_perceptionButtonDecrease = GetNode<Button>("");
		//_enduranceButtonDecrease = GetNode<Button>("");
		//_charismaButtonDecrease = GetNode<Button>("");
		//_intelligenceButtonDecrease = GetNode<Button>("");
		//_agilityButtonDecrease = GetNode<Button>("$VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer6/Button3");
		//_luckButtonDecrease = GetNode<Button>("$VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer7/Button3");
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
		if (_strength > 0) _strength--;
		UpdateLabels();
	}


	private void _on_button_strength_increase_pressed()
	{
		if (_strength < 10 && _remainingPoints > 0) _strength++;
		UpdateLabels();
	}


	private void _on_button_perception_decrease_pressed()
	{
		if (_perception > 0) _perception--;
		UpdateLabels();
	}


	private void _on_button_perception_increase_pressed()
	{
		if (_perception < 10 && _remainingPoints > 0) _perception++;
		UpdateLabels();
	}


	private void _on_button_endurance_decrease_pressed()
	{
		if (_endurance > 0) _endurance--;
		UpdateLabels();
	}


	private void _on_button_endurance_increase_pressed()
	{
		if (_endurance < 10 && _remainingPoints > 0) _endurance++;
		UpdateLabels();
	}


	private void _on_button_charisma_decrease_pressed()
	{
		if (_charisma > 0) _charisma--;
		UpdateLabels();
	}


	private void _on_button_charisma_increase_pressed()
	{
		if (_charisma < 10 && _remainingPoints > 0) _charisma++;
		UpdateLabels();
	}


	private void _on_button_intelligence_decrease_pressed()
	{
		if (_intelligence > 0) _intelligence--;
		UpdateLabels();
	}


	private void _on_button_intelligence_increase_pressed()
	{
		if (_intelligence < 10 && _remainingPoints > 0) _intelligence++;
		UpdateLabels();
	}


	private void _on_button_agility_decrease_pressed()
	{
		if (_agility > 0) _agility--;
		UpdateLabels();
	}


	private void _on_button_agility_increase_pressed()
	{
		if (_agility < 10 && _remainingPoints > 0) _agility++;
		UpdateLabels();
	}


	private void _on_button_luck_decrease_pressed()
	{
		if (_luck > 0) _luck--;
		UpdateLabels();
	}


	private void _on_button_luck_increase_pressed()
	{
		if (_luck < 10 && _remainingPoints > 0) _luck++;
		UpdateLabels();
	}
	
	private void UpdateLabels()
	{
		_remainingPoints = 35 - (_strength + _perception + _endurance + _charisma + _intelligence + _agility + _luck);
		_strengthLabel.Text = $"{_strength}";
		_perceptionLabel.Text = $"{_perception}";
		_enduranceLabel.Text = $"{_endurance}";
		_charismaLabel.Text = $"{_charisma}";
		_intelligenceLabel.Text = $"{_intelligence}";
		_agilityLabel.Text = $"{_agility}";
		_luckLabel.Text = $"{_luck}";
		_remainingPointsLabel.Text = $"{_remainingPoints}";
	}
}



