namespace Finite
{
	public interface ILink<T>
	{
		string ConditionDescription { get; }
		State<T> Target { get; set; }

		bool IsActive(T switches);
	}
}
