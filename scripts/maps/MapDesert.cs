using Godot;
using System;

public partial class MapDesert : Node2D
{
	private Control _userInterface;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NodePath Path = GetPath();
		GD.Print("Path of MapDesert:", Path.ToString());

		var scene = ResourceLoader.Load<PackedScene>("res://scenes/player/UserInterface.tscn").Instantiate();
		_userInterface = (Control)scene;

		GetTree().Root.CallDeferred("add_child", _userInterface);

		_userInterface.Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
}
