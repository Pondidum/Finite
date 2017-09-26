using System;

namespace Finite
{
	public class UnknownStateException : Exception
	{
		public UnknownStateException(Type state)
			: base(string.Format("The state '{0}' is not defined in the StateProvider.", state.Name))
		{
		}
	}
}
