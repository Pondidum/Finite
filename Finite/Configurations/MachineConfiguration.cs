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
	}
}
