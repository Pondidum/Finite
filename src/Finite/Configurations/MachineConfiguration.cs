using System;

namespace Finite.Configurations
{
	public class MachineConfiguration<TSwitches>
	{
		public IStateChangedHandler<TSwitches> StateChangedHandler { get; private set; }

		public MachineConfiguration()
		{
			OnStateChange(new DefaultStateChangedHandler<TSwitches>());
		}

		public MachineConfiguration<TSwitches> OnStateChange(IStateChangedHandler<TSwitches> handler)
		{
			StateChangedHandler = handler;
			return this;
		}

		public MachineConfiguration<TSwitches> OnStateChange(Action<object, StateChangeEventArgs<TSwitches>> enter = null, Action<object, StateChangeEventArgs<TSwitches>> leave = null, Action<object, StateChangeEventArgs<TSwitches>> reset = null)
		{
			var handler = StateChangedHandler as LambdaStateChangedHandler<TSwitches> ?? new LambdaStateChangedHandler<TSwitches>();

			if (enter != null)
				handler.OnEnter = enter;

			if (leave != null)
				handler.OnLeave = leave;

			if (reset != null)
				handler.OnReset = reset;

			StateChangedHandler = handler;
			return this;
		}
	}
}
