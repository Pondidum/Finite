using System;
using System.CodeDom;
using System.Linq;
using System.Security.Policy;
using Finite.Tests.TestData;
using Shouldly;
using Xunit;

namespace Finite.Tests
{
	public class StateMachineTests
	{
		[Fact]
		public void When_setting_the_initial_state()
		{
			var machine = new StateMachine<TestArgs>(new DefaultInstanceCreator());
			machine.InitialiseFrom(new Type[] {typeof(FirstState), typeof(SecondState), typeof(ThirdState), typeof(FourthState)});

			machine.SetStateTo<FirstState>();

			machine.CurrentState.ShouldBe(typeof(FirstState));
			machine.GetAllTargetStates().ShouldBe(new Type[] {typeof (SecondState)});
		}

		[Fact]
		public void When_trying_to_move_to_non_allowed_state()
		{
			var machine = new StateMachine<TestArgs>(new DefaultInstanceCreator());
			machine.InitialiseFrom(new Type[] { typeof(FirstState), typeof(SecondState), typeof(ThirdState), typeof(FourthState) });

			machine.SetStateTo<FirstState>();

			machine.CurrentState.ShouldBe(typeof(FirstState));
			Assert.Throws<InvalidTransitionException>(() => machine.SetStateTo<ThirdState>());
		}

		[Fact]
		public void When_trying_to_move_to_an_allowed_state()
		{
			var machine = new StateMachine<TestArgs>(new DefaultInstanceCreator());
			machine.InitialiseFrom(new Type[] { typeof(FirstState), typeof(SecondState), typeof(ThirdState), typeof(FourthState) });

			machine.SetStateTo<FirstState>();
			machine.CurrentState.ShouldBe(typeof(FirstState));

			machine.SetStateTo<SecondState>();
			machine.CurrentState.ShouldBe(typeof(SecondState));
		}
	}
}
