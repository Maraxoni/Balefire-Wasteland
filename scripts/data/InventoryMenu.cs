using Godot;
using System;
using GameProject;

public partial class InventoryMenu : Control
{
	private UserInterface _userInterface;
	//private PlayerCharacter _playerCharacter;
	
	private CharacterData _characterData;
	
	private Label _strengthLabel;
	private Label _perceptionLabel;
	private Label _enduranceLabel;
	private Label _charismaLabel;
	private Label _intelligenceLabel;
	private Label _agilityLabel;
	private Label _luckLabel;
	private Label _nameLabel;
	private ItemList _itemList;

	public override void _Ready()
	{
		_userInterface = GetNode<UserInterface>("/root/UserInterface");
		_characterData = GetNode<CharacterData>("/root/GlobalCharacterData");
		
		_itemList = GetNode<ItemList>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer2/PanelContainer2/ItemList");
		_strengthLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label3");
		_perceptionLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label5");
		_enduranceLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label7");
		_charismaLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label9");
		_intelligenceLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label11");
		_agilityLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label13");
		_luckLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/MarginContainer/PanelContainer/PauseMenuButtonContainer/Label15");
		_nameLabel = GetNode<Label>("PanelContainer/VBoxContainer/HBoxContainer/PanelContainer/CharacterName");
		
		// Initially update the inventory and stats labels
		UpdateInventoryLabel();
		UpdateStatsLabels();
	}

	private void UpdateInventoryLabel()
	{
		var inventory = _characterData.PlayerInventory;
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
		Stats stats = _characterData.PlayerStats;
		if (stats == null)
		{
			GD.PrintErr("Error: Stats is null in PlayerCharacter in UpdateStatsLabels.");
			return;
		}
		_strengthLabel.Text = $"{stats.Strength}";
		_perceptionLabel.Text = $"{stats.Perception}";
		_enduranceLabel.Text = $"{stats.Endurance}";
		_charismaLabel.Text = $"{stats.Charisma}";
		_intelligenceLabel.Text = $"{stats.Intelligence}";
		_agilityLabel.Text = $"{stats.Agility}";
		_luckLabel.Text = $"{stats.Luck}";
		_nameLabel.Text = $"{stats.Name}";
	}

	public void Refresh()
	{
		UpdateInventoryLabel();
		UpdateStatsLabels();
	}
	
	private void _on_item_list_item_clicked(long index, Vector2 at_position, long mouse_button_index)
	{
		var inventory = _characterData.PlayerInventory;
		if (inventory == null)
		{
			GD.PrintErr("Error: Inventory is null in PlayerCharacter in _on_item_list_item_clicked.");
			return;
		}
		var item = inventory.GetItems()[(int)index];
		inventory.RemoveItem(item);
		Refresh();
	}
	
	private void _on_back_inventory_menu_button_pressed()
	{
		Node parent = GetParent();
		parent.RemoveChild(this);
		_userInterface.IsInventoryVisible = false;
	}

}
