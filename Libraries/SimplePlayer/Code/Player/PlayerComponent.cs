using Movement.Interfaces;

namespace Player
{
	[Icon( "directions_walk" )]
	public sealed class PlayerComponent : Component
	{
		[Property, Category( "Components" )] public IVelocityCalculation VelocityCalculation { get; private set; }
		[Property, Category( "Components" )] public IJumpBehavior JumpMechanic { get; private set; }
		[Property, Category( "Components" )] public IMovementBehavior MovementBehavior { get; private set; }
		[Property, Category( "Components" )] public CharacterController PlayerController { get; private set; }

		[Property, Category( "Measurements" )] public Vector3 DesiredVelocity { get; private set; }
		[Property, Category( "Measurements" )] public float AirSpeed { get; set; } = 50f;
		[Property, Category( "Measurements" )] public float AirFriction { get; } = 0.25f;
		[Property, Category( "Measurements" )] public float BaseSpeed { get; set; } = 200;
		[Property, Category( "Measurements" )] public float WalkingDelta { get; set; } = -100f;
		[Property, Category( "Measurements" )] public float SpritDelta { get; set; } = 100f;
		[Property, Category( "Measurements" )] public float DuckDelta { get; set; } = -125f;
		[Property, Category( "Measurements" )] public int DuckHeightDelta { get { return -(int)(72 / 2f); } }
		[Property, Category( "Measurements" )] public float InitHeight { get { return 72; } }
		[Property, Category( "Measurements" )] public float JumpForce { get; set; } = 300f;
		[Property, Category( "Measurements" )] public float Friction { get; set; } = 4f;

		protected override void OnStart()
		{
			VelocityCalculation = Components.Get<IVelocityCalculation>();
			JumpMechanic = Components.Get<IJumpBehavior>();
			MovementBehavior = Components.Get<IMovementBehavior>();
		}

		protected override void OnUpdate()
		{

		}

		protected override void OnFixedUpdate()
		{
			if ( MovementBehavior != null )
			{
				MovementBehavior.Move(PlayerController, DesiredVelocity, Friction, AirSpeed, AirFriction);
			}
			else
			{
				Log.Warning( "Movement behavior is not assigned! (MovementBehavior is null)" );
			}

			if ( VelocityCalculation != null )
			{
				DesiredVelocity = VelocityCalculation.CalculateDesiredVelocity(GameObject.Parent.Transform.Rotation) * BaseSpeed;
			}
			else
			{
				Log.Warning( "Velocity calculation is not assigned! (VelocityCalculation is null)" );
			}

			if ( Input.Down( "Jump" ) )
			{
				if(JumpMechanic == null) { Log.Warning( "Jumping behavior is not assigned! (JumpMechanic is null)" ); }
				JumpMechanic?.Jump( PlayerController, JumpForce );
			}
		}
	}
}
