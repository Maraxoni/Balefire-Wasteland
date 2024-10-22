using Godot;
using System;

public partial class MapVault : Node2D
{
	private Control _userInterface;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NodePath Path = GetPath();
		GD.Print("Path of MapVault:", Path.ToString());
		// Preload the PauseMenu scene once
		// Załaduj scenę UI
		var scene = ResourceLoader.Load<PackedScene>("res://scenes/player/UserInterface.tscn").Instantiate();
		_userInterface = (Control)scene;  // Przypisz referencję do interfejsu

		// Dodaj UI do drzewa scen
		GetTree().Root.CallDeferred("add_child", _userInterface);

		// Na początku ukryj interfejs
		_userInterface.Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
}
