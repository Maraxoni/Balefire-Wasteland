using Godot;
using System;

public partial class Map : Node2D
{
	public Camera2D camera;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		camera.SetPosition(PlayerCharacter.GetPosition());
	}
	
	public override void _Input(InputEvent @event)
	{
		
		if (@event.IsActionPressed("esc_key"))
		{
			GetTree().ChangeSceneToFile("res://scenes/MainMenu.tscn");
		}
	}
}
