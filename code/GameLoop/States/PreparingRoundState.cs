using HideAndSeek.GameLoop.StateMachines;

namespace HideAndSeek.GameLoop.States
{
	public class PreparingRoundState : IRoundState
	{
		private readonly RoundStateMachine _roundStateMachine;

		public PreparingRoundState(RoundStateMachine roundStateMachine)
		{
			_roundStateMachine = roundStateMachine;
		}

		public void Enter()
		{
		}

		public void Exit()
		{
		}

		public void Update()
		{
			if ( _roundStateMachine.Round.ShouldSeekersBeEnabled )
			{
				_roundStateMachine.ChangeState( this );
			}
		}
	}
}
