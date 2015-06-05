using System;
using System.Collections.Generic;
using System.Linq;
using Finite.Configurations;
using Finite.StateProviders;
using Finite.Tests.Acceptance.States;
using Finite.Tests.TestData;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Finite.Tests.Acceptance
{
	public class StateChangedTests
	{
		private readonly List<LogEntry> _log;

		public StateChangedTests()
		{
			_log = new List<LogEntry>();

			var config = new MachineConfiguration<LightsSwitches>()
				.OnStateChange(new LoggingStateChangedHandler(_log));

			var machine = new StateMachine<LightsSwitches>(
				config,
				new ScanningStateProvider<LightsSwitches>(new CustomInstanceCreator(_log)),
				new LightsSwitches());

			machine.ResetTo<LightOff>();
			machine.TransitionTo<LightOnFull>();

		}

		[Fact]
		public void There_are_the_correct_number_of_notifications_raised()
		{
			_log.Count.ShouldBe(5);
		}

		[Fact]
		public void The_initial_notification_is_reset()
		{
			var init = _log[0];

			init.Name.ShouldBe("Config.OnReset");
			init.Previous.ShouldBe(null);
			init.Next.ShouldBeOfType<LightOff>();
		}

		[Fact]
		public void First_notification_is_config_leave()
		{
			var first = _log[1];

			first.Name.ShouldBe("Config.OnLeave");
			first.Previous.ShouldBeOfType<LightOff>();
			first.Next.ShouldBeOfType<LightOnFull>();
		}

		[Fact]
		public void Second_notification_is_state_leave()
		{
			var second = _log[2];

			second.Name.ShouldBe("State.OnLeave");
			second.Previous.ShouldBeOfType<LightOff>();
			second.Next.ShouldBeOfType<LightOnFull>();
		}

		[Fact]
		public void Third_notification_is_state_enter()
		{
			var third = _log[3];

			third.Name.ShouldBe("State.OnEnter");
			third.Previous.ShouldBeOfType<LightOff>();
			third.Next.ShouldBeOfType<LightOnFull>();
		}

		[Fact]
		public void Fourth_notification_is_config_enter()
		{
			var fourth = _log[4];

			fourth.Name.ShouldBe("Config.OnEnter");
			fourth.Previous.ShouldBeOfType<LightOff>();
			fourth.Next.ShouldBeOfType<LightOnFull>();
		}

		private class LogEntry
		{
			public string Name;
			public State<LightsSwitches> Previous;
			public State<LightsSwitches> Next;

			public LogEntry(string name, State<LightsSwitches> prev, State<LightsSwitches> next)
			{
				Name = name;
				Previous = prev;
				Next = next;
			}
		}

		private class LoggingStateChangedHandler : IStateChangedHandler<LightsSwitches>
		{
			private readonly List<LogEntry> _log;

			public LoggingStateChangedHandler(List<LogEntry> log)
			{
				_log = log;
			}

			public void OnResetState(object sender, StateChangeEventArgs<LightsSwitches> stateChangeArgs)
			{
				_log.Add(new LogEntry("Config.OnReset", stateChangeArgs.Previous, stateChangeArgs.Next));
			}

			public void OnEnterState(object sender, StateChangeEventArgs<LightsSwitches> stateChangeArgs)
			{
				_log.Add(new LogEntry("Config.OnEnter", stateChangeArgs.Previous, stateChangeArgs.Next));
			}

			public void OnLeaveState(object sender, StateChangeEventArgs<LightsSwitches> stateChangeArgs)
			{
				_log.Add(new LogEntry("Config.OnLeave", stateChangeArgs.Previous, stateChangeArgs.Next));
			}

		}
		private class CustomInstanceCreator : IInstanceCreator
		{
			private readonly List<LogEntry> _log;
			private readonly DefaultInstanceCreator _default;

			public CustomInstanceCreator(List<LogEntry> log)
			{
				_log = log;
				_default = new DefaultInstanceCreator();
			}

			public State<T> Create<T>(Type type)
			{
				var state = _default.Create<T>(type);

				var ls = state as LightState;

				ls.OnEnterAction = args => _log.Add(new LogEntry("State.OnEnter", args.Previous, args.Next));
				ls.OnLeaveAction = args => _log.Add(new LogEntry("State.OnLeave", args.Previous, args.Next));

				return state;
			}
		}
	}
}
