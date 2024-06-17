using Godot;
using System;
using System.Text;
using GameProject;

public partial class InventoryMenu : Control
{
	private UserInterface _userInterface;
	private Label _inventoryLabel; // Use underscore for private fields
	private Label _strengthLabel;
	private Label _perceptionLabel;
	private Label _enduranceLabel;
	private Label _charismaLabel;
	private Label _intelligenceLabel;
	private Label _agilityLabel;
	private Label _luckLabel;

	private PlayerCharacter _playerCharacter;

	public void _Ready()
	{
		_userInterface = GetNode<UserInterface>("/root/UserInterface");
		_inventoryLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer2/PanelContainer2/PauseMenuButtonContainer/InventoryList");
		_strengthLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label3"); // Example path, adjust as per your scene structure
		_perceptionLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label5");
		_enduranceLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label7");
		_charismaLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label9");
		_intelligenceLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label11");
		_agilityLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label13");
		_luckLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label15");

		UpdateInventoryLabel();
		UpdateStatsLabels(); // Update stats labels initially
	}

	public void _Process(float delta)
	{
		// You can add processing logic here if needed
	}

	private void _on_back_inventory_menu_button_pressed()
	{
		Node parent = GetParent();
		parent.RemoveChild(this);
		_userInterface.IsInventoryVisible = false;
	}

	private void UpdateInventoryLabel()
	{
		if (_playerCharacter == null)
		{
			GD.PrintErr("Error: PlayerCharacter is null.");
			return;
		}

		var inventory = _playerCharacter.GetInventory();
		if (inventory == null)
		{
			GD.PrintErr("Error: Inventory is null in PlayerCharacter.");
			return;
		}

		StringBuilder labelTextBuilder = new StringBuilder();
		labelTextBuilder.AppendLine("Inventory Items:");

		foreach (var item in inventory.GetItems())
		{
			labelTextBuilder.AppendLine(item.Name);
		}

		_inventoryLabel.Text = labelTextBuilder.ToString();
	}

	private void UpdateStatsLabels()
	{
		if (_playerCharacter == null)
		{
			GD.PrintErr("Error: PlayerCharacter is null in UpdateStatsLabels.");
			return;
		}

		Stats stats = _playerCharacter.GetStats();
		if (stats == null)
		{
			GD.PrintErr("Error: Stats is null in PlayerCharacter in UpdateStatsLabels.");
			return;
		}

		_strengthLabel.Text = $"Strength: {stats.Strength}";
		_perceptionLabel.Text = $"Perception: {stats.Perception}";
		_enduranceLabel.Text = $"Endurance: {stats.Endurance}";
		_charismaLabel.Text = $"Charisma: {stats.Charisma}";
		_intelligenceLabel.Text = $"Intelligence: {stats.Intelligence}";
		_agilityLabel.Text = $"Agility: {stats.Agility}";
		_luckLabel.Text = $"Luck: {stats.Luck}";
		// Print stats to console
		GD.Print($"\nStrength: {stats.Strength}");
		GD.Print($"Perception: {stats.Perception}");
		GD.Print($"Endurance: {stats.Endurance}");
		GD.Print($"Charisma: {stats.Charisma}");
		GD.Print($"Intelligence: {stats.Intelligence}");
		GD.Print($"Agility: {stats.Agility}");
		GD.Print($"Luck: {stats.Luck}");
	}
}
