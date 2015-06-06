using System;
using Finite.Renderers;
using Finite.StateProviders;
using Finite.Tests.Acceptance;
using Finite.Tests.Acceptance.States;
using Shouldly;
using Xunit;

namespace Finite.Tests.Renderers
{
	public class GrapvizRendererTests
	{
		private const string GraphDsl =
			"digraph {\r\n\tLightOff -> LightOnFull;\r\n\tLightOff -> LightOnDim;\r\n\tLightOnDim -> LightOnFull;\r\n\tLightOnDim -> LightOff;\r\n\tLightOnFull -> LightOnDim;\r\n\tLightOnFull -> LightOff;\r\n}\r\n";

		[Fact]
		public void Rendering_a_simple_graph()
		{
			var allStates = new State<LightsSwitches>[]
			{
				new LightOff(),
				new LightOnDim(),
				new LightOnFull(),
			};

			var switches = new LightsSwitches();
			var machine = new StateMachine<LightsSwitches>(new ManualStateProvider<LightsSwitches>(allStates), switches);

			var renderer = new GraphvizRenderer();
			renderer.Render(machine);

			renderer.Output.ShouldBe(GraphDsl);
		}
	}
}
