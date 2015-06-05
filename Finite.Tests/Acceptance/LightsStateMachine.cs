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
			var allStates = new State<LightsSwitches>[]
			{
				new LightOff(),
				new LightOnDim(),
				new LightOnFull(),
			};

			var switches = new LightsSwitches();

			var machine = new StateMachine<LightsSwitches>(new ManualStateProvider<LightsSwitches>(allStates), switches);
			
			machine.ResetTo<LightOff>();
			machine.CurrentState.ShouldBeOfType<LightOff>();

			machine.AllTargetStates
				.Select(state => state.GetType())
				.ShouldBe(new[] { typeof(LightOnDim), typeof(LightOnFull) }, true);

			machine.ActiveTargetStates
				.Select(state => state.GetType())
				.ShouldBe(new[] { typeof(LightOnFull) });

			machine.InactiveTargetStates
				.Select(state => state.GetType())
				.ShouldBe(new[] { typeof(LightOnDim) });

			Should.Throw<InvalidTransitionException>(() => machine.TransitionTo<LightOnDim>());
			machine.CurrentState.ShouldBeOfType<LightOff>();

			machine.TransitionTo<LightOnFull>();
			machine.CurrentState.ShouldBeOfType<LightOnFull>();
		}
	}
}
