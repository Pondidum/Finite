using System;
using System.Linq;
using Finite.StateProviders;
using Shouldly;
using Xunit;

namespace Finite.Tests
{
	public class StateTests
	{
		[Fact]
		public void When_a_state_has_been_configured()
		{
			var state = new TestState();
			var machine = new StateMachine<object>(new ManualStateProvider<object>(new[] { state }), null);

			Should.Throw<InvalidOperationException>(() => state.Modify());
		}

		[Fact]
		public void When_a_state_has_been_conifgured_with_no_links()
		{
			var state = new UnlinkedState();
			var machine = new StateMachine<object>(new ManualStateProvider<object>(new[] { state }), null);

			Should.Throw<InvalidOperationException>(() => state.Modify());
		}

		[Fact]
		public void When_a_state_has_custom_links()
		{
			var state = new CustomLinkedState();
			var machine = new StateMachine<object>(new ManualStateProvider<object>(new[] { state }), null);

			Should.Throw<InvalidOperationException>(() => state.Modify());
		}

		[Fact]
		public void When_checking_if_transition_is_valid_by_state_instance()
		{
			var state = new TestState();
			var unlinked = new UnlinkedState();
			var machine = new StateMachine<object>(new ManualStateProvider<object>(new State<object>[] { state, unlinked }), null);

			state.CanTransitionTo(state).ShouldBe(true);
		}

		[Fact]
		public void When_checking_if_transition_is_valid_by_generic()
		{
			var state = new TestState();
			var unlinked = new UnlinkedState();
			var machine = new StateMachine<object>(new ManualStateProvider<object>(new State<object>[] { state, unlinked }), null);

			state.CanTransitionTo<TestState>().ShouldBe(true);
		}

		[Fact]
		public void When_checking_if_transition_is_valid_by_type()
		{
			var state = new TestState();
			var unlinked = new UnlinkedState();
			var machine = new StateMachine<object>(new ManualStateProvider<object>(new State<object>[] { state, unlinked }), null);

			state.CanTransitionTo(typeof(TestState)).ShouldBe(true);
		}

		[Fact]
		public void When_checking_if_transition_is_not_valid_by_state_instance()
		{
			var state = new TestState();
			var unlinked = new UnlinkedState();
			var machine = new StateMachine<object>(new ManualStateProvider<object>(new State<object>[] { state, unlinked }), null);

			state.CanTransitionTo(unlinked).ShouldBe(false);
		}

		[Fact]
		public void When_checking_if_transition_is_not_valid_by_generic()
		{
			var state = new TestState();
			var unlinked = new UnlinkedState();
			var machine = new StateMachine<object>(new ManualStateProvider<object>(new State<object>[] { state, unlinked }), null);

			state.CanTransitionTo<UnlinkedState>().ShouldBe(false);
		}

		[Fact]
		public void When_checking_if_transition_is_not_valid_by_type()
		{
			var state = new TestState();
			var unlinked = new UnlinkedState();
			var machine = new StateMachine<object>(new ManualStateProvider<object>(new State<object>[] { state, unlinked }), null);

			state.CanTransitionTo(typeof(UnlinkedState)).ShouldBe(false);
		}

		private class TestState : State<object>
		{
			public TestState()
			{
				LinkTo<TestState>();
			}

			public void Modify()
			{
				LinkTo<TestState>();
			}
		}

		private class UnlinkedState : State<object>
		{
			public void Modify()
			{
				LinkTo<UnlinkedState>();
			}
		}

		private class CustomLinkedState : State<object>
		{
			public CustomLinkedState()
			{
				LinkTo<CustomLinkedState>(new CustomLink());
			}

			public void Modify()
			{
				LinkTo<CustomLinkedState>(new CustomLink());
			}
		}

		internal class CustomLink : ILink<object>
		{
			public string ConditionDescription { get; private set; }
			public State<object> Target { get; set; }

			public bool IsActive(object switches)
			{
				return true;
			}
		}
	}

}
