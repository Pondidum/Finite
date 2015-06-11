using System;
using System.Collections.Generic;
using System.Linq;

namespace Finite.StateProviders
{
	/// <summary>
	/// Finds all Types in the assembly which inherit <see cref="State&lt;TSwitches&gt;"/>
	/// </summary>
	/// <typeparam name="TSwitches"></typeparam>
	public class ScanningStateProvider<TSwitches> : IStateProvider<TSwitches>
	{
		private readonly Func<Type, State<TSwitches>> _create;
		private readonly List<Type> _types;

		public ScanningStateProvider() : this(Create)
		{
		}

		public ScanningStateProvider(Func<Type, State<TSwitches>> create)
		{
			_create = create;
			_types = typeof(TSwitches)
				.Assembly
				.GetTypes()
				.Where(t => t.IsAbstract == false)
				.Where(t => typeof(State<TSwitches>).IsAssignableFrom(t))
				.ToList();
		}

		public IEnumerable<Type> KnownTypes {  get { return _types; }}

		public IEnumerable<State<TSwitches>> Execute()
		{
			return _types
				.Select(_create)
				.ToList();
		}

		private static State<TSwitches> Create(Type type)
		{
			var ctor = type.GetConstructor(Type.EmptyTypes);

			if (ctor == null)
			{
				throw new MissingMethodException(type.Name, "ctor");
			}

			return (State<TSwitches>)ctor.Invoke(null);
		}
	}
}
