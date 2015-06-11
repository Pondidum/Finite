using System;
using System.Linq.Expressions;

namespace Finite
{
	public class Link<T> : ILink<T>
	{
		private readonly Func<T, bool> _condition;
		private readonly Expression<Func<T, bool>> _expression;

		public Link(State<T> type, Expression<Func<T, bool>> condition)
		{
			Target = type;
			_condition = condition.Compile();
			_expression = condition;
		}

		public string ConditionDescription { get { return _expression.Body.ToString(); } }
		public State<T> Target { get; private set; }

		public bool IsActive(T switches)
		{
			return _condition.Invoke(switches);
		}
	}
}
