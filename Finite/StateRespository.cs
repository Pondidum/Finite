using System;
using System.Collections.Generic;
using System.Linq;
using Finite.Infrastructure;

namespace Finite
{
	public class StateRespository<TSwitches>
	{
		private readonly IDictionary<Type, State<TSwitches>> _states;

		public StateRespository(IEnumerable<State<TSwitches>> states)
		{
			_states = states.ToDictionary(s => s.GetType(), s => s);
		}

		public void InitialiseStates()
		{
			_states.Values.ForEach(state => state.Configure(this));
		}

		public State<TSwitches> GetStateFor<TState>()
		{
			return GetStateFor(typeof(TState));
		}

		public State<TSwitches> GetStateFor(Type stateType)
		{
			if (_states.ContainsKey(stateType) == false)
			{
				throw new UnknownStateException(stateType);
			}

			return _states[stateType];
		}

		public IEnumerable<State<TSwitches>> AsEnumerable()
		{
			return _states.Values.ToList();
		}

	}
}
