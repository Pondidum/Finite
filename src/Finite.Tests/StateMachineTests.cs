﻿using System.Linq;
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
			var allStates = new State<TestArgs>[]
			{
				new FirstState(),
				new SecondState(),
				new ThirdState(),
				new FourthState()
			};

			_machine = new StateMachine<TestArgs>(new ManualStateProvider<TestArgs>(allStates), new TestArgs());
		}

		[Fact]
		public void When_setting_the_initial_state()
		{
			_machine.ResetTo<FirstState>();

			_machine.CurrentState.ShouldBeOfType<FirstState>();
			_machine.CurrentState.Links.Single().Target.ShouldBeOfType<SecondState>();
		}

		[Fact]
		public void When_trying_to_move_to_non_allowed_state()
		{
			_machine.ResetTo<FirstState>();
			_machine.CurrentState.ShouldBeOfType<FirstState>();

			Should.Throw<InvalidTransitionException>(() => _machine.TransitionTo<ThirdState>());
		}

		[Fact]
		public void When_trying_to_move_to_an_allowed_state()
		{

			_machine.ResetTo<FirstState>();
			_machine.CurrentState.ShouldBeOfType<FirstState>();

			_machine.TransitionTo<SecondState>();
			_machine.CurrentState.ShouldBeOfType<SecondState>();
		}

		[Fact]
		public void State_can_only_be_set_to_a_state_the_machine_knows_about()
		{
			Should.Throw<UnknownStateException>(() => _machine.TransitionTo<FifthState>());
		}

		[Fact]
		public void When_resetting_with_a_type()
		{
			var type = typeof(object);
			Should.Throw<UnknownStateException>(() => _machine.ResetTo(type));
		}

		[Fact]
		public void When_transitioning_to_a_type()
		{
			var type = typeof(object);
			Should.Throw<UnknownStateException>(() => _machine.TransitionTo(type));
		}
	}
}
