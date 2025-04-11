using System;

namespace Movement.Interfaces
{
	public interface IMovementBehavior
	{
		public event Action JumpAction;

		public void Move( CharacterController controller, Vector3 desiredVelocity, float friction, float airSpeed, float airFriction );
	}
}
