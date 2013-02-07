using System;

namespace Finite
{
	public class Link<T> : ILinkConfigurationExpression<T>
	{
		private Func<T, bool> _condition;

		public Type Target { get; private set; }

		public Link(Type type)
		{
			Target = type;
		}

		public bool IsActive(T args)
		{
			return _condition.Invoke(args);
		}

		void ILinkConfigurationExpression<T>.When(Func<T, bool> condition)
		{
			_condition = condition;
		}

	}
}
