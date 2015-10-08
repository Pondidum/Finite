using Finite;
using Finite.Renderers;
using Finite.StateProviders;
using Sample.Common;
using Xunit;
using Xunit.Abstractions;

namespace Sample.Rendering
{
	public class GraphicalOutput
	{
		private readonly ITestOutputHelper _output;

		public GraphicalOutput(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public void Rendering_the_state_machine_with_graphviz()
		{
			var machine = new StateMachine<CreditRequest>(
				new ScanningStateProvider<CreditRequest>(),
				new CreditRequest());

			var renderer = new GraphvizRenderer();
			renderer.Render(machine);

			_output.WriteLine("Copy and paste the following output into http://www.webgraphviz.com/ :");

			_output.WriteLine(renderer.Output);
		}
	}
}
