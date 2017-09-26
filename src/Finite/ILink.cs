namespace Finite
{
	public interface ILink<TSwitches>
	{
		string ConditionDescription { get; }
		State<TSwitches> Target { get; }

		bool IsActive(TSwitches switches);
	}
}
