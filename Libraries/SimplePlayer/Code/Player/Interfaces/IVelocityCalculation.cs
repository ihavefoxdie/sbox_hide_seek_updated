namespace Movement.Interfaces
{
	public interface IVelocityCalculation
	{
		public Vector3 CalculateDesiredVelocity( Rotation rotation );
	}
}
