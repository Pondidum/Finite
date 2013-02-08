using System;

namespace Finite
{
	public class MachineConfiguration
	{
		public Action<Type, Type> OnEnterState { internal get; set; }
		public Action<Type, Type> OnLeaveState { internal get; set; }

		public MachineConfiguration()
		{
			OnEnterState = (prev, next) => { };
			OnLeaveState = (prev, next) => { };
		}
	}
}
