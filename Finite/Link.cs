using System;

namespace Finite
{
	public class Link<T> : ILinkConfigurationExpression<T>
	{
		private Func<T, bool> _condition;

		public State<T> Target { get; private set; }

		public Link(State<T> type)
		{
			Target = type;
			_condition = x => true;		//no When specified, assume active
		}

		public bool IsActive(T switches)
		{
			return _condition.Invoke(switches);
		}

		void ILinkConfigurationExpression<T>.When(Func<T, bool> condition)
		{
			_condition = condition;
		}

	}
}
