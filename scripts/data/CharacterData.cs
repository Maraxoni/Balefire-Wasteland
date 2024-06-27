using Godot;
using System;
using GameProject;

public partial class CharacterData : Node
{
	private Inventory _playerInventory = new Inventory();
	private Stats _playerStats = new Stats();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playerStats.Strength = 5;
		_playerStats.Perception = 5;
		_playerStats.Endurance = 5;
		_playerStats.Charisma = 5;
		_playerStats.Intelligence = 5;
		_playerStats.Agility = 5;
		_playerStats.Luck = 5;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	public Inventory PlayerInventory
	{
		get { return _playerInventory; }
	}
	
	public Stats PlayerStats 
	{
		get { return _playerStats; }
	}
	
}
