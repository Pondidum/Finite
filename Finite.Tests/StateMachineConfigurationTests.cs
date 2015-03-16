using System;
using Finite.InstanceCreators;
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
			var configuration = Substitute.For<IMachineConfiguration<TestArgs>>();

			var states = new ManualStateProvider<TestArgs>(
				new DefaultInstanceCreator(),
				new[] { typeof(FirstState), typeof(SecondState) });

			var machine = new StateMachine<TestArgs>(configuration, states, new TestArgs());
			
			machine.SetStateTo<FirstState>();

			configuration
				.Received(1)
				.OnLeaveState(machine, Arg.Is<StateChangeEventArgs<TestArgs>>(a => a.Next.GetType() == typeof(FirstState)));

			configuration
				.Received(1)
				.OnEnterState(machine, Arg.Is<StateChangeEventArgs<TestArgs>>(a => a.Next.GetType() == typeof(FirstState)));
		}
	}
}
