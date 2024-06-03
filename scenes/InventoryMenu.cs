using Godot;
using System;
using GameProject;

public partial class InventoryMenu : Control
{
	private UserInterface _userInterface;
	private Inventory inventory; // Przechowywanie inwentarza jako pole klasy
	private Label inventoryLabel; // Przechowywanie label jako pole klasy
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_userInterface = GetNode<UserInterface>("/root/UserInterface");
		// Utwórz inwentarz
		inventory = new Inventory();

		// Utwórz przedmioty
		Item apple = new Item("Apple")
		{
			Id = 1,
			ResourcePath = "res://path_to_apple_resource",
			Quantity = 1,
			StackSize = 10,
			IsStackable = true
		};

		Item banana = new Item("Banana")
		{
			Id = 2,
			ResourcePath = "res://path_to_banana_resource",
			Quantity = 1,
			StackSize = 10,
			IsStackable = true
		};

		Item stick = new Item("Stick")
		{
			Id = 3,
			ResourcePath = "res://path_to_stick_resource",
			Quantity = 1,
			StackSize = 10,
			IsStackable = true
		};

		// Dodaj przedmioty do inwentarza
		inventory.AddItem(apple);
		inventory.AddItem(banana);
		inventory.AddItem(stick);
		

		inventoryLabel = GetNode<Label>("VBoxContainer/HBoxContainer/MarginContainer2/PauseMenuButtonContainer/Label2");

		UpdateInventoryLabel();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_back_inventory_menu_button_pressed()
	{
		Node parent = GetParent();
		parent.RemoveChild(this);
		_userInterface.IsInventoryVisible = false;
	}
	
	private void UpdateInventoryLabel()
	{
		// Sprawdź, czy inventoryLabel nie jest nullem
		if (inventoryLabel != null)
		{
			string labelText = "Inventory:\n";
			foreach (var item in inventory.GetAllItems())
			{
				labelText += item.Name + "\n";
			}
			inventoryLabel.Text = labelText;
		}
		else
		{
			GD.Print("inventoryLabel is null");
		}
	}

}
