using Movement.Interfaces;
using System;

namespace Movement
{
	[Icon ( "trending_up" )]
	public sealed class SimpleJump : Component, IJumpBehavior
	{
		public event Action JumpAction;

		public bool Jump( CharacterController controller, float jumpForce )
		{
			if ( !controller.IsOnGround )
			{
				return false;
			}
			if ( Input.Down( "Jump" ) )
			{
				JumpAction?.Invoke();
				controller.Punch( Vector3.Up * jumpForce );
				return true;
			}
			return false;
		}
	}
}
