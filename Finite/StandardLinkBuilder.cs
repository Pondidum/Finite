using System;
using System.Linq.Expressions;

namespace Finite
{
	internal class StandardLinkBuilder<TSwitches> : ILinkBuilder<TSwitches>
	{
		public Type TargetState { get; set; }
		public Expression<Func<TSwitches, bool>> Condition { get; set; }

		public ILink<TSwitches> CreateLink(StateRespository<TSwitches> stateRepository)
		{
			return new Link<TSwitches>(Condition)
			{
				Target = stateRepository.GetStateFor(TargetState)
			};
		}
	}

	internal class WrappingLinkBuilder<TSwitches> : ILinkBuilder<TSwitches>
	{
		public Type TargetState { get; set; }
		public ILink<TSwitches> Link { get; set; }
 
		public ILink<TSwitches> CreateLink(StateRespository<TSwitches> stateRepository)
		{
			Link.Target = stateRepository.GetStateFor(TargetState);
			return Link;
		}
	}
}
