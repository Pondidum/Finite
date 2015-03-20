using System;
using System.Collections.Generic;
using System.Linq;
using Finite.Configurations;
using Finite.Infrastructure;

namespace Finite
{
	public class States<TSwitches>
	{
		private readonly List<Type> _types;
		private readonly IDictionary<Type, State<TSwitches>> _states;

		public States()
		{
			_types = new List<Type>();
			_states = new Dictionary<Type, State<TSwitches>>();
		}

		public IEnumerable<Type> KnownTypes { get { return _types; } }
 
		public States<TSwitches> Are(params Type[] states)
		{
			_types.AddRange(states);
			return this;
		}
		
		public void InitialiseStates(IInstanceCreator instanceCreator)
		{
			_states.AddRange(_types
				.Where(t => typeof(State<TSwitches>).IsAssignableFrom(t))
				.ToDictionary(t => t, instanceCreator.Create<TSwitches>));

			_states.ForEach(i => i.Value.Configure(this));
		}

		public State<TSwitches> GetStateFor<TState>()
		{
			var stateType = typeof(TState);

			if (_states.ContainsKey(stateType) == false)
			{
				throw new UnknownStateException(stateType);
			}

			return _states[stateType];
		}
 
	}
}