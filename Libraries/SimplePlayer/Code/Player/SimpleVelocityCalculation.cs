using Movement.Interfaces;

namespace Movement
{
	[Icon( "fast_forward" )]
	public sealed class SimpleVelocityCalculation : Component, IVelocityCalculation
	{
		public Vector3 CalculateDesiredVelocity( Rotation rotation )
		{
			Vector3 desiredVelocity = Input.AnalogMove;

			desiredVelocity = desiredVelocity.WithZ( 0 );

			if ( !desiredVelocity.IsNearZeroLength )
				desiredVelocity = desiredVelocity.Normal;

			return desiredVelocity;
		}
	}
}
