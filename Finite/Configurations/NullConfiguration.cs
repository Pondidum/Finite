namespace Finite.Configurations
{
	public class NullConfiguration<T> : IMachineConfiguration<T>
	{
		public void OnLeaveState(T args, State<T> previous, State<T> next)
		{
		}

		public void OnEnterState(T args, State<T> previous, State<T> next)
		{
		}
	}
}
