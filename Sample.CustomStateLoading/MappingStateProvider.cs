using System;
using System.Collections.Generic;
using System.Linq;
using Finite;

namespace Sample.CustomStateLoading
{
	public class MappingStateProvider : IStateProvider<CreditRequest>
	{
		private readonly Lazy<List<CustomState<CreditRequest>>> _states;

		public MappingStateProvider(IStateProvider<CreditRequest> other)
		{
			_states = new Lazy<List<CustomState<CreditRequest>>>(() => other
				.Execute()
				.Cast<CustomState<CreditRequest>>()
				.ToList());
		}

		public State<CreditRequest> StateFrom(Progress type)
		{
			return _states.Value.Single(state => state.Type == type);
		} 

		public IEnumerable<State<CreditRequest>> Execute()
		{
			return _states.Value;
		}
	}
}
