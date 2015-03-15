using System;
using System.Collections.Generic;
using System.Linq;

namespace Finite
{
	public class ManualStateProvider<T> : IStateProvider<T>
	{
		private readonly IInstanceCreator _instanceCreator;
		private readonly Dictionary<Type, State<T>> _states;

		public ManualStateProvider(IInstanceCreator instanceCreator)
		{
			_instanceCreator = instanceCreator;
			_states = new Dictionary<Type, State<T>>();
		}

		public ManualStateProvider(IInstanceCreator instanceCreator, IEnumerable<Type> states)
			: this(instanceCreator)
		{
			AddRange(states);
		}

		public ManualStateProvider<T> AddRange(IEnumerable<Type> types)
		{
			var instances = types
				.Where(t => typeof(State<T>).IsAssignableFrom(t))
				.ToDictionary(
					t => t,
					t => _instanceCreator.Create<T>(t));

			foreach (var pair in instances)
			{
				_states.Add(pair.Key, pair.Value);
			}

			return this;
		}

		public State<T> GetStateFor(Type stateType)
		{
			return _states[stateType];
		}
	}
}
