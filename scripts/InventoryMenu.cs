using Godot;
using System;

public partial class InventoryMenu : Control
{
	private PlayerCharacter _playerCharacter;
	private Label _strengthLabel;
	private Label _perceptionLabel;
	private Label _enduranceLabel;
	private Label _charismaLabel;
	private Label _intelligenceLabel;
	private Label _agilityLabel;
	private Label _luckLabel;
	private ItemList _itemList;

	public override void _Ready()
	{
		_playerCharacter = GetNode<PlayerCharacter>("/root/PlayerCharacter");
		if (_playerCharacter == null)
		{
			GD.PrintErr("Error: PlayerCharacter node not found.");
			return;
		}
		
		_itemList = GetNode<ItemList>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer2/PanelContainer2/ItemList");
		_strengthLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label3");
		_perceptionLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label5");
		_enduranceLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label7");
		_charismaLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label9");
		_intelligenceLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label11");
		_agilityLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label13");
		_luckLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label15");
		
		// Initially update the inventory and stats labels
		UpdateInventoryLabel();
		UpdateStatsLabels();
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
		_itemList.Clear(); // Clear existing items from the item list
		foreach (var item in inventory.GetItems())
		{
			_itemList.AddItem(item.Name); // Add each item to the item list
		}
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
	}

	public void Refresh()
	{
		UpdateInventoryLabel();
		UpdateStatsLabels();
	}
	
	private void _on_item_list_item_clicked(long index, Vector2 at_position, long mouse_button_index)
	{
		if (_playerCharacter == null)
		{
			GD.PrintErr("Error: PlayerCharacter is null in _on_item_list_item_clicked.");
			return;
		}
		var inventory = _playerCharacter.GetInventory();
		if (inventory == null)
		{
			GD.PrintErr("Error: Inventory is null in PlayerCharacter in _on_item_list_item_clicked.");
			return;
		}
		var item = inventory.GetItems()[(int)index];
		inventory.RemoveItem(item);
		Refresh();
	}
}



