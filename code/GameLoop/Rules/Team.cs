using System;

namespace HideAndSeek.GameLoop.Rules;

public struct Team
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
	public List<Guid> TeamPlayers { get; set; }

	public Team( string name, string color )
	{
		Name = name;
		Color = color;
		TeamPlayers = [];
	}

	public Team()
	{
		Name = "Unnamed";
		Color = "Blue";
		TeamPlayers = [];
	}

}
