using Godot;
using System;

public partial class Bullet : Node2D
{
	public float Speed = 600f; // Speed of the bullet
	public int Damage = 10; // Damage dealt by the bullet

	private Vector2 _direction;

	public void Initialize(Vector2 direction)
	{
		_direction = direction.Normalized(); // Normalize the direction so that it is consistent
	}

	public override void _Process(double delta)
	{
		Position += _direction * Speed * (float)delta;


		// if (timer > maxLifetime) {
		//     QueueFree();
		// }
	}

	private void _on_area_2d_body_entered(Node body)
	{
		GD.Print($"Bullet hit: {body.Name}");
		if (body is EnemyCharacter enemy)
		{
			enemy.TakeDamage(Damage);
			QueueFree();
		}
		else if (body is StaticBody2D || body is TileMap)
		{
			QueueFree();
		}
	}
}
