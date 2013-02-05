using System;

namespace Finite
{
	internal class InvalidTransitionException : Exception
	{
		public InvalidTransitionException(Type currentState, Type targetState)
			: base(string.Format("There is no active link from {0} to {1}.", currentState.Name, targetState.Name))
		{

		}
	}
}
