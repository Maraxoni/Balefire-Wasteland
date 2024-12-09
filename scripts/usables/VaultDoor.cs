using Godot;
using System;

public partial class VaultDoor : StaticBody2D
{
	private bool _isHovered = false;
	private AnimationPlayer _animationPlayer;
	private bool _isOpen = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _Draw(){
		Color yellow = Colors.Yellow;
		Color transparent = new Color("0000008f");
		
		if(_isHovered){
			DrawCircle(new Vector2(0, 0.0f), 25.0f, yellow);
			DrawCircle(new Vector2(0, 0.0f), 20.0f, transparent);
		}
	}
	
	// Handle input events
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.ButtonIndex == MouseButton.Left && eventMouseButton.Pressed && _isHovered)
			{
				ToggleDoor();
			}
		}
	}
	
	private void _on_area_2d_mouse_entered(){
		_isHovered = true;
		QueueRedraw();
	}

	private void _on_area_2d_mouse_exited(){
		_isHovered = false;
		QueueRedraw();
	}
	
	private void ToggleDoor()
	{
		if (_isOpen)
		{
			_animationPlayer.Play("close");
		}
		else
		{
			_animationPlayer.Play("open");
		}
		_isOpen = !_isOpen;
	}
}
