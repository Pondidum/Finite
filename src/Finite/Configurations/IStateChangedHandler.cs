namespace Finite.Configurations
{
	public interface IStateChangedHandler<TSwitches>
	{
		void OnLeaveState(object sender, StateChangeEventArgs<TSwitches> stateChangeArgs);
		void OnEnterState(object sender, StateChangeEventArgs<TSwitches> stateChangeArgs);
		void OnResetState(object sender, StateChangeEventArgs<TSwitches> stateChangeArgs);
	}
}
