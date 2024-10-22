using Godot;
using System;
using GameProject;

public partial class ChestBody : StaticBody2D
{
	private Inventory inventory; // Przechowywanie inwentarza
	private bool is_selected;

	// Funkcja _Ready() jest wywoływana przy wczytaniu sceny
	public override void _Ready()
	{

	}

	// Funkcja _Input() odpowiada za interakcję z graczem
	public override void _Input(InputEvent @event)
	{
		if (is_selected && @event.IsActionPressed("left_click"))
		{
			OpenInventoryMenu();
		}
	}

	private void OpenInventoryMenu()
	{

	}

	// Funkcja zamykająca skrzynię i ukrywająca ItemList
	private void CloseInventoryMenu()
	{

	}
}
