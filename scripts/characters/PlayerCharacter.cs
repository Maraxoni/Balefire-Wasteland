using Godot;
using System;
using GameProject;

public partial class PlayerCharacter : CharacterBase
{
	private Vector2 _mousePosition;
	private Vector2 _mouseClickedPosition;
	private Vector2 _destinationPosition;
	
	public bool is_selected = false;
	public bool is_moving = false;

	public override void _Draw()
	{
		Color green = Colors.Green;
		Color blue = new Color("478cbf");
		Color grey = new Color("414042");
		Color transparent = new Color("0000008f");
		
		if(is_selected){
			DrawCircle(new Vector2(0, 10.0f), 25.0f, green);
			DrawCircle(new Vector2(0, 10.0f), 20.0f, transparent);
		}
		if(is_moving){
			DrawCircle(new Vector2(_destinationPosition[0], _destinationPosition[1]), 15.0f, blue);
			DrawCircle(new Vector2(_destinationPosition[0], _destinationPosition[1]), 10.0f, transparent);
		}
		
		if (_characterData?.EquippedWeapon != null)
		{
			string iconPath = _characterData.EquippedWeapon.IconPath;
			if (!string.IsNullOrEmpty(iconPath))
			{
			Texture2D texture = GD.Load<Texture2D>(iconPath);
				 if (texture != null)
				{
					// Get or create a Sprite2D node
				Sprite2D sprite = GetNodeOrNull<Sprite2D>("WeaponSprite");
				if (sprite == null)
				{
					sprite = new Sprite2D();
					sprite.Name = "WeaponSprite";
					AddChild(sprite);
				}

				// Set the texture
				sprite.Texture = texture;

				// Calculate the direction from the character to the cursor
				_mousePosition = GetGlobalMousePosition();
				Vector2 direction = _mousePosition - GlobalPosition;

				// Calculate the angle to rotate the sprite
				float angle = direction.Angle();

				// Set sprite position to rotate around the character
				float radius = 50.0f; // Radius of the sprite orbit
				Vector2 spritePosition = direction.Normalized() * radius;
				sprite.Position = spritePosition;

				// Rotate the sprite to face the cursor
				sprite.Rotation = angle;

				sprite.FlipH = true;
				sprite.FlipV = direction[0] < 0;
				}
			}
		}
		
	}

	public override void _Ready()
	{
		Speed = 200;
		NodePath Path = GetPath();
		GD.Print("Path of PlayerCharacter:", Path.ToString());
		_mousePosition = GetGlobalMousePosition();
		_characterData = GetNode<CharacterData>("/root/GlobalCharacterData");
		
		_character_position = Position; // Set initial character position
		
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_selectedShape = GetNode<CollisionShape2D>("CollisionShape");

		PrintInventoryContents();
	}



	public override void _Input(InputEvent @event)
	{
		
		if (is_selected)
		{
			if (@event.IsActionPressed("right_click"))
			{
				_mouseClickedPosition = GetGlobalMousePosition();
				_character_position = Position; // Initialize _character_position here
				
				if (_mouseClickedPosition[0] > _character_position[0])
				{
					_animatedSprite.FlipH = true;
				}
				else
				{
					_animatedSprite.FlipH = false;
				}
				
				is_moving = true;
			}
			if (@event.IsActionPressed("left_click"))
			{
				UserInterface _userInterface = GetNode<UserInterface>("/root/UserInterface");
				if(_userInterface.IsPauseMenuVisible == false && _userInterface.IsInventoryMenuVisible == false && _userInterface.IsItemInventoryMenuVisible == false && _userInterface.IsDialogueMenuVisible == false){
					PlayerAttack();
				}
			}
		}	
	}

	private void _on_area_2d_input_event(Node viewport, InputEvent @event, long shape_idx)
	{
		if (@event.IsActionPressed("left_click"))
		{
			_selectedShape.Visible = true;
			is_selected = true;
			QueueRedraw();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		_mousePosition = GetGlobalMousePosition();
		//very not effective
		_character_position = Position;
		_destinationPosition = _mouseClickedPosition - _character_position;
		QueueRedraw();
		
		Velocity = Position.DirectionTo(_mouseClickedPosition) * Speed;
		if (is_moving)
		{
			QueueRedraw();
			Velocity = Position.DirectionTo(_mouseClickedPosition) * Speed;
			// LookAt(_mouseClickedPosition);
			if (Position.DistanceTo(_mouseClickedPosition) > 10)
			{
				MoveAndSlide();
			}
			else
			{
				is_moving = false;
			}
		}

		if (is_moving == true)
		{
			_animatedSprite.Play("walk");
		}
		else
		{
			_animatedSprite.Play("default");
		}

	}
	
	private void PlayerAttack(){
		if (_characterData.EquippedWeapon != null && _characterData.EquippedWeapon.IsRanged == true)
		{
			SpawnBullet();
		} else
		if (_characterData.EquippedWeapon != null && _characterData.EquippedWeapon.IsRanged == false)
		{
			SpawnShockwave();
		} else
		{
			
		}
	}
	
	private void SpawnBullet()
	{
		// Calculate direction to spawn bullet
		Vector2 direction = (_mousePosition - GlobalPosition).Normalized();
		
		// Create the bullet instance from the Bullet scene
		Bullet bullet = GD.Load<PackedScene>("res://scenes/Bullet.tscn").Instantiate<Bullet>();
		bullet.Position = GlobalPosition; // Start the bullet at the player position
		bullet.Initialize(direction); // Set the direction of the bullet

		// Add the bullet to the scene
		GetParent().AddChild(bullet);
	}
	
	private void SpawnShockwave()
	{
		// Calculate direction to spawn bullet
		Vector2 direction = (_mousePosition - GlobalPosition).Normalized();
		
		// Create the bullet instance from the Bullet scene
		Bullet bullet = GD.Load<PackedScene>("res://scenes/Bullet.tscn").Instantiate<Bullet>();
		bullet.Position = GlobalPosition; // Start the bullet at the player position
		bullet.Initialize(direction); // Set the direction of the bullet

		// Add the bullet to the scene
		GetParent().AddChild(bullet);
	}
	
	public void TakeDamage(int damage)
	{
		_characterData.HealthPoints = _characterData.HealthPoints - damage;
		
		if (_characterData.HealthPoints <= 0)
		{
			HandleDeath();
		}
	}
	
	private void HandleDeath()
	{
		GetTree().ChangeSceneToFile("res://scenes/menus/GameLostMenu.tscn");
		GD.Print("Player has died.");
		QueueFree();
	}
}
