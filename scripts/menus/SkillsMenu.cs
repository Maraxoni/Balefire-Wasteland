using Godot;
using System;

public partial class SkillsMenu : Control
{
	private Label _strengthLabel;
	private Label _perceptionLabel;
	private Label _enduranceLabel;
	private Label _charismaLabel;
	private Label _intelligenceLabel;
	private Label _agilityLabel;
	private Label _luckLabel;
	private Label _remainingPointsLabel;
	private Label _characterNameLabel;
	
	private UserInterface _userInterface;
	private CharacterData _currentCharacterData;
	private Stats _modifiedPlayerStats;
	private int _skillPoints = 0;
	private int _remainingPoints = 0;
	private string _currentName;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_currentCharacterData = GetNode<CharacterData>("/root/GlobalCharacterData");
		_userInterface = GetNode<UserInterface>("/root/UserInterface");
		
		_modifiedPlayerStats = _currentCharacterData.PlayerStats.Clone();
		_skillPoints = _currentCharacterData.SkillPoints;
		_remainingPoints = _skillPoints;
		
		_currentName = _currentCharacterData.PlayerStats.Name;
		
		NodePath Path = GetPath();
		GD.Print("Path of CharacterCreation:", Path.ToString());

		_strengthLabel = GetNode<Label>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer/Label");
		_perceptionLabel = GetNode<Label>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer2/Label");
		_enduranceLabel = GetNode<Label>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer3/Label");
		_charismaLabel = GetNode<Label>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer4/Label");
		_intelligenceLabel = GetNode<Label>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer5/Label");
		_agilityLabel = GetNode<Label>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer6/Label");
		_luckLabel = GetNode<Label>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer2/HBoxContainer7/Label");
		_remainingPointsLabel = GetNode<Label>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer2/Label9");
		_characterNameLabel = GetNode<Label>("MarginContainer/VBoxContainer/HBoxContainer2/VBoxContainer/CharacterNameLabel");
		
		UpdateLabels();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_confirm_button_pressed()
	{
		ConfirmStats();
		Node parent = GetParent();
		parent.RemoveChild(this);
		_userInterface.IsSkillsMenuVisible = false;
	}
	
	private void _on_back_button_pressed()
	{
		Node parent = GetParent();
		parent.RemoveChild(this);
		_userInterface.IsSkillsMenuVisible = false;
	}
	
	private void ConfirmStats()
	{
		_currentCharacterData.PlayerStats = _modifiedPlayerStats;
		UpdateLabels();
	}
	
	private void _on_button_strength_decrease_pressed()
	{
		if (_modifiedPlayerStats.Strength > _currentCharacterData.PlayerStats.Strength)
		{
			_modifiedPlayerStats.Strength--;
			_remainingPoints++;
		}
		UpdateLabels();
	}

	private void _on_button_strength_increase_pressed()
	{
		if (_modifiedPlayerStats.Strength < 10 && _remainingPoints > 0)
		{
			_modifiedPlayerStats.Strength++;
			_remainingPoints--;
		}
		UpdateLabels();
	}

	private void _on_button_perception_decrease_pressed()
	{
		if (_modifiedPlayerStats.Perception > _currentCharacterData.PlayerStats.Perception)
		{
			_modifiedPlayerStats.Perception--;
			_remainingPoints++;
		}
		UpdateLabels();
	}

	private void _on_button_perception_increase_pressed()
	{
		if (_modifiedPlayerStats.Perception < 10 && _remainingPoints > 0)
		{
			_modifiedPlayerStats.Perception++;
			_remainingPoints--;
		}
		UpdateLabels();
	}

	private void _on_button_endurance_decrease_pressed()
	{
		if (_modifiedPlayerStats.Endurance > _currentCharacterData.PlayerStats.Endurance)
		{
			_modifiedPlayerStats.Endurance--;
			_remainingPoints++;
		}
		UpdateLabels();
	}

	private void _on_button_endurance_increase_pressed()
	{
		if (_modifiedPlayerStats.Endurance < 10 && _remainingPoints > 0)
		{
			_modifiedPlayerStats.Endurance++;
			_remainingPoints--;
		}
		UpdateLabels();
	}

	private void _on_button_charisma_decrease_pressed()
	{
		if (_modifiedPlayerStats.Charisma > _currentCharacterData.PlayerStats.Charisma)
		{
			_modifiedPlayerStats.Charisma--;
			_remainingPoints++;
		}
		UpdateLabels();
	}

	private void _on_button_charisma_increase_pressed()
	{
		if (_modifiedPlayerStats.Charisma < 10 && _remainingPoints > 0)
		{
			_modifiedPlayerStats.Charisma++;
			_remainingPoints--;
		}
		UpdateLabels();
	}

	private void _on_button_intelligence_decrease_pressed()
	{
		if (_modifiedPlayerStats.Intelligence > _currentCharacterData.PlayerStats.Intelligence)
		{
			_modifiedPlayerStats.Intelligence--;
			_remainingPoints++;
		}
		UpdateLabels();
	}

	private void _on_button_intelligence_increase_pressed()
	{
		if (_modifiedPlayerStats.Intelligence < 10 && _remainingPoints > 0)
		{
			_modifiedPlayerStats.Intelligence++;
			_remainingPoints--;
		}
		UpdateLabels();
	}

	private void _on_button_agility_decrease_pressed()
	{
		if (_modifiedPlayerStats.Agility > _currentCharacterData.PlayerStats.Agility)
		{
			_modifiedPlayerStats.Agility--;
			_remainingPoints++;
		}
		UpdateLabels();
	}

	private void _on_button_agility_increase_pressed()
	{
		if (_modifiedPlayerStats.Agility < 10 && _remainingPoints > 0) 
		{
			_modifiedPlayerStats.Agility++;
			_remainingPoints--;
		}
		UpdateLabels();
	}

	private void _on_button_luck_decrease_pressed()
	{
		if (_modifiedPlayerStats.Luck > _currentCharacterData.PlayerStats.Luck)
		{
			_modifiedPlayerStats.Luck--;
			_remainingPoints++;
		} 
		UpdateLabels();
	}

	private void _on_button_luck_increase_pressed()
	{
		if (_modifiedPlayerStats.Luck < 10 && _remainingPoints > 0)
		{
			_modifiedPlayerStats.Luck++;
			_remainingPoints--;
		}
		UpdateLabels();
	}
	
	public void UpdateLabels()
	{
		_strengthLabel.Text = $"{_modifiedPlayerStats.Strength}";
		_perceptionLabel.Text = $"{_modifiedPlayerStats.Perception}";
		_enduranceLabel.Text = $"{_modifiedPlayerStats.Endurance}";
		_charismaLabel.Text = $"{_modifiedPlayerStats.Charisma}";
		_intelligenceLabel.Text = $"{_modifiedPlayerStats.Intelligence}";
		_agilityLabel.Text = $"{_modifiedPlayerStats.Agility}";
		_luckLabel.Text = $"{_modifiedPlayerStats.Luck}";
		_remainingPointsLabel.Text = $"{_remainingPoints}";
		_characterNameLabel.Text = $"{_modifiedPlayerStats.Name}";
	}
	
}
