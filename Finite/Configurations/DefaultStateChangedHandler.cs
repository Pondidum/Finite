namespace Finite.Configurations
{
	public class DefaultStateChangedHandler<T> : IStateChangedHandler<T>
	{
		public void OnLeaveState(object sender, StateChangeEventArgs<T> stateChangeArgs)
		{
		}

		public void OnEnterState(object sender, StateChangeEventArgs<T> stateChangeArgs)
		{
		}
	}
}
