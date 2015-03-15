using System;

namespace Finite
{
	public class MachineConfiguration<T>
	{
		public Action<T, State<T>, State<T>> OnEnterState { get; set; }
		public Action<T, State<T>, State<T>> OnLeaveState { get; set; }

		public MachineConfiguration()
		{
			OnEnterState = (args, prev, next) => { };
			OnLeaveState = (args, prev, next) => { };
		}
	}
}
