using System.Collections.Generic;
using System.Linq;

namespace Finite.StateProviders
{
	public class ManualStateProvider<TSwitches> : IStateProvider<TSwitches>
	{
		private readonly List<State<TSwitches>> _states;

		public ManualStateProvider(IEnumerable<State<TSwitches>> states)
		{
			_states = states.ToList();
		}

		public IEnumerable<State<TSwitches>> Execute()
		{
			return _states;
		}
	}
}
