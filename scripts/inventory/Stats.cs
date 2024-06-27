using Godot;
using System;

public partial class Stats : Node
{
	
	private String _name;
	private int _strength;
	private int _perception;
	private int _endurance;
	private int _charisma;
	private int _intelligence;
	private int _agility;
	private int _luck;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	// Methods to set and get attributes
	public String Name 
	{
		get { return _name; }
		set { _name = value; }
	}
	
	// Methods to set and get attributes
	public int Strength 
	{
		get { return _strength; }
		set { _strength = value; }
	}

	public int Perception 
	{
		get { return _perception; }
		set { _perception = value; }
	}

	public int Endurance 
	{
		get { return _endurance; }
		set { _endurance = value; }
	}

	public int Charisma 
	{
		get { return _charisma; }
		set { _charisma = value; }
	}

	public int Intelligence 
	{
		get { return _intelligence; }
		set { _intelligence = value; }
	}

	public int Agility 
	{
		get { return _agility; }
		set { _agility = value; }
	}

	public int Luck 
	{
		get { return _luck; }
		set { _luck = value; }
	}
	
	public int TotalStats()
	{
		return _strength + _perception + _endurance + _charisma + _intelligence + _agility + _luck;
	}
	
}
