using Godot;
using System;
using GameProject;

public partial class ItemInventoryMenu : Control
{
	private ItemList _itemInventoryItemList;
	
	private CharacterData _characterData;
	private UserInterface _userInterface;
	
	private Inventory _currentInventory = new Inventory();
	private Inventory _playerInventory;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_userInterface = GetNode<UserInterface>("/root/UserInterface");
		_characterData = GetNode<CharacterData>("/root/GlobalCharacterData");
		
		_itemInventoryItemList = GetNode<ItemList>("PanelContainer/VBoxContainer/MarginContainer2/PanelContainer2/ItemInventoryItemList");
		UpdateInventoryLabel();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void UpdateInventoryLabel()
	{
		if (_currentInventory == null)
		{
			GD.PrintErr("Error: Inventory is null in Container.");
			return;
		}
		_itemInventoryItemList.Clear(); // Clear existing items from the item list
		foreach (var item in _currentInventory.GetItems())
		{
			_itemInventoryItemList.AddItem(item.Name); // Add each item to the item list
		}
	}
	
	private void _on_item_inventory_item_list_item_clicked(int index, Vector2 at_position, int mouse_button_index)
	{

	}
	
	private void _on_item_inventory_item_list_item_activated(int index)
	{
		var _playerInventory = _characterData.PlayerInventory;
		
		if (_currentInventory == null)
		{
			GD.PrintErr("Error: Inventory is null in Container.");
			return;
		}
		var item = _currentInventory.GetItems()[(int)index];
		_playerInventory.AddItem(item);
		_currentInventory.RemoveItem(item);
		Refresh();
	}
	
	private void Refresh()
	{
		UpdateInventoryLabel();
	}
	
	private void _on_take_all_menu_button_pressed()
	{
		var _playerInventory = _characterData.PlayerInventory;
		
		if (_currentInventory == null)
		{
			GD.PrintErr("Error: Inventory is null in Container.");
			return;
		}
		
		// Przenieś wszystkie przedmioty z _currentInventory do _playerInventory
		foreach (var item in _currentInventory.GetItems())
		{
			_playerInventory.AddItem(item);
			_currentInventory.RemoveItem(item);
		}
		
		Refresh();
	}
	
	private void _on_back_item_inventory_menu_button_pressed()
	{
		Node parent = GetParent();
		parent.RemoveChild(this);
		_userInterface.IsItemInventoryMenuVisible = false;
	}
	
	public Inventory CurrentInventory
	{
		get { return _currentInventory; }
		set { _currentInventory = value; }
	}
}
