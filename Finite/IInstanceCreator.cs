using System;

namespace Finite
{
	public interface IInstanceCreator
	{
		State<T> Create<T>(Type type);
	}
}
