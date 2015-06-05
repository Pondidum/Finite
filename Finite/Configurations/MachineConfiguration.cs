namespace Finite.Configurations
{
	public class MachineConfiguration<T>
	{
		public IStateChangedHandler<T> StateChangedHandler { get; private set; }

		public MachineConfiguration()
		{
			OnStateChange(new DefaultStateChangedHandler<T>());
		}

		public MachineConfiguration<T> OnStateChange(IStateChangedHandler<T> handler)
		{
			StateChangedHandler = handler;
			return this;
		}
	}
}
