using Godot;
using System;
using GameProject;

public partial class UserInterface : Control
{
	private bool _isPauseMenuVisible = false;
	private bool _isInventoryMenuVisible = false;
	private bool _isItemInventoryMenuVisible = false;
	private bool _isDialogueMenuVisible = false;
	
	private Control pauseMenu;
	private InventoryMenu inventoryMenu;
	private ItemInventoryMenu itemInventoryMenu;
	private DialogueMenu dialogueMenu;
	private Godot.Camera2D interfaceCamera;
	private float _cameraSpeed = 400.0f;
	private float _scrollSpeed = 20.0f;
	private float _scaleChange = 0.05f;
	//Bars
	private ProgressBar _healthProgressBar;
	private ProgressBar _actionProgressBar;
	private ProgressBar _experienceProgressBar;
	// Map boundaries
	private Vector2 _screenSize;
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
		
		_healthProgressBar = GetNode<ProgressBar>("CanvasLayer/MarginContainer/VBoxContainer/HBoxContainer/HealthProgressBar");
		_actionProgressBar = GetNode<ProgressBar>("CanvasLayer/MarginContainer/VBoxContainer/HBoxContainer/ActionProgressBar");
		_experienceProgressBar = GetNode<ProgressBar>("CanvasLayer/MarginContainer/VBoxContainer/HBoxContainer3/ExperienceProgressBar");
		
		interfaceCamera = GetNode<Godot.Camera2D>("InterfaceCamera");
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

		// Define movement thresholds
		float threshold = 1.0f;

		// Calculate camera movement based on mouse position
		Vector2 cameraMovement = new Vector2();
		if(_isPauseMenuVisible == false && _isInventoryMenuVisible == false && _isItemInventoryMenuVisible == false && _isDialogueMenuVisible == false){
			if (mousePosition[0] < threshold)
			{
				cameraMovement[0] -= 1;
			}
			else if (mousePosition[0] > screenSize[0] - threshold)
			{
				cameraMovement[0] += 1;
			}
			if (mousePosition[1] < threshold)
			{
				cameraMovement[1] -= 1;
			}
			else if (mousePosition[1] > screenSize[1] - threshold)
			{
				cameraMovement[1] += 1;
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
		else if (@event.IsActionPressed("i_key"))
		{
			ToggleInventoryMenu();
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
		Vector2 currentZoom = interfaceCamera.Zoom;

		// Oblicz nowy zoom
		Vector2 newZoom = currentZoom + new Vector2(zoomChange, zoomChange);

		// Zoom
		float minZoom = 1.0f;
		float maxZoom = 2.0f;
		newZoom.X = Mathf.Clamp(newZoom.X, minZoom, maxZoom);
		newZoom.Y = Mathf.Clamp(newZoom.Y, minZoom, maxZoom);

		// Camera
		interfaceCamera.Zoom = newZoom;
		Vector2 baseScale = new Vector2(1.0f, 1.0f);
	}
	
	private void UpdateCameraBounds()
	{
		// Get current screen size and camera zoom
		_screenSize = GetViewportRect().Size;
		Vector2 currentZoom = interfaceCamera.Zoom;
		// Base map boundaries
		_mapMinBounds = new Vector2(0, 0);
		_mapMaxBounds = new Vector2(_mapSize.X - _screenSize.X, _mapSize.Y - _screenSize.Y); 
		
		// Calculate scaled screen size
		Vector2 scaledScreenSize = _screenSize / currentZoom;
		
		_mapMinBounds = new Vector2(_mapMinBounds.X - (_screenSize.X - scaledScreenSize.X) / 2, _mapMinBounds.Y - (_screenSize.Y - scaledScreenSize.Y) / 2);
		_mapMaxBounds = new Vector2(_mapMaxBounds.X + (_screenSize.X - scaledScreenSize.X) / 2, _mapMaxBounds.Y + (_screenSize.Y - scaledScreenSize.Y) / 2);
	}
	
	private void _on_menu_interface_button_pressed()
	{
		TogglePauseMenu();
		if (_isInventoryMenuVisible)
		{
			_isInventoryMenuVisible = false;
			if (inventoryMenu.GetParent() != null)
			{
				GetTree().Root.RemoveChild(inventoryMenu);
			}
		}
	}
	
	private void _on_inventory_interface_button_pressed()
	{
		ToggleInventoryMenu();
		if (_isPauseMenuVisible)
		{
			_isPauseMenuVisible = false;
			if (pauseMenu.GetParent() != null)
			{
				GetTree().Root.RemoveChild(pauseMenu);
			}
		}
	}
	
	public void UpdateProgressBars(){
		CharacterData _characterData = GetNode<CharacterData>("/root/GlobalCharacterData");
		_healthProgressBar.MinValue = 0;
		_healthProgressBar.MaxValue = 100;
		_actionProgressBar.MinValue = 0;
		_actionProgressBar.MaxValue = 100;
		_experienceProgressBar.MinValue = 0;
		_experienceProgressBar.MaxValue = 100;
		_healthProgressBar.Value = _characterData.HealthPoints;
		_actionProgressBar.Value = _characterData.ActionPoints;
		_experienceProgressBar.Value = _characterData.ExperiencePoints;
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
			pauseMenu.Position = new Vector2(Position[0], Position[1]);
			pauseMenu.Show();
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
			inventoryMenu.Position = new Vector2(Position[0], Position[1]);
			
			inventoryMenu.Show();
			inventoryMenu.Refresh();
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
			itemInventoryMenu.Position = new Vector2(Position[0], Position[1]);
			itemInventoryMenu.Show();
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
			dialogueMenu.Position = new Vector2(Position[0], Position[1]);
			dialogueMenu.StartDialogue(characterName, startingKey);
			dialogueMenu.Show();
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
}
