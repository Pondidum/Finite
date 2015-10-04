using Finite.Configurations;
using Finite.StateProviders;
using Finite.Tests.TestData;
using Shouldly;
using Xunit;

namespace Finite.Tests.Configurations
{
	public class LambdaStateChangedHandlerTests
	{
		private readonly StateMachine<TestArgs> _machine;
		private MachineConfiguration<TestArgs> _config;

		public LambdaStateChangedHandlerTests()
		{
			_config = new MachineConfiguration<TestArgs>();

			var args = new TestArgs();
			var stateProvider = new ScanningStateProvider<TestArgs>();

			_machine = new StateMachine<TestArgs>(_config, stateProvider, args);
		}

		[Fact]
		public void When_reseting_state_and_no_action_is_specified()
		{
			var handler = new LambdaStateChangedHandler<TestArgs>();
			_config.OnStateChange(handler);

			_machine.ResetTo<FirstState>();
		}

		[Fact]
		public void When_changing_state_and_no_aciton_is_specified()
		{
			var handler = new LambdaStateChangedHandler<TestArgs>();
			_config.OnStateChange(handler);

			_machine.ResetTo<FirstState>();
			_machine.TransitionTo<SecondState>();
		}

		[Fact]
		public void When_reseting_state_and_there_is_an_action_specified()
		{
			var resetCalled = 0;
			var handler = new LambdaStateChangedHandler<TestArgs>();
			handler.OnReset = (s, a) => { resetCalled++; };

			_config.OnStateChange(handler);

			_machine.ResetTo<FirstState>();

			resetCalled.ShouldBe(1);
		}

		[Fact]
		public void When_changing_state_and_an_onleave_action_is_specified()
		{
			var leaveCalled = 0;
			var handler = new LambdaStateChangedHandler<TestArgs>();
			handler.OnLeave = (s, a) => { leaveCalled++; };

			_config.OnStateChange(handler);
			_machine.ResetTo<FirstState>();

			_machine.TransitionTo<SecondState>();
			leaveCalled.ShouldBe(1);
		}

		[Fact]
		public void When_changing_state_and_an_onenter_action_is_specified()
		{
			var enterCalled = 0;
			var handler = new LambdaStateChangedHandler<TestArgs>();
			handler.OnEnter = (s, a) => { enterCalled++; };

			_config.OnStateChange(handler);
			_machine.ResetTo<FirstState>();

			_machine.TransitionTo<SecondState>();
			enterCalled.ShouldBe(1);
		}
	}
}
