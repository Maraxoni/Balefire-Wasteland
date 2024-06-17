using Godot;
using System;

public partial class MapVault : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var player = ResourceLoader.Load<PackedScene>("res://scenes/PlayerCharacter.tscn").Instantiate();
		GetTree().Root.AddChild(player);
		var scene = ResourceLoader.Load<PackedScene>("res://scenes/UserInterface.tscn").Instantiate();
		GetTree().Root.AddChild(scene);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
}
