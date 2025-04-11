using Movement.Interfaces;
using System;

namespace Movement
{
	public class SimpleMove : Component, IMovementBehavior
	{
		public event Action JumpAction;

		public void Move( CharacterController controller, Vector3 desiredVelocity, float friction, float airSpeed, float airFriction )
		{
			Vector3 gravity = Scene.PhysicsWorld.Gravity;

			if ( controller.IsOnGround )
			{
				/*if ( _jumped )
				{
					_jumped = false;
					LandedAction?.Invoke();
				}*/
				controller.Velocity = controller.Velocity.WithZ( 0 );
				controller.Accelerate( desiredVelocity );
				controller.ApplyFriction( friction );
			}
			else
			{
				controller.Velocity += gravity * Time.Delta * 0.5f;
				controller.Accelerate( desiredVelocity.ClampLength( airSpeed ) );
				controller.ApplyFriction( airFriction );
			}

			controller.Move();
			if ( !controller.IsOnGround )
			{
				controller.Velocity += gravity * Time.Delta * 0.5f;
			}
			else
			{
				controller.Velocity = controller.Velocity.WithZ( 0 );
			}
		}
	}
}
