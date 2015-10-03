using System;
using System.Linq;
using Finite.StateProviders;
using Shouldly;
using Xunit;

namespace Finite.Tests
{
	public class StateTests
	{
		private StateMachine<object> _machine;
		private TestState _state;
		private UnlinkedState _unlinked;
		private CustomLinkedState _custom;

		public StateTests()
		{

			_state = new TestState();
			_unlinked = new UnlinkedState();
			_custom = new CustomLinkedState();

			var stateProvider = new ManualStateProvider<object>(new State<object>[]
			{
				_state,
				_unlinked,
				_custom
			});

			_machine = new StateMachine<object>(stateProvider, null);
		}

		[Fact]
		public void When_a_state_has_been_configured()
		{
			Should.Throw<InvalidOperationException>(() => _state.Modify());
		}

		[Fact]
		public void When_a_state_has_been_conifgured_with_no_links()
		{
			Should.Throw<InvalidOperationException>(() => _unlinked.Modify());
		}

		[Fact]
		public void When_a_state_has_custom_links()
		{
			Should.Throw<InvalidOperationException>(() => _custom.Modify());
		}

		[Fact]
		public void When_checking_if_transition_is_valid_by_state_instance()
		{
			_state.CanTransitionTo(_state).ShouldBe(true);
		}

		[Fact]
		public void When_checking_if_transition_is_valid_by_generic()
		{
			_state.CanTransitionTo<TestState>().ShouldBe(true);
		}

		[Fact]
		public void When_checking_if_transition_is_valid_by_type()
		{
			_state.CanTransitionTo(typeof(TestState)).ShouldBe(true);
		}

		[Fact]
		public void When_checking_if_transition_is_not_valid_by_state_instance()
		{
			_state.CanTransitionTo(_unlinked).ShouldBe(false);
		}

		[Fact]
		public void When_checking_if_transition_is_not_valid_by_generic()
		{
			_state.CanTransitionTo<UnlinkedState>().ShouldBe(false);
		}

		[Fact]
		public void When_checking_if_transition_is_not_valid_by_type()
		{
			_state.CanTransitionTo(typeof(UnlinkedState)).ShouldBe(false);
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
