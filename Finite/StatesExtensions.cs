using System.Linq;

namespace Finite
{
	public static class StatesExtensions
	{
		//this could be a method on States<>, but I want it for extensibility demoing.
		public static States<TSwitches> Scan<TSwitches>(this States<TSwitches> states)
		{
			var types = typeof(TSwitches)
				.Assembly
				.GetTypes()
				.Where(t => t.IsAbstract == false)
				.Where(t => typeof(State<TSwitches>).IsAssignableFrom(t))
				.ToArray();

			states.Are(types);

			return states;
		}
	}
}
