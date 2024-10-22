using Godot;
using System;

public partial class UserInterface : Control
{
	private bool _isPauseMenuVisible = false;
	private bool _isInventoryVisible = false;
	private Control pauseMenu;
	private InventoryMenu inventoryMenu;
	private Godot.Camera2D interfaceCamera;
	private float _cameraSpeed = 400.0f;
	private float _scrollSpeed = 20.0f;
	private float _scaleChange = 0.05f;
	
	public override void _Ready()
	{
		// Preload the PauseMenu scene once
		var pauseScene = ResourceLoader.Load<PackedScene>("res://scenes/menus/PauseMenu.tscn");
		pauseMenu = pauseScene.Instantiate<Control>();
		var inventoryScene = ResourceLoader.Load<PackedScene>("res://scenes/menus/InventoryMenu.tscn");
		inventoryMenu = inventoryScene.Instantiate<InventoryMenu>();
		
		interfaceCamera = GetNode<Godot.Camera2D>("InterfaceCamera");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Get mouse position
		Vector2 mousePosition = GetLocalMousePosition();

		// Get screen size
		Vector2 screenSize = GetViewportRect().Size;

		// Define movement thresholds
		float threshold = 1.0f;

		// Calculate camera movement based on mouse position
		Vector2 cameraMovement = new Vector2();
		if(_isPauseMenuVisible == false && _isInventoryVisible == false){
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
		
		if(_isPauseMenuVisible == false && _isInventoryVisible == false)
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
		Vector2 TempSize;
		
		if(direction)
		{
			if((Scale[0] > 0.5f) && (Scale[1] > 0.5f))
			{
				Scale = new Vector2(Scale[0] - _scaleChange, Scale[1] - _scaleChange);
				interfaceCamera.Zoom = new Vector2(1 / Scale[0], 1 / Scale[1]);
				Position = new Vector2(
					Position[0], 
					Position[1]
				);
			}
		}
		else{
			if((Scale[0] < 4f) && (Scale[1] < 4f))
			{
				Scale = new Vector2(Scale[0] + _scaleChange, Scale[1] + _scaleChange);
				interfaceCamera.Zoom = new Vector2(1 / Scale[0], 1 / Scale[1]);
				Position = new Vector2( 
					Position[0],
					Position[1]
				);
			}
		}
	}
	
	private void _on_menu_interface_button_pressed()
	{
		TogglePauseMenu();
		if (_isPauseMenuVisible)
		{
			_isPauseMenuVisible = false;
			if (pauseMenu.GetParent() != null)
			{
				GetTree().Root.RemoveChild(pauseMenu);
			}
		}
	}
	
	private void _on_inventory_interface_button_pressed()
	{
		ToggleInventoryMenu();
		if (_isInventoryVisible)
		{
			_isInventoryVisible = false;
			if (inventoryMenu.GetParent() != null)
			{
				GetTree().Root.RemoveChild(inventoryMenu);
			}
		}
	}

	private void TogglePauseMenu()
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
	
	private void ToggleInventoryMenu()
	{
		if (!_isInventoryVisible)
		{
			_isInventoryVisible = true;
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
			_isInventoryVisible = false;
			if (inventoryMenu.GetParent() != null)
			{
				GetTree().Root.RemoveChild(inventoryMenu);
			}
		}
	}
	
	public bool IsPauseMenuVisible
	{
		get { return _isPauseMenuVisible; }
		set { _isPauseMenuVisible = value; }
	}
	
	public bool IsInventoryVisible
	{
		get { return _isInventoryVisible; }
		set { _isInventoryVisible = value; }
	}
}
