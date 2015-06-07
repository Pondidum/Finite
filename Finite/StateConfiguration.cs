using System;
using System.Linq.Expressions;

namespace Finite
{
	internal class StateConfiguration<TSwitches>
	{
		public Type TargetState { get; set; }
		public Expression<Func<TSwitches, bool>> Condition { get; set; }
	}
}
