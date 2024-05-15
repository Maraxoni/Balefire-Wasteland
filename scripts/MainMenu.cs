using Godot;
using System;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_new_game_menu_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/Map.tscn");
	}
	
	private void _on_load_game_menu_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/LoadGameMenu.tscn");
	}
	
	private void _on_settings_menu_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/SettingsMenu.tscn");
	}
	
	private void _on_credits_menu_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/CreditsMenu.tscn");
	}
	
	private void _on_exit_game_menu_button_pressed()
	{
		var scene = ResourceLoader.Load<PackedScene>("res://scenes/Map.tscn").Instantiate();
		GetTree().Root.AddChild(scene);
	}

}
