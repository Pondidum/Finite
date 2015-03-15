using System;
using System.Collections.Generic;
using System.Linq;

namespace Finite
{
	public class StateMachine<T>
	{
		private readonly IStateProvider<T> _stateProvider;
		private readonly T _args;

		public StateMachine(IStateProvider<T> stateProvider, T args)
		{
			_stateProvider = stateProvider;
			_args = args;
			Configuration = new MachineConfiguration<T>();
		}

		public MachineConfiguration<T> Configuration { get; private set; }
		public State<T> CurrentState { get; private set; }


		public void SetStateTo<TTarget>() where TTarget : State<T>
		{
			SetStateTo(typeof (TTarget));
		}
		
		public IEnumerable<Type> GetAllTargetStates()
		{
			return CurrentState.Links.Select(l => l.Target);
		}

		public IEnumerable<Type> GetActiveTargetStates()
		{
			return CurrentState.Links.Where(l => l.IsActive(_args)).Select(l => l.Target);
		}

		private void SetStateTo(Type target)
		{
			_stateProvider.ThrowIfNotKnown(target);

			if (CurrentState != null && GetActiveTargetStates().Contains(target) == false)
			{
				throw new InvalidTransitionException(CurrentState.GetType(), target);
			}

			var previous = CurrentState;

			OnLeaveState(target, previous);

			CurrentState = _stateProvider.GetStateFor(target);
			
			OnEnterState(target, previous);
		}

		private void OnEnterState(Type target, State<T> previous)
		{
			var prevType = previous != null ? previous.GetType() : null;

			CurrentState.OnEnter(_args, prevType);
			Configuration.OnEnterState(_args, prevType, target);
		}

		private void OnLeaveState(Type target, State<T> previous)
		{
			var prevType = previous != null ? previous.GetType() : null;

			Configuration.OnLeaveState(_args, prevType, target);

			if (previous != null)
			{
				previous.OnLeave(_args, target);
			}
		}

		
	}
}