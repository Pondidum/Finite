using System.Linq;
using Finite.StateProviders;
using Finite.Tests.Acceptance.States;
using Shouldly;
using Xunit;

namespace Finite.Tests.Acceptance
{
	public class LightsStateMachineAcceptanceTest
	{
		public StateMachine<LightsSwitches> Machine { get; private set; }

		public LightsStateMachineAcceptanceTest()
		{
			var allStates = new State<LightsSwitches>[]
			{
				new LightOff(),
				new LightOnDim(),
				new LightOnFull(),
			};

			Machine = new StateMachine<LightsSwitches>(
				new ManualStateProvider<LightsSwitches>(allStates),
				new LightsSwitches());
		}
	}

	public class InitialStateAcceptanceTest : LightsStateMachineAcceptanceTest
	{
		[Fact]
		public void When_the_machine_has_not_been_set_to_initial_state()
		{
			Should.Throw<StateMachineException>(() => Machine.TransitionTo<LightOnFull>());
		}
	}

	public class ValidStateAcceptanceTest : LightsStateMachineAcceptanceTest
	{
		public ValidStateAcceptanceTest()
		{
			Machine.ResetTo<LightOff>();
		}

		[Fact]
		public void The_current_state_should_be()
		{
						Machine.CurrentState.ShouldBeOfType<LightOff>();
		}

		[Fact]
		public void All_target_states()
		{
			Machine.AllTargetStates
				.Select(state => state.GetType())
				.ShouldBe(new[] { typeof(LightOnDim), typeof(LightOnFull) }, true);
		}

		[Fact]
		public void All_active_target_states()
		{
						Machine.ActiveTargetStates
				.Select(state => state.GetType())
				.ShouldBe(new[] { typeof(LightOnFull) });
		}

		[Fact]
		public void All_inactive_target_states()
		{
			Machine.InactiveTargetStates
				.Select(state => state.GetType())
				.ShouldBe(new[] { typeof(LightOnDim) });
		}
	}

	public class InvalidStateAcceptanceTest : LightsStateMachineAcceptanceTest
	{
		public InvalidStateAcceptanceTest()
		{
			Machine.ResetTo<LightOff>();
		}

		[Fact]
		public void Transitioning_to_an_invalid_state()
		{
			Should.Throw<InvalidTransitionException>(() => Machine.TransitionTo<LightOnDim>());
			Machine.CurrentState.ShouldBeOfType<LightOff>();

		}
	}
}
