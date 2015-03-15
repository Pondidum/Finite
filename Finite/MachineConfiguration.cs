using System;

namespace Finite
{
	public class MachineConfiguration<T>
	{
		public Action<T, Type, Type> OnEnterState { get; set; }
		public Action<T, Type, Type> OnLeaveState { get; set; }

		public MachineConfiguration()
		{
			OnEnterState = (args, prev, next) => { };
			OnLeaveState = (args, prev, next) => { };
		}
	}
}
