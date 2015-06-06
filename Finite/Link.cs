using System;

namespace Finite
{
	public class Link<T>
	{
		private readonly Func<T, bool> _condition;

		public Link(State<T> type, Func<T, bool> condition)
		{
			Target = type;
			_condition = condition;
		}

		public State<T> Target { get; private set; }

		public bool IsActive(T switches)
		{
			return _condition.Invoke(switches);
		}
	}
}
