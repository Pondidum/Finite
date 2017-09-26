using Finite.Configurations;
using Finite.Tests.TestData;
using Shouldly;
using Xunit;

namespace Finite.Tests.Configurations
{
	public class MachineConfigurationTests
	{
		private readonly StateChangeEventArgs<TestArgs> _args;
		private readonly MachineConfiguration<TestArgs> _config;

		public MachineConfigurationTests()
		{
			_args = new StateChangeEventArgs<TestArgs>(new TestArgs(), new FirstState(), new SecondState());
			_config = new MachineConfiguration<TestArgs>();
		}

		private void Reset()
		{
			_config.StateChangedHandler.OnResetState(this, _args);
		}

		private void Transition()
		{
			_config.StateChangedHandler.OnLeaveState(this, _args);
			_config.StateChangedHandler.OnEnterState(this, _args);
		}

		[Fact]
		public void When_specifying_nothing()
		{
			_config.OnStateChange();

			Should.NotThrow(() => Reset());
			Should.NotThrow(() => Transition());
		}

		[Fact]
		public void When_specifying_on_enter()
		{
			var calls = 0;
			_config.OnStateChange(enter: (s, e) => calls++);

			Transition();
			
			calls.ShouldBe(1);
		}

		[Fact]
		public void When_specifying_on_leave()
		{
			var calls = 0;
			_config.OnStateChange(leave: (s, e) => calls++);

			Transition();

			calls.ShouldBe(1);
		}

		[Fact]
		public void When_specifying_on_reset()
		{
			var calls = 0;
			_config.OnStateChange(reset: (s, e) => calls++);

			Reset();

			calls.ShouldBe(1);
		}

		[Fact]
		public void When_specifying_on_enter_twice()
		{
			var first = 0;
			var second = 0;

			_config.OnStateChange(enter: (s, e) => first++);
			_config.OnStateChange(enter: (s, e) => second++);

			Transition();

			first.ShouldBe(0);
			second.ShouldBe(1);
		}

		[Fact]
		public void When_specifying_on_leave_twice()
		{
			var first = 0;
			var second = 0;

			_config.OnStateChange(leave: (s, e) => first++);
			_config.OnStateChange(leave: (s, e) => second++);

			Transition();

			first.ShouldBe(0);
			second.ShouldBe(1);
		}

		[Fact]
		public void When_specifying_on_reset_twice()
		{
			var first = 0;
			var second = 0;

			_config.OnStateChange(reset: (s, e) => first++);
			_config.OnStateChange(reset: (s, e) => second++);

			Reset();

			first.ShouldBe(0);
			second.ShouldBe(1);
		}

		[Fact]
		public void When_specifying_on_enter_and_on_leave_together()
		{
			var leave = 0;
			var enter = 0;

			_config.OnStateChange(
				enter: (s, e) => enter++,
				leave: (s, e) => leave++
			);

			Transition();

			leave.ShouldBe(1);
			enter.ShouldBe(1);
		}

		[Fact]
		public void When_specifying_on_enter_and_on_leave_separately()
		{
			var leave = 0;
			var enter = 0;

			_config.OnStateChange(enter: (s, e) => enter++);
			_config.OnStateChange(leave: (s, e) => leave++);

			Transition();

			leave.ShouldBe(1);
			enter.ShouldBe(1);
		}

		[Fact]
		public void When_there_is_already_a_custom_handler()
		{
			var enter = 0;

			_config.OnStateChange(new ThrowingStateChangedHandler());
			_config.OnStateChange(enter: (s, e) => enter++);

			Transition();

			enter.ShouldBe(1);
		}


		private class ThrowingStateChangedHandler: IStateChangedHandler<TestArgs>
		{
			public void OnLeaveState(object sender, StateChangeEventArgs<TestArgs> stateChangeArgs)
			{
				throw new System.NotImplementedException();
			}

			public void OnEnterState(object sender, StateChangeEventArgs<TestArgs> stateChangeArgs)
			{
				throw new System.NotImplementedException();
			}

			public void OnResetState(object sender, StateChangeEventArgs<TestArgs> stateChangeArgs)
			{
				throw new System.NotImplementedException();
			}
		}
	}
}
