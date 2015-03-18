using System;
using Finite.Configurations;

namespace Finite
{
	public interface IStateProvider<T>
	{
		void InitialiseStates(IInstanceCreator instanceCreator);
		State<T> GetStateFor<TState>();
	}
}
