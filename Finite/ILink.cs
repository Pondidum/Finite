namespace Finite
{
	public interface ILink<T>
	{
		string ConditionDescription { get; }
		State<T> Target { get; }

		bool IsActive(T switches);
	}
}
