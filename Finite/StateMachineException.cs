using System;

namespace Finite
{
	public class StateMachineException : Exception
	{
		public StateMachineException()
			: base("ResetTo() has not been called yet.")
		{
		}
	}
}
