

using System;
using System.Threading.Tasks;

namespace HideAndSeek;

public sealed class MyComponent
{
	public Func<Task> BeforeChange { get; set; }

}
