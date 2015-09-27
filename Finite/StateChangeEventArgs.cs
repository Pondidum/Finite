namespace Finite
{
	public class StateChangeEventArgs<TSwitches>
	{
		public TSwitches Switches { get; private set; }
		public State<TSwitches> Previous { get; private set; }
		public State<TSwitches> Next { get; private set; }

		public StateChangeEventArgs(TSwitches switches, State<TSwitches> previous, State<TSwitches> next)
		{
			Switches = switches;
			Previous = previous;
			Next = next;
		}
	}
}
