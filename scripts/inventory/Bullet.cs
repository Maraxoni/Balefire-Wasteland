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

	private void _on_Bullet_body_entered(Node body)
	{
		if (body is EnemyCharacter)
		{
			(body as EnemyCharacter).TakeDamage(Damage);
			QueueFree(); // Bullet disappears after hitting the enemy
		}
		else if (body is StaticBody2D || body is TileMap) // Bullet hits a wall
		{
			QueueFree(); // Bullet disappears when hitting a wall or obstacle
		}
	}
}
