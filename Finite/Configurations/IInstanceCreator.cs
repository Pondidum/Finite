using System;

namespace Finite.Configurations
{
	public interface IInstanceCreator
	{
		State<T> Create<T>(Type type);
	}
}
