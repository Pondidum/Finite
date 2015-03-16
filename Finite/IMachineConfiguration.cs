namespace Finite
{
	public interface IMachineConfiguration<T>
	{
		void OnLeaveState(T args, State<T> previous, State<T> next);
		void OnEnterState(T args, State<T> previous, State<T> next);
	}
}
