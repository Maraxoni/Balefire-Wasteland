using Godot;
using System;
using GameProject;
using System.Threading;

public partial class UserInterface : Control
{
	private bool _isPauseMenuVisible = false;
	private bool _isInventoryMenuVisible = false;
	private bool _isItemInventoryMenuVisible = false;
	private bool _isDialogueMenuVisible = false;
	private bool _isSkillsMenuVisible = false;
	
	private CharacterData _currentCharacterData;
	private Control pauseMenu;
	private InventoryMenu inventoryMenu;
	private ItemInventoryMenu itemInventoryMenu;
	private DialogueMenu dialogueMenu;
	private SkillsMenu skillsMenu;
	
	private Godot.Camera2D _interfaceCamera;
	private float _cameraSpeed = 400.0f;
	private float _scrollSpeed = 20.0f;
	private float _scaleChange = 0.05f;
	private float _scaleRatio = 1;
	
	float leftThreshold;
	float rightThreshold;
	float upThreshold;
	float downThreshold;	
	//Bars
	private ProgressBar _healthProgressBar;
	private ProgressBar _actionProgressBar;
	private ProgressBar _experienceProgressBar;
	
	private Label _levelLabel;
	// Map boundaries
	private Vector2 _screenSize;
	private Vector2 _defaultResolution = new Vector2(1920, 1080);
	
	private Vector2 _mapSize = new Vector2(32*64, 32*64);
	private Vector2 _mapMinBounds;
	private Vector2 _mapMaxBounds;
	
	public override void _Ready()
	{
		// Preload the PauseMenu scene once
		var pauseScene = ResourceLoader.Load<PackedScene>("res://scenes/menus/PauseMenu.tscn");
		pauseMenu = pauseScene.Instantiate<Control>();
		var dialogueScene = ResourceLoader.Load<PackedScene>("res://scenes/menus/DialogueMenu.tscn");
		dialogueMenu = dialogueScene.Instantiate<DialogueMenu>();
		var inventoryScene = ResourceLoader.Load<PackedScene>("res://scenes/menus/InventoryMenu.tscn");
		inventoryMenu = inventoryScene.Instantiate<InventoryMenu>();
		var itemInventoryScene = ResourceLoader.Load<PackedScene>("res://scenes/menus/ItemInventoryMenu.tscn");
		itemInventoryMenu = itemInventoryScene.Instantiate<ItemInventoryMenu>();
		var skillsScene = ResourceLoader.Load<PackedScene>("res://scenes/menus/SkillsMenu.tscn");
		skillsMenu = skillsScene.Instantiate<SkillsMenu>();
		
		_healthProgressBar = GetNode<ProgressBar>("CanvasLayer/MarginContainer/VBoxContainer/HBoxContainer/HealthProgressBar");
		_actionProgressBar = GetNode<ProgressBar>("CanvasLayer/MarginContainer/VBoxContainer/HBoxContainer/ActionProgressBar");
		_experienceProgressBar = GetNode<ProgressBar>("CanvasLayer/MarginContainer/VBoxContainer/HBoxContainer3/ExperienceProgressBar");
		
		_levelLabel = GetNode<Label>("CanvasLayer/MarginContainer/VBoxContainer/HBoxContainer3/LevelLabel");
		
		_interfaceCamera = GetNode<Godot.Camera2D>("InterfaceCamera");
		
		_screenSize = GetViewportRect().Size;
		_scaleRatio = _screenSize.X / _defaultResolution.X;
		_interfaceCamera.Zoom = _interfaceCamera.Zoom * _scaleRatio;
		
		CenterCameraOnPlayer();
		AdjustSceneScale();
		UpdateCameraScale();
	}
	
	
	// Metoda dostosowująca skalę sceny
	private void AdjustSceneScale()
	{
		// Pobierz aktualny rozmiar ekranu
		_screenSize = GetViewportRect().Size;

		// Oblicz stosunek rozdzielczości
		float scaleX = _screenSize.X / _defaultResolution.X;
		float scaleY = _screenSize.Y / _defaultResolution.Y;

		// Możesz zdecydować, którą oś preferujesz (X lub Y), aby nie zdeformować elementów
		float scaleFactor = Mathf.Min(scaleX, scaleY);

		// Ustaw skalowanie sceny
		this.Scale = new Vector2(scaleFactor, scaleFactor);
	}
	
	public void CenterCameraOnPlayer()
	{
		var playerCharacter = FindPlayerCharacterInMaps();
		NodePath Path = GetPath();
		GD.Print("Path of UserInterface:", Path.ToString());
		if (playerCharacter != null)
		{
			_screenSize = GetViewportRect().Size;
			// Calculate camera position such that the player is centered
			Vector2 newCameraPosition = playerCharacter.Position - (_screenSize / 2);
			// Set camera to player position
			Position = newCameraPosition;
		} else 
		{
			GD.Print("Player does not exist");
		}
	}
	
	private void CenterControlOnScreen(Control control)
	{
		_screenSize = GetViewportRect().Size;
		Vector2 screenPosition = Position;
		
		Vector2 centerPosition = (_screenSize - control.Size) / 2;
		
		Vector2 centerPositionCamera = centerPosition + screenPosition;
		
		control.Position = centerPositionCamera;
	}
	
	private PlayerCharacter FindPlayerCharacterInMaps()
	{
		Node root = GetTree().Root;

		foreach (Node child in root.GetChildren()) // Przejdź przez dzieci węzła root
		{
			NodePath childPath = child.GetPath();
			GD.Print("Path of UserInterface:", childPath.ToString());
			// Sprawdź, czy dziecko ma PlayerCharacter
			var playerCharacter = child.GetNodeOrNull<PlayerCharacter>("PlayerCharacter");
			if (playerCharacter != null)
			{
				return playerCharacter; // Zwróć znalezionego PlayerCharacter
			}
		}

		GD.PrintErr("PlayerCharacter not found in any map!");
		return null; // Jeśli nie znaleziono, zwróć null
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateCameraBounds();
		UpdateProgressBars();
		// Get mouse position
		Vector2 mousePosition = GetLocalMousePosition();

		// Get screen size
		Vector2 screenSize = GetViewportRect().Size;
		Vector2 cameraSize = GetViewportRect().Size;
		
		//GD.Print($"offsetX: {offsetX}, offsetY: {offsetY}, Zoom: {_interfaceCamera.Zoom.Y}");
		//GD.Print($"Scaled Camera: {scaledCameraSize}, MouseX: {mousePosition.X}, MouseY: {mousePosition.Y}");
		
		// Calculate camera movement based on mouse position
		Vector2 cameraMovement = new Vector2();
		if(_isPauseMenuVisible == false && _isInventoryMenuVisible == false && _isItemInventoryMenuVisible == false && _isDialogueMenuVisible == false && _isSkillsMenuVisible == false){
			
			//GD.Print($"leftThreshold: {leftThreshold}, rightThreshold: {rightThreshold}, upThreshold: {upThreshold}, downThreshold: {downThreshold}");
			if (mousePosition.X * _scaleRatio < leftThreshold)
			{
				cameraMovement.X -= 1;
			}
			else if (mousePosition.X * _scaleRatio > rightThreshold)
			{
				cameraMovement.X += 1;
			}

			// Vertical movement
			if (mousePosition.Y * _scaleRatio < upThreshold)
			{
				cameraMovement.Y -= 1;
			}
			else if (mousePosition.Y * _scaleRatio > downThreshold)
			{
				cameraMovement.Y += 1;
			}
			
			if (Input.IsActionPressed("arrow_right"))
			{
				cameraMovement[0] += 1;
			}
			else if (Input.IsActionPressed("arrow_left"))
			{
				cameraMovement[0] -= 1;
			} 
			if (Input.IsActionPressed("arrow_down"))
			{
				cameraMovement[1] += 1;
			}
			else if (Input.IsActionPressed("arrow_up"))
			{
				cameraMovement[1] -= 1;
			} 
		}
		
		// Move the camera
		Position += cameraMovement * (float)(_cameraSpeed * delta);

		// Clamp the camera position within the boundaries
		Position = new Vector2(
			Mathf.Clamp(Position.X, _mapMinBounds.X, _mapMaxBounds.X),
			Mathf.Clamp(Position.Y, _mapMinBounds.Y, _mapMaxBounds.Y)
		);

	}
	
	public override void _Input(InputEvent @event)
	{
		
		if (@event.IsActionPressed("esc_key"))
		{
			TogglePauseMenu();
		}
		else if (@event.IsActionPressed("inventory_key"))
		{
			ToggleInventoryMenu();
		} else if (@event.IsActionPressed("skills_key"))
		{
			ToggleSkillsMenu();
		}
		
		if(_isPauseMenuVisible == false && _isInventoryMenuVisible == false)
		{
			if (@event is InputEventMouse eventMouse)
			{
				if (eventMouse.IsAction("mousewheel_up"))
				{
					// Scroll up action
					ScrollContent(true);
				}
				else if (eventMouse.IsAction("mousewheel_down"))
				{
					// Scroll down action
					ScrollContent(false);
				}
			}
		}
		
	}
	
	private void _on_menu_interface_button_pressed()
	{
		TogglePauseMenu();
	}
	
	private void _on_skills_interface_button_pressed()
	{
		ToggleSkillsMenu();
	}
	
	private void _on_inventory_interface_button_pressed()
	{
		ToggleInventoryMenu();
	}
	
	private void ScrollContent(bool direction)
	{
		float zoomChange = 0;
		
		if(direction)
		{
			zoomChange = _scaleChange;
		}
		else{
			zoomChange = -_scaleChange;
		}
		// Obecny zoom kamery
		Vector2 currentZoom = _interfaceCamera.Zoom;

		// Oblicz nowy zoom
		Vector2 newZoom = currentZoom + new Vector2(zoomChange, zoomChange);

		// Zoom
		float minZoom = 1.0f * _scaleRatio;
		float maxZoom = 2.0f * _scaleRatio;
		newZoom.X = Mathf.Clamp(newZoom.X, minZoom, maxZoom);
		newZoom.Y = Mathf.Clamp(newZoom.Y, minZoom, maxZoom);

		// Camera
		_interfaceCamera.Zoom = newZoom;
		Vector2 baseScale = new Vector2(1.0f, 1.0f);
		
		UpdateCameraScale();
	}
	
	private void UpdateCameraScale()
	{
		Vector2 cameraSize = GetViewportRect().Size;
		float threshold = 1.00f;
		Vector2 scaledCameraSize = cameraSize / _interfaceCamera.Zoom.X;
		float offsetX = (cameraSize.X - scaledCameraSize.X) / 2;
		float offsetY = (cameraSize.Y - scaledCameraSize.Y) / 2;
		leftThreshold = offsetX + threshold;
		rightThreshold = (scaledCameraSize.X + offsetX) - threshold;
		upThreshold = offsetY + threshold;
		downThreshold = (scaledCameraSize.Y + offsetY) - threshold;
	}
	
	private void UpdateCameraBounds()
	{
		// Get current screen size and camera zoom
		_screenSize = GetViewportRect().Size;
		Vector2 currentZoom = _interfaceCamera.Zoom;
		// Base map boundaries
		_mapMinBounds = new Vector2(0, 0);
		_mapMaxBounds = new Vector2(_mapSize.X - _screenSize.X, _mapSize.Y - _screenSize.Y); 
		
		// Calculate scaled screen size
		Vector2 scaledScreenSize = _screenSize / currentZoom;
		
		_mapMinBounds = new Vector2(_mapMinBounds.X - (_screenSize.X - scaledScreenSize.X) / 2, _mapMinBounds.Y - (_screenSize.Y - scaledScreenSize.Y) / 2);
		_mapMaxBounds = new Vector2(_mapMaxBounds.X + (_screenSize.X - scaledScreenSize.X) / 2, _mapMaxBounds.Y + (_screenSize.Y - scaledScreenSize.Y) / 2);
	}
	
	public void UpdateProgressBars(){
		_currentCharacterData = GetNode<CharacterData>("/root/GlobalCharacterData");
		_healthProgressBar.MinValue = 0;
		_healthProgressBar.MaxValue = _currentCharacterData.MaxHealthPoints;
		_actionProgressBar.MinValue = 0;
		_actionProgressBar.MaxValue = _currentCharacterData.MaxActionPoints;
		_experienceProgressBar.MinValue = 0;
		_experienceProgressBar.MaxValue = _currentCharacterData.MaxExperiencePoints;
		_healthProgressBar.Value = _currentCharacterData.HealthPoints;
		_actionProgressBar.Value = _currentCharacterData.ActionPoints;
		_experienceProgressBar.Value = _currentCharacterData.ExperiencePoints;
		_levelLabel.Text = $"{_currentCharacterData.CurrentLevel}";
		
	}
	
	public void TogglePauseMenu()
	{
		if (!_isPauseMenuVisible)
		{
			_isPauseMenuVisible = true;
			if (pauseMenu.GetParent() == null)
			{
				GetTree().Root.AddChild(pauseMenu);
			}
			CenterControlOnScreen(pauseMenu);
			pauseMenu.Show();
			
			if (_isInventoryMenuVisible)
			{
				_isInventoryMenuVisible = false;
				if (inventoryMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(inventoryMenu);
				}
			}
			if (_isItemInventoryMenuVisible)
			{
				_isItemInventoryMenuVisible = false;
				if (itemInventoryMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(itemInventoryMenu);
				}
			}
			if (_isSkillsMenuVisible)
			{
				_isSkillsMenuVisible = false;
				if (skillsMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(skillsMenu);
				}
			}
			if (_isDialogueMenuVisible)
			{
				_isDialogueMenuVisible = false;
				if (dialogueMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(dialogueMenu);
				}
			}
		}
		else
		{
			_isPauseMenuVisible = false;
			if (pauseMenu.GetParent() != null)
			{
				GetTree().Root.RemoveChild(pauseMenu);
			}
		}
	}

	public void ToggleInventoryMenu()
	{
		if (!_isInventoryMenuVisible)
		{
			_isInventoryMenuVisible = true;
			if (inventoryMenu.GetParent() == null)
			{
				GetTree().Root.AddChild(inventoryMenu);
			}
			CenterControlOnScreen(inventoryMenu);
			inventoryMenu.Show();
			inventoryMenu.Refresh();
			
			if (_isPauseMenuVisible)
			{
				_isPauseMenuVisible = false;
				if (pauseMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(pauseMenu);
				}
			}
			if (_isItemInventoryMenuVisible)
			{
				_isItemInventoryMenuVisible = false;
				if (itemInventoryMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(itemInventoryMenu);
				}
			}
			if (_isSkillsMenuVisible)
			{
				_isSkillsMenuVisible = false;
				if (skillsMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(skillsMenu);
				}
			}
			if (_isDialogueMenuVisible)
			{
				_isDialogueMenuVisible = false;
				if (dialogueMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(dialogueMenu);
				}
			}
		}
		else
		{
			_isInventoryMenuVisible = false;
			if (inventoryMenu.GetParent() != null)
			{
				GetTree().Root.RemoveChild(inventoryMenu);
			}
		}
	}
	
	public void ToggleItemInventoryMenu(Inventory _currentInventory)
	{
		if (!_isItemInventoryMenuVisible)
		{
			_isItemInventoryMenuVisible = true;
			itemInventoryMenu.CurrentInventory = _currentInventory;
			if (itemInventoryMenu.GetParent() == null)
			{
				GetTree().Root.AddChild(itemInventoryMenu);
			}
			CenterControlOnScreen(itemInventoryMenu);
			itemInventoryMenu.Show();
			
			if (_isPauseMenuVisible)
			{
				_isPauseMenuVisible = false;
				if (pauseMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(pauseMenu);
				}
			}
			if (_isInventoryMenuVisible)
			{
				_isInventoryMenuVisible = false;
				if (inventoryMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(inventoryMenu);
				}
			}
			if (_isSkillsMenuVisible)
			{
				_isSkillsMenuVisible = false;
				if (skillsMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(skillsMenu);
				}
			}
			if (_isDialogueMenuVisible)
			{
				_isDialogueMenuVisible = false;
				if (dialogueMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(dialogueMenu);
				}
			}
		}
		else
		{
			_isItemInventoryMenuVisible = false;
			if (itemInventoryMenu.GetParent() != null)
			{
				GetTree().Root.RemoveChild(itemInventoryMenu);
			}
		}
	}
	
	public void ToggleDialogueMenu(string characterName, string startingKey)
	{
		if (!_isDialogueMenuVisible)
		{
			_isDialogueMenuVisible = true;
			if (dialogueMenu.GetParent() == null)
			{
				GetTree().Root.AddChild(dialogueMenu);
			}
			CenterControlOnScreen(dialogueMenu);
			dialogueMenu.StartDialogue(characterName, startingKey);
			dialogueMenu.Show();
			
			if (_isPauseMenuVisible)
			{
				_isPauseMenuVisible = false;
				if (pauseMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(pauseMenu);
				}
			}
			if (_isInventoryMenuVisible)
			{
				_isInventoryMenuVisible = false;
				if (inventoryMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(inventoryMenu);
				}
			}
			if (_isSkillsMenuVisible)
			{
				_isSkillsMenuVisible = false;
				if (skillsMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(skillsMenu);
				}
			}
			if (_isItemInventoryMenuVisible)
			{
				_isItemInventoryMenuVisible = false;
				if (itemInventoryMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(itemInventoryMenu);
				}
			}
		}
		else
		{
			_isDialogueMenuVisible = false;
			if (dialogueMenu.GetParent() != null)
			{
				GetTree().Root.RemoveChild(dialogueMenu);
			}
		}
	}
	
	public void ToggleSkillsMenu()
	{
		if (!_isSkillsMenuVisible)
		{
			_isSkillsMenuVisible = true;
			if (skillsMenu.GetParent() == null)
			{
				GetTree().Root.AddChild(skillsMenu);
			}
			CenterControlOnScreen(skillsMenu);
			skillsMenu.UpdateLabels();
			skillsMenu.Show();
			
			if (_isPauseMenuVisible)
			{
				_isPauseMenuVisible = false;
				if (pauseMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(pauseMenu);
				}
			}
			if (_isInventoryMenuVisible)
			{
				_isInventoryMenuVisible = false;
				if (inventoryMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(inventoryMenu);
				}
			}
			if (_isDialogueMenuVisible)
			{
				_isDialogueMenuVisible = false;
				if (dialogueMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(dialogueMenu);
				}
			}
			if (_isItemInventoryMenuVisible)
			{
				_isItemInventoryMenuVisible = false;
				if (itemInventoryMenu.GetParent() != null)
				{
					GetTree().Root.RemoveChild(itemInventoryMenu);
				}
			}
		}
		else
		{
			_isSkillsMenuVisible = false;
			if (skillsMenu.GetParent() != null)
			{
				GetTree().Root.RemoveChild(skillsMenu);
			}
		}
	}
	
	public bool IsPauseMenuVisible
	{
		get { return _isPauseMenuVisible; }
		set { _isPauseMenuVisible = value; }
	}
	
	public bool IsInventoryMenuVisible
	{
		get { return _isInventoryMenuVisible; }
		set { _isInventoryMenuVisible = value; }
	}
	
	public bool IsItemInventoryMenuVisible
	{
		get { return _isItemInventoryMenuVisible; }
		set { _isItemInventoryMenuVisible = value; }
	}
	
	public bool IsDialogueMenuVisible
	{
		get { return _isDialogueMenuVisible; }
		set { _isDialogueMenuVisible = value; }
	}
	
	public bool IsSkillsMenuVisible
	{
		get { return _isSkillsMenuVisible; }
		set { _isSkillsMenuVisible = value; }
	}
}
