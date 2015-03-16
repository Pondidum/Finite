namespace Finite
{
	public interface IMachineConfiguration<T>
	{
		void OnLeaveState(object sender, StateChangeEventArgs<T> stateChangeArgs);
		void OnEnterState(object sender, StateChangeEventArgs<T> stateChangeArgs);
	}
}
