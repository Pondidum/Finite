namespace Finite.Configurations
{
	public interface IStateChangedHandler<T>
	{
		void OnLeaveState(object sender, StateChangeEventArgs<T> stateChangeArgs);
		void OnEnterState(object sender, StateChangeEventArgs<T> stateChangeArgs);
		void OnResetState(object sender, StateChangeEventArgs<T> stateChangeArgs);
	}
}
