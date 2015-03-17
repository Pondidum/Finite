using System;

namespace Finite.Configurations
{
	public class DefaultInstanceCreator : IInstanceCreator
	{
		public State<T> Create<T>(Type type)
		{
			var ctor = type.GetConstructor(Type.EmptyTypes);

			if (ctor == null)
			{
				throw new MissingMethodException(type.Name, "ctor");
			}

			return (State<T>) ctor.Invoke(null);
		}
	}
}
