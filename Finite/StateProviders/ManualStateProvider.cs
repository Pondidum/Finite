using System;
using System.Collections.Generic;
using System.Linq;
using Finite.Configurations;
using Finite.Infrastructure;

namespace Finite.StateProviders
{
	public class ManualStateProvider<T> : IStateProvider<T>
	{
		private readonly IDictionary<Type, State<T>> _states;
		private readonly List<Type> _types;

		public ManualStateProvider(IEnumerable<Type> states)
		{
			_types = states.ToList();
			_states = new Dictionary<Type, State<T>>();
		}

		public void InitialiseStates(IInstanceCreator instanceCreator)
		{
			_states.AddRange(_types
				.Where(t => typeof(State<T>).IsAssignableFrom(t))
				.ToDictionary(t => t, instanceCreator.Create<T>));

			_states.ForEach(i => i.Value.Configure(this));
		}

		public State<T> GetStateFor(Type stateType)
		{
			if (_states.ContainsKey(stateType) == false)
			{
				throw new UnknownStateException(stateType);
			}

			return _states[stateType];
		}
	}
}
