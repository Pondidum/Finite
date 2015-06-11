namespace Finite
{
	internal interface ILinkBuilder<TSwitches>
	{
		ILink<TSwitches> CreateLink(StateRespository<TSwitches> stateRepository);
	}
}
