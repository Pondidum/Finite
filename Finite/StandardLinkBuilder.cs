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
			return new Link<TSwitches>(
				stateRepository.GetStateFor(TargetState),
				Condition);
		}
	}
}
