using Godot;
using System;
using GameProject;

public partial class CharacterData : Node
{
	[Export]
	private string _characterName = "";
	private Inventory _playerInventory = new Inventory();
	private Stats _playerStats = new Stats();
	
	private float _healthPoints = 100;
	private float _actionPoints = 100;
	private float _experiencePoints = 0;
	private int _currentLevel = 1;
	private int _skillPoints = 0;
	
	private float _maxHealthPoints = 100;
	private float _maxActionPoints = 100;
	private float _maxExperiencePoints = 100;
	
	private Weapon _equippedWeapon;
	private Armor _equippedArmor;
	
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
		
		CalculateStats();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	private void CalculateStats()
	{
		_maxHealthPoints = 50 + _playerStats.Endurance * 10;
		_maxActionPoints = 50 + _playerStats.Agility * 10;
		_maxExperiencePoints = _currentLevel * 100;
	}
	
	private void CheckLevelUp()
	{
		if (_experiencePoints >= _maxExperiencePoints)
		{
			_experiencePoints -= _maxExperiencePoints; // Zachowaj nadwyżkę doświadczenia
			_currentLevel++; // Zwiększ poziom
			_skillPoints++;
			CalculateStats(); // Przelicz statystyki dla nowego poziomu
			
			GD.Print($"Current level: {_currentLevel}.");
		}
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
		set { _playerStats = value; }
	}
	
	public float HealthPoints 
	{
		get { return _healthPoints; }
		set { _healthPoints = value; }
	}
	
	public float MaxHealthPoints 
	{
		get { return _maxHealthPoints; }
		set { _maxHealthPoints = value; }
	}
	
	public float ActionPoints 
	{
		get { return _actionPoints; }
		set { _actionPoints = value; }
	}
	
	public float MaxActionPoints 
	{
		get { return _maxActionPoints; }
		set { _maxActionPoints = value; }
	}
	
	public int CurrentLevel 
	{
		get { return _currentLevel; }
		set { _currentLevel = value; }
	}
	
	public float ExperiencePoints 
	{
		get { return _experiencePoints; }
		set 
		{ 
			_experiencePoints = value; 
			CheckLevelUp(); 
		}
	}
	
	public float MaxExperiencePoints
	{
		get { return _maxExperiencePoints; }
		set { _maxExperiencePoints = value; }
	}
	
	public int SkillPoints
	{
		get { return _skillPoints; }
		set { _skillPoints = value; }
	}
	
	public string CharacterName
	{
		get { return _characterName; }
		set { _characterName = value; }
	}
}
