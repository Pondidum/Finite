namespace Finite.Configurations
{
	public class NullConfiguration<T> : IMachineConfiguration<T>
	{
		public void OnLeaveState(object sender, StateChangeEventArgs<T> stateChangeArgs)
		{
		}

		public void OnEnterState(object sender, StateChangeEventArgs<T> stateChangeArgs)
		{
		}
	}
}
