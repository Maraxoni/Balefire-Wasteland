using Godot;
using System;

public partial class PlayerCharacter
{
	public const float Speed = 300.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.

	public void _movement(double delta)
	{
		//Vector2 velocity = Velocity;
//
		//// Handle Jump.
		//
		//if (direction != Vector2.Zero)
		//{
			//velocity.X = direction.X * Speed;
		//}
		//else
		//{
			//velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		//}
//
		//Velocity = velocity;
	}
}
