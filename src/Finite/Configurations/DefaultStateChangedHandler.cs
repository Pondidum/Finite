namespace Finite.Configurations
{
	public class DefaultStateChangedHandler<TSwitches> : IStateChangedHandler<TSwitches>
	{
		public void OnLeaveState(object sender, StateChangeEventArgs<TSwitches> stateChangeArgs)
		{
		}

		public void OnEnterState(object sender, StateChangeEventArgs<TSwitches> stateChangeArgs)
		{
		}

		public void OnResetState(object sender, StateChangeEventArgs<TSwitches> stateChangeArgs)
		{
		}
	}
}
