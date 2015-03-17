namespace Finite
{
	public class StateChangeEventArgs<T>
	{
		public T Switches { get; private set; }
		public State<T> Previous { get; private set; }
		public State<T> Next { get; private set; }

		public StateChangeEventArgs(T switches, State<T> previous, State<T> next)
		{
			Switches = switches;
			Previous = previous;
			Next = next;
		}
	}
}
