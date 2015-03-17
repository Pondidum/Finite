using Finite.InstanceCreators;

namespace Finite.Configurations
{
	public class MachineConfiguration<T>
	{
		public IInstanceCreator InstanceCreator { get; set; }
		public IStateChangedHandler<T> StateChangedHandler { get; set; }
		public IStateProvider<T> StateProvider { get; set; }

		public MachineConfiguration()
		{
			InstanceCreator = new DefaultInstanceCreator();
			StateChangedHandler = new DefaultStateChangedHandler<T>();
		}
	}
}
