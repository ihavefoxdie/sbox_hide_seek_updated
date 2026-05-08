using HideAndSeek.GameLoop.StateMachines;
using HideAndSeek.GameLoop.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace HideAndSeek.GameLoop.States
{
	public abstract class RoundState( RoundStateMachine roundStateMachine ) : Component, IState
	{
		protected RoundStateMachine RoundStateMachine { get; private set; } = roundStateMachine;

		public abstract void Enter();

		public abstract void Exit();

		public abstract void Update();
	}
}
