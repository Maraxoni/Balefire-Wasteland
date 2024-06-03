using Godot;
using System;

public partial class UserInterface : Control
{
	private bool _IsPauseMenuVisible = false;
	private bool _isInventoryVisible = false;
	private Control pauseMenu;
	private Control inventoryMenu;
	private Node2D interfaceNode;
	private float cameraSpeed = 100.0f; // Adjust camera speed as needed
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Preload the PauseMenu scene once
		var pauseScene = ResourceLoader.Load<PackedScene>("res://scenes/menus/PauseMenu.tscn");
		pauseMenu = pauseScene.Instantiate<Control>();
		var inventoryScene = ResourceLoader.Load<PackedScene>("res://scenes/menus/InventoryMenu.tscn");
		inventoryMenu = inventoryScene.Instantiate<Control>();
		
		// Find the node the camera is attached to
		interfaceNode = GetNode<Node2D>("/root");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Get mouse position
		Vector2 mousePosition = GetGlobalMousePosition();

		// Get screen size
		Vector2 screenSize = GetViewportRect().Size;

		// Define movement thresholds
		float threshold = 20.0f;

		// Calculate camera movement based on mouse position
		Vector2 cameraMovement = new Vector2();
		if (mousePosition[0] < threshold)
			cameraMovement[0] = -1;
		else if (mousePosition[0] > screenSize[0] - threshold)
			cameraMovement[0] = 1;
		if (mousePosition[1] < threshold)
			cameraMovement[1] = -1;
		else if (mousePosition[1] > screenSize[1] - threshold)
			cameraMovement[1] = 1;

		// Move the camera
		interfaceNode.Position += new Vector2(cameraMovement[0] * (float)(cameraSpeed * delta), cameraMovement[1] * (float)(cameraSpeed * delta));
	}
	
	public override void _Input(InputEvent @event)
	{
		
		if (@event.IsActionPressed("esc_key"))
		{
			TogglePauseMenu();
		}
	}
	
	private void _on_menu_interface_button_pressed()
	{
		TogglePauseMenu();
	}
	
	private void _on_inventory_interface_button_pressed()
	{
		ToggleInventoryMenu();
	}

	
	private void TogglePauseMenu()
	{
		if (!_IsPauseMenuVisible)
		{
			_IsPauseMenuVisible = true;
			if (pauseMenu.GetParent() == null)
			{
				GetTree().Root.AddChild(pauseMenu);
			}
			pauseMenu.Show();
		}
		else
		{
			_IsPauseMenuVisible = false;
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
			inventoryMenu.Show();
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
		get { return _IsPauseMenuVisible; }
		set { _IsPauseMenuVisible = value; }
	}
	
	public bool IsInventoryVisible
	{
		get { return _isInventoryVisible; }
		set { _isInventoryVisible = value; }
	}
}





