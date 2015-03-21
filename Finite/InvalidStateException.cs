using System;

namespace Finite
{
	public class InvalidStateException : Exception
	{
		public InvalidStateException(Type switches, Type invalidType)
			: base(string.Format("The type '{0}' cannot be added as it doesn't inherit 'State<{1}>'", invalidType.Name, switches.Name))
		{
			
		}
	}
}
