namespace Finite
{
	public interface IStateConfiguration<T>
	{
		ILinkConfigurationExpression<T> LinkTo<TTarget>() where TTarget : State<T>;
	}
}
