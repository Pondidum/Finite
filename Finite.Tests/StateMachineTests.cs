using System;
using System.CodeDom;
using System.Linq;
using System.Security.Policy;
using Finite.InstanceCreators;
using Finite.StateProviders;
using Finite.Tests.TestData;
using Shouldly;
using Xunit;

namespace Finite.Tests
{
	public class StateMachineTests
	{
		private readonly StateMachine<TestArgs> _machine;

		public StateMachineTests()
		{
			var states = new ManualStateProvider<TestArgs>(
			new DefaultInstanceCreator(),
			new Type[] { typeof(FirstState), typeof(SecondState), typeof(ThirdState), typeof(FourthState) });

			_machine = new StateMachine<TestArgs>(states, new TestArgs());
		}

		[Fact]
		public void When_setting_the_initial_state()
		{
			_machine.SetStateTo<FirstState>();

			_machine.CurrentState.ShouldBeOfType<FirstState>();
			_machine.GetAllTargetStates().Single().ShouldBeOfType<SecondState>();
		}

		[Fact]
		public void When_trying_to_move_to_non_allowed_state()
		{
			_machine.SetStateTo<FirstState>();

			_machine.CurrentState.ShouldBeOfType<FirstState>();
			Should.Throw<InvalidTransitionException>(() => _machine.SetStateTo<ThirdState>());
		}

		[Fact]
		public void When_trying_to_move_to_an_allowed_state()
		{

			_machine.SetStateTo<FirstState>();
			_machine.CurrentState.ShouldBeOfType<FirstState>();

			_machine.SetStateTo<SecondState>();
			_machine.CurrentState.ShouldBeOfType<SecondState>();
		}

		[Fact]
		public void State_can_only_be_set_to_a_state_the_machine_knows_about()
		{
			Should.Throw<UnknownStateException>(() => _machine.SetStateTo<FifthState>());
		}
	}
}
