using System;

namespace HideAndSeek.GameLoop.Rules;

public struct Round
{
	#region Properties
	/// <summary>
	/// Time since the start of the round.
	/// </summary>
	public TimeSince TimeSinceStart { get; private set; }
	/// <summary>
	/// Round length, in seconds.
	/// </summary>
	public int RoundLength { get; private set; }
	/// <summary>
	/// Is round in progress?
	/// </summary>
	public bool IsStarted { get; private set; } = false;
	/// <summary>
	/// Should the round end?
	/// </summary>
	public readonly bool ShouldRoundEnd
	{
		get
		{
			if ( TimeSinceStart.Relative >= RoundLength )
			{
				return true;
			}
			return false;
		}
	}
	#endregion



	#region Actions
	/// <summary>
	/// Gets invoked when the end event occurs.
	/// </summary>
	public Action OnEnd { get; set; }
	/// <summary>
	/// Gets invoked when the start event occurs.
	/// </summary>
	/// <remarks>Assign a delegate to perform custom initialization or startup logic. If not set, no action is
	/// performed when the start event occurs.</remarks>
	public Action OnStart { get; set; }
	#endregion



	#region Variables
	#endregion



	public Round()
	{
		RoundLength = 300;
	}

	public Round( int roundLength )
	{
		RoundLength = roundLength;
	}



	#region Methods
	/// <summary>
	/// Start the round.
	/// </summary>
	public void StartTheRound()
	{
		if ( IsStarted ) return;

		TimeSinceStart = 0;
		OnStart?.Invoke();
		IsStarted = true;
	}

	/// <summary>
	/// End the round.
	/// </summary>
	public void EndTheRound()
	{
		OnEnd?.Invoke();
		IsStarted = false;
	}
	#endregion
}
