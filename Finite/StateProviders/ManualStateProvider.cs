using System;
using System.Collections.Generic;
using System.Linq;
using Finite.Configurations;
using Finite.Infrastructure;

namespace Finite.StateProviders
{
	public class ManualStateProvider<T> : IStateProvider<T>
	{
		private readonly IInstanceCreator _instanceCreator;
		private readonly IDictionary<Type, State<T>> _states;

		public ManualStateProvider(IEnumerable<Type> states)
			: this(null, states)
		{
		}

		public ManualStateProvider(IInstanceCreator instanceCreator, IEnumerable<Type> states)
		{
			_instanceCreator = instanceCreator ?? new DefaultInstanceCreator();

			_states = states
				.Where(t => typeof(State<T>).IsAssignableFrom(t))
				.ToDictionary(t => t, t => _instanceCreator.Create<T>(t));

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
