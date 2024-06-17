using Godot;
using System;

public partial class PauseMenu : Control
{
	private UserInterface _userInterface;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_userInterface = GetNode<UserInterface>("/root/UserInterface");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_resume_menu_button_pressed()
	{
		Node parent = GetParent();
		parent.RemoveChild(this);
		_userInterface.isPauseMenuVisible = false;
	}

	private void _on_settings_menu_button_pressed()
	{
		
		GetTree().ChangeSceneToFile("res://scenes/menus/SettingsMenu.tscn");
		// Hide and then exit the UserInterface
		_userInterface.isPauseMenuVisible = false;
		_userInterface.Hide();
		_userInterface.QueueFree(); // Free the UserInterface node from the scene tree
		Node parent = GetParent();
		parent.RemoveChild(this);
	}

	private void _on_exit_to_main_menu_menu_button_pressed()
	{
		
		GetTree().ChangeSceneToFile("res://scenes/menus/MainMenu.tscn");
		// Hide and then exit the UserInterface
		_userInterface.isPauseMenuVisible = false;
		_userInterface.Hide();
		_userInterface.QueueFree(); // Free the UserInterface node from the scene tree
		Node parent = GetParent();
		parent.RemoveChild(this);
	}

	private void _on_exit_game_menu_button_pressed()
	{
		GetTree().Quit();
	}
	
}



