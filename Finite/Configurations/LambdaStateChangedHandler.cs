using System;

namespace Finite.Configurations
{
	public class LambdaStateChangedHandler<TSwitches> : IStateChangedHandler<TSwitches>
	{

		public Action<object, StateChangeEventArgs<TSwitches>> OnLeave { get; set; }
		public Action<object, StateChangeEventArgs<TSwitches>> OnEnter { get; set; }
		public Action<object, StateChangeEventArgs<TSwitches>> OnReset { get; set; }

		public void OnLeaveState(object sender, StateChangeEventArgs<TSwitches> stateChangeArgs)
		{
			OnLeave?.Invoke(sender, stateChangeArgs);
		}

		public void OnEnterState(object sender, StateChangeEventArgs<TSwitches> stateChangeArgs)
		{
			OnEnter?.Invoke(sender, stateChangeArgs);
		}

		public void OnResetState(object sender, StateChangeEventArgs<TSwitches> stateChangeArgs)
		{
			OnReset?.Invoke(sender, stateChangeArgs);
		}
	}
}
