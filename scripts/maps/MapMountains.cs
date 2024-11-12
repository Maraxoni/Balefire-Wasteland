using Godot;
using System;

public partial class MapMountains : Node2D
{
	public Camera2D camera;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NodePath Path = GetPath();
		GD.Print("Path of MapMountains:", Path.ToString());
		// Load and add the gameplay UI dynamically after the game starts
		PackedScene gameplayUI = (PackedScene)GD.Load("res://scenes/player/UserInterface.tscn");
		Control uiInstance = (Control)gameplayUI.Instantiate(); // Use Instantiate() instead of Instance()
		uiInstance.Visible = true;
		AddChild(uiInstance);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
