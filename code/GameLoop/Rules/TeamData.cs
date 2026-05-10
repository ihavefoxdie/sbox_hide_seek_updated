using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.GameLoop.Rules;

public struct TeamData
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
}
