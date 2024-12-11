using Godot;
using System;
using System.Collections.Generic;

public partial class SettingsMenu : Control
{
	private Label _resolutionLabel;
	private Label _screenTypeLabel;
	
	private List<Vector2I> resolutions = new List<Vector2I>
	{
		new Vector2I(1280, 720),
		new Vector2I(1920, 1080),
		new Vector2I(2560, 1440),
		new Vector2I(3840, 2160)
	};
	
	private int _currentResolutionIndex = 0;
	private bool _isFullscreen = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_resolutionLabel = GetNode<Label>("MarginContainer/TabContainer/Graphics/ResolutionContainer/ResolutionLabel");
		_screenTypeLabel = GetNode<Label>("MarginContainer/TabContainer/Graphics/ScreenTypeContainer/ScreenTypeLabel");

		UpdateResolutionLabel();
		UpdateScreenTypeLabel();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_back_options_menu_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/menus/MainMenu.tscn");
	}
	
	public override void _Input(InputEvent @event)
	{
		
		if (@event.IsActionPressed("esc_key"))
		{
			GetTree().ChangeSceneToFile("res://scenes/menus/MainMenu.tscn");
		}
	}
	
	private void UpdateResolutionLabel()
	{
		var currentResolution = resolutions[_currentResolutionIndex];
		_resolutionLabel.Text = $"{currentResolution.X}x{currentResolution.Y}";
	}

	private void UpdateScreenTypeLabel()
	{
		_screenTypeLabel.Text = _isFullscreen ? "Fullscreen" : "Windowed";
	}

	private void ApplySettings()
	{
		var currentResolution = resolutions[_currentResolutionIndex];

		// Zmiana rozdzielczości i trybu pełnoekranowego
		DisplayServer.WindowSetSize(currentResolution);
		DisplayServer.WindowSetMode(_isFullscreen ? DisplayServer.WindowMode.Fullscreen : DisplayServer.WindowMode.Windowed);

		// Wyśrodkowanie okna (tylko w trybie okienkowym)
		if (!_isFullscreen)
		{
			var screenSize = DisplayServer.ScreenGetSize();
			var newPosition = screenSize / 2 - currentResolution / 2;
			DisplayServer.WindowSetPosition(newPosition);
		}
	}
	
	private void _on_screen_type_decrease_button_pressed()
	{
		_isFullscreen = false;
		UpdateScreenTypeLabel();
		ApplySettings();
	}

	private void _on_screen_type_increase_button_pressed()
	{
		_isFullscreen = true;
		UpdateScreenTypeLabel();
		ApplySettings();
	}

	private void _on_resolution_decrease_button_pressed()
	{
		_currentResolutionIndex = Mathf.Max(0, _currentResolutionIndex - 1);
		UpdateResolutionLabel();
		ApplySettings();
	}

	private void _on_resolution_increase_button_pressed()
	{
		_currentResolutionIndex = Mathf.Min(resolutions.Count - 1, _currentResolutionIndex + 1);
		UpdateResolutionLabel();
		ApplySettings();
	}

}
