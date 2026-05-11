using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek.Utils.Interfaces
{
	public interface IBeforeSceneChangeListener
	{
		public Task BeforeChange();
	}
}
