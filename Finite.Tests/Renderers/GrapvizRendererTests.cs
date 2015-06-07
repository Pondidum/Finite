using System;
using Finite.Renderers;
using Finite.StateProviders;
using Finite.Tests.Acceptance;
using Finite.Tests.Acceptance.States;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Finite.Tests.Renderers
{
	public class GrapvizRendererTests
	{
		private const string GraphDsl =
			"digraph {\r\n\tLightOff -> LightOnFull[label=\"(l.OnBattery == False)\"];\r\n\tLightOff -> LightOnDim[label=\"l.OnBattery\"];\r\n\tLightOnDim -> LightOnFull[label=\"(l.OnBattery == False)\"];\r\n\tLightOnDim -> LightOff[label=\"True\"];\r\n\tLightOnFull -> LightOnDim[label=\"l.OnBattery\"];\r\n\tLightOnFull -> LightOff[label=\"True\"];\r\n}\r\n";

		private readonly ITestOutputHelper _output;

		public GrapvizRendererTests(ITestOutputHelper output)
		{
			_output = output;
		}

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

			_output.WriteLine(renderer.Output);
			renderer.Output.ShouldBe(GraphDsl);
		}
	}
}
