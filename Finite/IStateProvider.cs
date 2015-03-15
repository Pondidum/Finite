using System;

namespace Finite
{
	public interface IStateProvider<T>
	{
		State<T> GetStateFor(Type stateType);
		void ThrowIfNotKnown(Type stateType);
	}
}
