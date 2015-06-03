using System;
using Finite.Renderers;
using Finite.Tests.Acceptance;
using Finite.Tests.Acceptance.States;
using Xunit;

namespace Finite.Tests.Renderers
{
	public class GrapvizRendererTests
	{
		[Fact]
		public void Rendering_a_simple_graph()
		{
			var allStates = new[]
			{
				typeof (LightOff),
				typeof (LightOnDim),
				typeof (LightOnFull),
			};

			var switches = new LightsSwitches();
			var machine = new StateMachine<LightsSwitches>(states => states.Are(allStates), switches);

			var renderer = new GraphvizRenderer();
			renderer.Render(machine);

			Console.Write(renderer.Output);
		}
	}
}
