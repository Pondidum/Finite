using System;

namespace Finite
{
	internal class LinkBuilder<TSwitches>
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
