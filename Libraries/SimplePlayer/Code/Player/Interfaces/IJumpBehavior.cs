using System;

namespace Movement.Interfaces
{
	public interface IJumpBehavior
	{
		public event Action JumpAction;

		/// <summary>
		/// Jumping behavior.
		/// </summary>
		/// <param name="controller">A controller to use the jump on.</param>
		/// <param name="jumpForce">Force of the jump.</param>
		/// <returns>True if jumped, False if didn't.</returns>
		public bool Jump( CharacterController controller, float jumpForce );
	}
}
