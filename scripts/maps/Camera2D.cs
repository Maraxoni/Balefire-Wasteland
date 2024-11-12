using Godot;
using System;

public partial class Camera2D : Godot.Camera2D
{
	
	// Prędkość poruszania się kamery
	public float CameraSpeed = 200.0f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Pobierz pozycję myszy
		Vector2 mousePosition = GetGlobalMousePosition();

		// Pobierz rozmiar ekranu
		Vector2 screenSize = GetViewportRect().Size;

		// Ustaw szybkość przesunięcia kamery w zależności od odległości od krawędzi ekranu
		Vector2 moveVector = new Vector2();

		if (mousePosition[0] < 10)
			moveVector[0] = -1;
		else if (mousePosition[0] > screenSize[0] - 10)
			moveVector[0] = 1;

		if (mousePosition[1] < 10)
			moveVector[1] = -1;
		else if (mousePosition[1] > screenSize[1] - 10)
			moveVector[1] = 1;

		// Przesuń kamerę
		Position += new Vector2(moveVector[0] * CameraSpeed * (float)delta, moveVector[1] * CameraSpeed * (float)delta);
	}
}
