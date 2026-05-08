using System;

namespace HideAndSeek.GameLoop.Rules;

public class Team
{
	/// <summary>
	/// Team's name.
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// Team's color.
	/// </summary>
	public string Color { get; set; }
	/// <summary>
	/// List of player connections in this team.
	/// </summary>
	private List<Guid> _players { get; set; }

	public IReadOnlyList<Guid> Players => _players.AsReadOnly();

	public Team( string name, string color )
	{
		Name = name;
		Color = color;
		_players = [];
	}

	public Team( string name, string color, IEnumerable<Guid> players)
	{
		Name = name;
		Color = color;
		_players = [.. players];
	}

	public Team()
	{
		Name = "Unnamed";
		Color = "Blue";
		_players = [];
	}

	public void AddPlayer( Guid playerId )
	{
		if ( !Players.Contains( playerId ) )
		{
			_players.Add( playerId );
		}
	}

	public void RemovePlayer( Guid playerId )
	{
		if ( Players.Contains( playerId ) )
		{
			_players.Remove( playerId );
		}
	}

	public void FlushPlayers()
	{
		_players.Clear();
	}

	public bool IsThePlayerInTeam( Guid playerId )
	{
		return Players.Contains( playerId );
	}
}
