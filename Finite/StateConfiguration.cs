using System;

namespace Finite
{
	internal class StateConfiguration<TSwitches>
	{
		public Type TargetState { get; set; }
		public Func<TSwitches, bool> Condition { get; set; }
	}
}
