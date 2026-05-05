using HideAndSeek.GameLoop.StateMachines;
using System;
using System.Collections.Generic;
using System.Text;

namespace HideAndSeek.GameLoop.States
{
	public class ActiveRoundState : IRoundState
	{
		private RoundStateMachine _roundStateMachine;

		public ActiveRoundState( RoundStateMachine roundStateMachine )
		{
			_roundStateMachine = roundStateMachine;
		}

		public void Enter()
		{
			throw new NotImplementedException();
		}

		public void Exit()
		{
			throw new NotImplementedException();
		}

		public void Update()
		{
			throw new NotImplementedException();
		}
	}
}
