namespace Finite
{
	public class StateChangeEventArgs<T>
	{
		public T Args { get; private set; }
		public State<T> Previous { get; private set; }
		public State<T> Next { get; private set; }

		public StateChangeEventArgs(T args, State<T> previous, State<T> next)
		{
			Args = args;
			Previous = previous;
			Next = next;
		}
	}
}
