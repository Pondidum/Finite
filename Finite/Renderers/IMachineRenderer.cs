namespace Finite.Renderers
{
	public interface IMachineRenderer<TSwitches>
	{
		void Render(States<TSwitches> states);
	}
}
