using Godot;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public partial class SettingsData : Node
{
	private Settings _settings { get; set; } = new Settings();
	private const string SettingsPath = "assets/settings/settings.json";
	
	private List<Vector2I> _resolutions = new List<Vector2I>
	{
		new Vector2I(1280, 720),
		new Vector2I(1920, 1080),
		new Vector2I(2560, 1440),
		new Vector2I(3840, 2160)
	};
	
	public override void _Ready()
	{
		LoadSettings();
		ApplySettings();
	}

	public void LoadSettings()
	{
		GD.Print("Loading global settings...");
		try
		{
			if (File.Exists(SettingsPath))
			{
				string jsonText = File.ReadAllText(SettingsPath);
				_settings = JsonSerializer.Deserialize<Settings>(jsonText) ?? new Settings();
				GD.Print("Settings loaded successfully.");
			}
			else
			{
				GD.Print("Settings file not found. Using default settings.");
			}
		}
		catch (IOException ioEx)
		{
			GD.PrintErr($"I/O error while loading settings: {ioEx.Message}");
		}
		catch (JsonException jsonEx)
		{
			GD.PrintErr($"JSON deserialization error: {jsonEx.Message}");
		}
	}

	public void SaveSettings()
	{
		GD.Print("Saving global settings...");
		try
		{
			string jsonText = JsonSerializer.Serialize(_settings);
			File.WriteAllText(SettingsPath, jsonText);
			GD.Print("Settings saved successfully.");
		}
		catch (IOException ioEx)
		{
			GD.PrintErr($"I/O error while saving settings: {ioEx.Message}");
		}
	}

	public void ApplySettings()
	{
		GD.Print($"Applying resolution: {_settings.Resolution}, Fullscreen: {_settings.FullscreenMode}");
		
		var currentResolutionIndex = _resolutions.FindIndex(r => r.ToString() == _settings.Resolution);
		var currentResolution = _resolutions[currentResolutionIndex];
		
		DisplayServer.WindowSetSize(currentResolution);
		DisplayServer.WindowSetMode(_settings.FullscreenMode ? DisplayServer.WindowMode.Fullscreen : DisplayServer.WindowMode.Windowed);

		if (!_settings.FullscreenMode)
		{
			Vector2I screenSize = DisplayServer.ScreenGetSize();
			Vector2I newPosition = (screenSize - currentResolution) / 2;
			DisplayServer.WindowSetPosition(newPosition);
		}

		GD.Print("Settings applied.");
	}

	public Settings Settings
	{
		get { return _settings; }
		set { _settings = value; }
	}
	
	public List<Vector2I> Resolutions
	{
		get { return _resolutions; }
		set { _resolutions = value; }
	}
}
