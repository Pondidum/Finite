using System;
using System.Collections.Generic;
using System.Linq;
using Finite.Configurations;
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

			var machine = new StateMachine<LightsSwitches>(
				config =>
				{
					config.StateChangedHandler = new LoggingStateChangedHandler(_log);
					config.InstanceCreator = new CustomInstanceCreator(_log);
				},
				states => states.Scan(),
				new LightsSwitches());

			machine.ResetTo<LightOff>();

			machine.TransitionTo<LightOnFull>();

			_log.ShouldSatisfyAllConditions(
				() => _log[0].Name.ShouldBe("Config.OnLeave"),
				() => _log[1].Name.ShouldBe("State.OnLeave"),
				() => _log[2].Name.ShouldBe("State.OnEnter"),
				() => _log[3].Name.ShouldBe("Config.OnEnter")
			);
		}

		[Fact]
		public void There_are_4_notifications_raised()
		{
			_log.Count.ShouldBe(4);
		}

		[Fact]
		public void First_notification_is_config_leave()
		{
			var first = _log[0];

			first.ShouldSatisfyAllConditions(
				() => first.Name.ShouldBe("Config.OnLeave"),
				() => first.Previous.ShouldBeOfType<LightOff>(),
				() => first.Next.ShouldBeOfType<LightOnFull>()
			);

		}

		[Fact]
		public void Second_notification_is_state_leave()
		{
			var second = _log[1];

			second.ShouldSatisfyAllConditions(
				() => second.Name.ShouldBe("State.OnLeave"),
				() => second.Previous.ShouldBeOfType<LightOff>(),
				() => second.Next.ShouldBeOfType<LightOnFull>()
			);
		}

		[Fact]
		public void Third_notification_is_state_enter()
		{
			var third = _log[2];

			third.ShouldSatisfyAllConditions(
				() => third.Name.ShouldBe("State.OnEnter"),
				() => third.Previous.ShouldBeOfType<LightOff>(),
				() => third.Next.ShouldBeOfType<LightOnFull>()
			);
		}

		[Fact]
		public void Fourth_notification_is_config_enter()
		{
			var fourth = _log[3];

			fourth.ShouldSatisfyAllConditions(
				() => fourth.Name.ShouldBe("Config.OnEnter"),
				() => fourth.Previous.ShouldBeOfType<LightOff>(),
				() => fourth.Next.ShouldBeOfType<LightOnFull>()
			);
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
