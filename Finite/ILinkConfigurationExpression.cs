using System;

namespace Finite
{
	public interface ILinkConfigurationExpression<T>
	{
		void When(Func<T, bool> condition);
	}
}
