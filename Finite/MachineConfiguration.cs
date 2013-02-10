using System;

namespace Finite
{
	public class MachineConfiguration<T>
	{
		public Action<T, Type, Type> OnEnterState { internal get; set; }
		public Action<T, Type, Type> OnLeaveState { internal get; set; }

		public MachineConfiguration()
		{
			OnEnterState = (args, prev, next) => { };
			OnLeaveState = (args, prev, next) => { };
		}
	}
}
