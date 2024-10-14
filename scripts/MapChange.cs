using Godot;
using System;

public partial class MapChange : Area2D
{
	[Export] public string PathToFile { get; set; } // Corrected export variable

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_body_entered(Node2D body)
	{
		if (body is PlayerCharacter) // Ensure the body is the player or intended object
		{
			GetTree().ChangeSceneToFile(PathToFile); // Use exported path
		}
	}
}
