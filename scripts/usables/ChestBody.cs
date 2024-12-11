using Godot;
using System;
using GameProject;

public partial class ChestBody : StaticBody2D
{
	private Inventory _chestInventory = new Inventory();
	
	private bool _isHovered = false;
	
	private const float InteractionRange = 100.0f;
	
	public override void _Ready()
	{
		_chestInventory.AddItem(new Caps(50));
		_chestInventory.AddItem(new Caps(500));
		_chestInventory.AddItem(new Knife(1));
		_chestInventory.AddItem(new MilitaryRifle(1));
		_chestInventory.AddItem(new MilitaryRifle(1));
	}

	// Handle input events
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			var PlayerCharacter = GetPlayer();
			if (eventMouseButton.ButtonIndex == MouseButton.Left && eventMouseButton.Pressed && _isHovered)
			{
				if (IsPlayerInRange())
				{
					GetTree().Root.GetNode<UserInterface>("UserInterface").ToggleItemInventoryMenu(_chestInventory);
				}
				else
				{
					GD.Print("Player is out of range of the chest.");
				}
			}
		}
	}
	
	public override void _Draw(){
		Color yellow = Colors.Yellow;
		Color transparent = new Color("0000008f");
		
		if(_isHovered){
			DrawCircle(new Vector2(0, 0.0f), 25.0f, yellow);
			DrawCircle(new Vector2(0, 0.0f), 20.0f, transparent);
		}
	}
	
	private void _on_area_2d_mouse_entered(){
		_isHovered = true;
		QueueRedraw();
	}

	private void _on_area_2d_mouse_exited(){
		_isHovered = false;
		QueueRedraw();
	}
	
	private bool IsPlayerInRange()
	{
		var player = GetPlayer();
		if (player == null)
		{
			GD.Print("Player not found!");
			return false;
		}

		// Oblicz dystans między graczem a skrzynią
		float distance = GlobalPosition.DistanceTo(player.GlobalPosition);

		// Sprawdź, czy dystans jest w zasięgu
		return distance <= InteractionRange;
	}
	
	protected PlayerCharacter GetPlayer()
	{
		return GetNodeOrNull<PlayerCharacter>("../PlayerCharacter");
	}
	
}
