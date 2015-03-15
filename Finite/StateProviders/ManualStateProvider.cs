using System;
using System.Collections.Generic;
using System.Linq;
using Finite.Infrastructure;
using Finite.InstanceCreators;

namespace Finite.StateProviders
{
	public class ManualStateProvider<T> : IStateProvider<T>
	{
		private readonly IInstanceCreator _instanceCreator;
		private readonly IDictionary<Type, State<T>> _states;

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

			instances.ForEach(_states.Add);
			instances.ForEach(i => i.Value.Configure(this));

			return this;
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
