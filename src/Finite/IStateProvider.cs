using System.Collections.Generic;

namespace Finite
{
	public interface IStateProvider<TSwitches>
	{
		IEnumerable<State<TSwitches>> Execute();
	}
}
