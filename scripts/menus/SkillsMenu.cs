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
	private CharacterData _modifiedCharacterData;
	private int _skillPoints = 0;
	private int _remainingPoints = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_currentCharacterData = GetNode<CharacterData>("/root/GlobalCharacterData");
		_userInterface = GetNode<UserInterface>("/root/UserInterface");
		
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
		RefreshStats();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_confirm_button_pressed()
	{
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
	
	private void _on_button_strength_decrease_pressed()
	{
		if (_modifiedCharacterData.PlayerStats.Strength > _currentCharacterData.PlayerStats.Strength)
		{
			_modifiedCharacterData.PlayerStats.Strength--;
			_remainingPoints++;
		}
		UpdateLabels();
	}

	private void _on_button_strength_increase_pressed()
	{
		if (_modifiedCharacterData.PlayerStats.Strength < 10 && _remainingPoints > 0)
		{
			_modifiedCharacterData.PlayerStats.Strength++;
			_remainingPoints--;
		}
		UpdateLabels();
	}

	private void _on_button_perception_decrease_pressed()
	{
		if (_modifiedCharacterData.PlayerStats.Perception > _currentCharacterData.PlayerStats.Perception)
		{
			_modifiedCharacterData.PlayerStats.Perception--;
			_remainingPoints++;
		}
		UpdateLabels();
	}

	private void _on_button_perception_increase_pressed()
	{
		if (_modifiedCharacterData.PlayerStats.Perception < 10 && _remainingPoints > 0)
		{
			_modifiedCharacterData.PlayerStats.Perception++;
			_remainingPoints--;
		}
		UpdateLabels();
	}

	private void _on_button_endurance_decrease_pressed()
	{
		if (_modifiedCharacterData.PlayerStats.Endurance > _currentCharacterData.PlayerStats.Endurance)
		{
			_modifiedCharacterData.PlayerStats.Endurance--;
			_remainingPoints++;
		}
		UpdateLabels();
	}

	private void _on_button_endurance_increase_pressed()
	{
		if (_modifiedCharacterData.PlayerStats.Endurance < 10 && _remainingPoints > 0)
		{
			_modifiedCharacterData.PlayerStats.Endurance++;
			_remainingPoints--;
		}
		UpdateLabels();
	}

	private void _on_button_charisma_decrease_pressed()
	{
		if (_modifiedCharacterData.PlayerStats.Charisma > _currentCharacterData.PlayerStats.Charisma)
		{
			_modifiedCharacterData.PlayerStats.Charisma--;
			_remainingPoints++;
		}
		UpdateLabels();
	}

	private void _on_button_charisma_increase_pressed()
	{
		if (_modifiedCharacterData.PlayerStats.Charisma < 10 && _remainingPoints > 0)
		{
			_modifiedCharacterData.PlayerStats.Charisma++;
			_remainingPoints--;
		}
		UpdateLabels();
	}

	private void _on_button_intelligence_decrease_pressed()
	{
		if (_modifiedCharacterData.PlayerStats.Intelligence > _currentCharacterData.PlayerStats.Intelligence)
		{
			_modifiedCharacterData.PlayerStats.Intelligence--;
			_remainingPoints++;
		}
		UpdateLabels();
	}

	private void _on_button_intelligence_increase_pressed()
	{
		if (_modifiedCharacterData.PlayerStats.Intelligence < 10 && _remainingPoints > 0)
		{
			_modifiedCharacterData.PlayerStats.Intelligence++;
			_remainingPoints--;
		}
		UpdateLabels();
	}

	private void _on_button_agility_decrease_pressed()
	{
		if (_modifiedCharacterData.PlayerStats.Agility > _currentCharacterData.PlayerStats.Agility)
		{
			_modifiedCharacterData.PlayerStats.Agility--;
			_remainingPoints++;
		}
		UpdateLabels();
	}

	private void _on_button_agility_increase_pressed()
	{
		if (_modifiedCharacterData.PlayerStats.Agility < 10 && _remainingPoints > 0) 
		{
			_modifiedCharacterData.PlayerStats.Agility++;
			_remainingPoints--;
		}
		UpdateLabels();
	}

	private void _on_button_luck_decrease_pressed()
	{
		if (_modifiedCharacterData.PlayerStats.Luck > 0)
		{
			_modifiedCharacterData.PlayerStats.Luck--;
			_remainingPoints++;
		} 
		UpdateLabels();
	}

	private void _on_button_luck_increase_pressed()
	{
		if (_modifiedCharacterData.PlayerStats.Luck < 10 && _remainingPoints > 0)
		{
			_modifiedCharacterData.PlayerStats.Luck++;
			_remainingPoints--;
		}
		UpdateLabels();
	}
	
	private void RefreshStats()
	{
		_modifiedCharacterData = _currentCharacterData;
		_skillPoints = _modifiedCharacterData.SkillPoints;
		_remainingPoints = _skillPoints;
		UpdateLabels();
	}
	
	private void ConfirmStats()
	{
		_currentCharacterData = _modifiedCharacterData;
		UpdateLabels();
	}
	
	private void UpdateLabels()
	{
		_strengthLabel.Text = $"{_modifiedCharacterData.PlayerStats.Strength}";
		_perceptionLabel.Text = $"{_modifiedCharacterData.PlayerStats.Perception}";
		_enduranceLabel.Text = $"{_modifiedCharacterData.PlayerStats.Endurance}";
		_charismaLabel.Text = $"{_modifiedCharacterData.PlayerStats.Charisma}";
		_intelligenceLabel.Text = $"{_modifiedCharacterData.PlayerStats.Intelligence}";
		_agilityLabel.Text = $"{_modifiedCharacterData.PlayerStats.Agility}";
		_luckLabel.Text = $"{_modifiedCharacterData.PlayerStats.Luck}";
		_remainingPointsLabel.Text = $"{_remainingPoints}";
		_characterNameLabel.Text = $"{_modifiedCharacterData.CharacterName}";
	}
	
}
