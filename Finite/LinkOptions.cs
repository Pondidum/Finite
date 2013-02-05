using System;

namespace Finite
{
	public class LinkOptions<T>
	{
		private Func<T, bool> _condition;

		public Type Target { get; private set; }

		public LinkOptions(Type type)
		{
			Target = type;
		}

		public bool IsActive(T args)
		{
			return _condition.Invoke(args);
		}

		public void When(Func<T, bool> condition)
		{
			_condition = condition;
		}
	}
}
