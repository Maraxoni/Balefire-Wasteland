using Godot;
using System;
using System.Collections.Generic;

public partial class SettingsMenu : Control
{
	private SettingsData _globalSettingsData;
	
	private Label _resolutionLabel;
	private Label _screenTypeLabel;
	
	private int _currentResolutionIndex = 0;
	private bool _isFullscreen = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_globalSettingsData = GetNode<SettingsData>("/root/GlobalSettingsData");
		
		_resolutionLabel = GetNode<Label>("MarginContainer/TabContainer/Graphics/ResolutionContainer/ResolutionLabel");
		_screenTypeLabel = GetNode<Label>("MarginContainer/TabContainer/Graphics/ScreenTypeContainer/ScreenTypeLabel");
		
		LoadSettingsData();
		
		UpdateResolutionLabel();
		UpdateScreenTypeLabel();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void LoadSettingsData()
	{
		GD.Print("Loading settings from GlobalSettings...");

		_currentResolutionIndex = _globalSettingsData.Resolutions.FindIndex(r => r.ToString() == _globalSettingsData.Settings.Resolution);
		if (_currentResolutionIndex == -1)
		{
			_currentResolutionIndex = 0; // Jeśli rozdzielczość nie pasuje, użyj domyślnej
		}

		_isFullscreen = _globalSettingsData.Settings.FullscreenMode;

		UpdateResolutionLabel();
		UpdateScreenTypeLabel();
	}

	private void SaveSettingsData()
	{
		GD.Print("Saving settings to GlobalSettings...");

		_globalSettingsData.Settings.Resolution = _globalSettingsData.Resolutions[_currentResolutionIndex].ToString();
		_globalSettingsData.Settings.FullscreenMode = _isFullscreen;

		_globalSettingsData.SaveSettings();
		_globalSettingsData.ApplySettings();
	}
	
	private void _on_save_settings_menu_button_pressed()
	{
		SaveSettingsData();
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
		var resolutions = _globalSettingsData.Resolutions;
		var currentResolution = resolutions[_currentResolutionIndex];
		_resolutionLabel.Text = $"{currentResolution.X}x{currentResolution.Y}";
	}

	private void UpdateScreenTypeLabel()
	{
		_screenTypeLabel.Text = _isFullscreen ? "Fullscreen" : "Windowed";
	}

	private void _on_screen_type_decrease_button_pressed()
	{
		_isFullscreen = false;
		UpdateScreenTypeLabel();
	}

	private void _on_screen_type_increase_button_pressed()
	{
		_isFullscreen = true;
		UpdateScreenTypeLabel();
	}

	private void _on_resolution_decrease_button_pressed()
	{
		_currentResolutionIndex = Mathf.Max(0, _currentResolutionIndex - 1);
		UpdateResolutionLabel();
	}

	private void _on_resolution_increase_button_pressed()
	{
		var resolutions = _globalSettingsData.Resolutions;
		_currentResolutionIndex = Mathf.Min(resolutions.Count - 1, _currentResolutionIndex + 1);
		UpdateResolutionLabel();
	}

}
