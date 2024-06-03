using Godot;
using System;

public partial class CharacterCreationMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _Input(InputEvent @event)
	{
		
		if (@event.IsActionPressed("esc_key"))
		{
			GetTree().ChangeSceneToFile("res://scenes/menus/MainMenu.tscn");
		}
	}
	
	private void _on_back_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/menus/MainMenu.tscn");
	}

	private void _on_create_char_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/maps/MapVault.tscn");
	}
}
