namespace Finite.Configurations
{
	public class MachineConfiguration<T>
	{
		public IInstanceCreator InstanceCreator { get; private set; }
		public IStateChangedHandler<T> StateChangedHandler { get; private set; }

		public MachineConfiguration()
		{
			CreateInstancesWith(new DefaultInstanceCreator());
			OnStateChange(new DefaultStateChangedHandler<T>());
		}

		public MachineConfiguration<T> CreateInstancesWith(IInstanceCreator instanceCreator)
		{
			InstanceCreator = instanceCreator;
			return this;
		}

		public MachineConfiguration<T> OnStateChange(IStateChangedHandler<T> handler)
		{
			StateChangedHandler = handler;
			return this;
		}
	}
}
