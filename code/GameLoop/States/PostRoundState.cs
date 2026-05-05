using HideAndSeek.GameLoop.StateMachines;

namespace HideAndSeek.GameLoop.States
{
	public class PostRoundState( RoundStateMachine roundStateMachine ) : IRoundState
	{
		private readonly RoundStateMachine _roundStateMachine = roundStateMachine;

		public void Enter()
		{
		}

		public void Exit()
		{
		}

		public void Update()
		{
			if ( _roundStateMachine.Round.HasTheGameEnded )
				return;

			if ( _roundStateMachine.Round.HasPostRoundEnded )
			{
				_roundStateMachine.ChangeState( this );
			}
		}
	}
}
