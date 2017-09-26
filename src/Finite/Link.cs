using System;
using System.Linq.Expressions;

namespace Finite
{
	public class Link<TSwitches> : ILink<TSwitches>
	{
		private readonly Func<TSwitches, bool> _condition;
		private readonly Expression<Func<TSwitches, bool>> _expression;

		public Link(Expression<Func<TSwitches, bool>> condition)
		{
			_condition = condition.Compile();
			_expression = condition;
		}

		public string ConditionDescription => _expression.Body.ToString();
		public State<TSwitches> Target { get; set; }

		public bool IsActive(TSwitches switches)
		{
			return _condition.Invoke(switches);
		}
	}
}
