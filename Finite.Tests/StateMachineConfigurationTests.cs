using System;
using Finite.Configurations;
using Finite.StateProviders;
using Finite.Tests.TestData;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Finite.Tests
{
	public class StateMachineConfigurationTests
	{
		[Fact]
		public void When_changing_state_onEnter_and_onLeave_should_be_called()
		{
			var stateChangedHandler = Substitute.For<IStateChangedHandler<TestArgs>>();
			var configuration = new MachineConfiguration<TestArgs>
			{
				StateChangedHandler = stateChangedHandler
			};

			var states = new ManualStateProvider<TestArgs>(new[]
			{
				typeof(FirstState),
				typeof(SecondState)
			});

			var machine = new StateMachine<TestArgs>(configuration, states, new TestArgs());

			machine.ResetTo<FirstState>();

			stateChangedHandler
				.DidNotReceiveWithAnyArgs()
				.OnLeaveState(null, null);

			stateChangedHandler
				.DidNotReceiveWithAnyArgs()
				.OnEnterState(null, null);
		}
	}
}
