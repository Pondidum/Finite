using Finite;

namespace Sample.CustomStateLoading
{
	public abstract class CustomState<T> : State<T>
	{
		public abstract Progress Type { get; }
	}
}
