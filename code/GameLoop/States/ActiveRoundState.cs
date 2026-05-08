using HideAndSeek.GameLoop.StateMachines;

namespace HideAndSeek.GameLoop.States
{
	public class ActiveRoundState( RoundStateMachine roundStateMachine ) : RoundState( roundStateMachine )
	{
		public override void Enter()
		{
		}

		public override void Exit()
		{

		}

		public override void Update()
		{
			if ( RoundStateMachine.GameLoop.Round.ShouldRoundEnd )
			{
				RoundStateMachine.ChangeState( this );
			}
		}
	}
}
