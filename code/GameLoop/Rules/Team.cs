using Sandbox.GameLoop.Rules;
using System;

namespace HideAndSeek.GameLoop.Rules;

public class Team : Component
{
	[Property]
	[Sync( SyncFlags.FromHost, Flags = SyncFlags.Query)]
	public TeamData TeamData { get; set; }

	[Sync]
	public NetList<Guid> Players { get; set; } = [];

	public void Init( string name, string color )
	{
		TeamData = new TeamData
		{
			Name = name,
			Color = color
		};
	}

	public void Init( string name, string color, IEnumerable<Guid> players )
	{
		TeamData = new TeamData
		{
			Name = name,
			Color = color
		};
		this.Players = [.. players];
	}

	public Team()
	{
		TeamData = new TeamData
		{
			Name = "Unnamed",
			Color = "Blue"
		};
	}

	public void AddPlayer( Guid playerId )
	{
		if ( !Players.Contains( playerId ) )
		{
			Players.Add( playerId );
		}
	}

	public void RemovePlayer( Guid playerId )
	{
		Players.Remove( playerId );
	}

	public void FlushPlayers()
	{
		Players.Clear();
	}

	public bool IsThePlayerInTeam( Guid playerId )
	{
		return Players.Contains( playerId );
	}
}
