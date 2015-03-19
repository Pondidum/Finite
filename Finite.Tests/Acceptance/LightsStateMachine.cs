using System.Linq;
using Finite.StateProviders;
using Finite.Tests.Acceptance.States;
using Shouldly;
using Xunit;

namespace Finite.Tests.Acceptance
{
	public class LightsStateMachine
	{
		[Fact]
		public void Configuring_the_machine()
		{
			var states = new ManualStateProvider<LightsSwitches>(new[]
			{
				typeof(LightOff),
				typeof(LightOnDim),
				typeof(LightOnFull),
			});

			var switches = new LightsSwitches();
			
			var machine = new StateMachine<LightsSwitches>(states, switches);
			machine.ResetTo<LightOff>();

			machine
				.GetAllTargetStates()
				.Select(state => state.GetType())
				.ShouldBe(new[] { typeof(LightOnDim), typeof(LightOnFull)}, true);

			Should.Throw<InvalidTransitionException>(() => machine.TransitionTo<LightOnDim>());
		} 
	}
}
