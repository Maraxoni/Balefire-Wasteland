using Godot;
using System.Collections.Generic;

public partial class NonPlayerCharacter : CharacterBase
{
	private bool _isHovered = false;
	
	private const float InteractionRange = 100.0f;
	
	public override void _Ready()
	{
	}

	// Handle input events
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.ButtonIndex == MouseButton.Left && eventMouseButton.Pressed && _isHovered)
			{
				if (IsPlayerInRange())
				{
					GetTree().Root.GetNode<UserInterface>("UserInterface").ToggleDialogueMenu("vaultmember", "start");
				}
				else
				{
					GD.Print("Player is out of range of the NPC.");
				}
			}
		}
	}
	
	public override void _Draw(){
		Color blue = Colors.LightBlue;
		Color transparent = new Color("0000008f");
		
		if(_isHovered){
			DrawCircle(new Vector2(0, 10.0f), 25.0f, blue);
			DrawCircle(new Vector2(0, 10.0f), 20.0f, transparent);
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
