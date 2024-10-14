using Godot;
using System;

public partial class MainMenu : Control
{
	private Control uiInstance;  // Referencja do załadowanego interfejsu UI
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Załaduj scenę UI
		var scene = ResourceLoader.Load<PackedScene>("res://scenes/UserInterface.tscn").Instantiate();
		uiInstance = (Control)scene;  // Przypisz referencję do interfejsu

		// Dodaj UI do drzewa scen
		GetTree().Root.CallDeferred("add_child", uiInstance);

		// Na początku ukryj interfejs
		uiInstance.Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_new_game_menu_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/menus/CharacterCreationMenu.tscn");
	}
	
	private void _on_load_game_menu_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/menus/LoadGameMenu.tscn");
	}
	
	private void _on_settings_menu_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/menus/SettingsMenu.tscn");
	}
	
	private void _on_credits_menu_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/menus/CreditsMenu.tscn");
	}
	
	private void _on_exit_game_menu_button_pressed()
	{
		GetTree().Quit();
	}

}
