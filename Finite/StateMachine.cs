using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Finite
{
	public class StateMachine<T>
	{
		private State<T> _currentState;
		private readonly Dictionary<Type, State<T>> _states;
		private T _args;

		public StateMachine()
		{
			_states = new Dictionary<Type, State<T>>();
		}

		public Type CurrentState
		{
			get { return _currentState.GetType(); }
		}

		public void BindTo(T args)
		{
			_args = args;
		}

		public void InitialiseFromThis()
		{
			InitialiseFrom(Assembly.GetExecutingAssembly());
		}

		private void InitialiseFrom(Assembly assembly)
		{
			var types = assembly.GetTypes()
								.Where(t => t.IsAbstract == false)
								.Where(t => typeof(State<T>).IsAssignableFrom(t))
								.ToList();

			InitialiseFrom(types);
		}

		public void InitialiseFrom(IEnumerable<Type> types)
		{
			var constuctorMap = types.ToDictionary(t => t, t => t.GetConstructor(new Type[] { }));

			var instances = constuctorMap.Where(c => c.Value != null)
										 .ToDictionary(p => p.Key, p => CreateInstance(p.Value))
										 .Where(p => p.Value != null)
										 .ToDictionary(p => p.Key, p => p.Value);

			var errors = new List<String>();
			errors.AddRange(constuctorMap.Where(c => c.Value == null)
										 .Select(c => string.Format("Unable to find a default constructor for {0}.", c.Key.Name)));

			errors.AddRange(instances.Select(p => p.Value) 
									 .OfType<Exception>()
									 .Select(e => e.Message));

			_states.Clear();

			foreach (var pair in instances.Where(p => p.Value is State<T>))
			{
				_states.Add(pair.Key, (State<T>)pair.Value);
			}
		}

		private Object CreateInstance(ConstructorInfo constructor)
		{
			try
			{
				return constructor.Invoke(null);
			}
			catch (Exception ex)
			{
				return ex;
			}
		}

		public void SetStateTo(State<T> state)
		{
			SetStateTo(state.GetType());
		}

		public void SetStateTo<TTarget>() where TTarget : State<T>
		{
			SetStateTo(typeof (TTarget));
		}

		private void SetStateTo(Type target)
		{
			if (_currentState != null && GetActiveTargetStates().Contains(target) == false)
			{
				throw new InvalidTransitionException(_currentState.GetType(), target);
			}

			var previous = _currentState;

			if (previous != null)
			{
				previous.OnExit(target);
			}

			_currentState = _states[target];

			_currentState.OnEnter(previous != null ? previous.GetType() : null);

		}

		public IEnumerable<Type> GetAllTargetStates()
		{
			return _currentState.Links.Select(l => l.Target);
		}

		public IEnumerable<Type> GetActiveTargetStates()
		{
			return _currentState.Links.Where(l => l.IsActive(_args)).Select(l => l.Target);
		}
	}
}