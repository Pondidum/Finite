namespace Finite
{
	public interface ILink<TSwitches>
	{
		string ConditionDescription { get; }
		State<TSwitches> Target { get; set; }

		bool IsActive(TSwitches switches);
	}
}
