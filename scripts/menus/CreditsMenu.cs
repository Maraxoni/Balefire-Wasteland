using Godot;
using System;
using GameProject;

public partial class CreditsMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_back_options_menu_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/menus/MainMenu.tscn");
	}
	
	public override void _Input(InputEvent @event)
	{
		
		if (@event.IsActionPressed("esc_key"))
		{
			GetTree().ChangeSceneToFile("res://scenes/menus/MainMenu.tscn");
		}
	}

}
