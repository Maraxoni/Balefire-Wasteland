using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Settings
{
	[JsonPropertyName("resolution")]
	public string Resolution { get; set; }

	[JsonPropertyName("fullscreenmode")]
	public bool FullscreenMode { get; set; }
}
