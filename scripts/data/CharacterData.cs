using Godot;
using System;
using GameProject;

public partial class CharacterData : Node
{
	private int _healthPoints = 100;
	private int _actionPoints = 100;
	private int _currentLevel = 1;
	private int _experiencePoints = 0;
	
	private Weapon _equippedWeapon;
	private Armor _equippedArmor;
	
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
	
	public Weapon EquippedWeapon
	{
		get { return _equippedWeapon; }
		set { _equippedWeapon = value; }
	}
	
	public Armor EquippedArmor
	{
		get { return _equippedArmor; }
		set { _equippedArmor = value; }
	}
	
	public Inventory PlayerInventory
	{
		get { return _playerInventory; }
	}
	
	public Stats PlayerStats 
	{
		get { return _playerStats; }
	}
	
	public int HealthPoints 
	{
		get { return _healthPoints; }
		set { _healthPoints = value; }
	}
	
	public int ActionPoints 
	{
		get { return _actionPoints; }
		set { _actionPoints = value; }
	}
	
	public int CurrentLevel 
	{
		get { return _currentLevel; }
		set { _currentLevel = value; }
	}
	
	public int ExperiencePoints 
	{
		get { return _experiencePoints; }
		set { _experiencePoints = value; }
	}
}
