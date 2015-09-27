using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Finite
{
	public class LinkIterator<TSwtiches> : IEnumerable<ILink<TSwtiches>>
	{
		private readonly StateMachine<TSwtiches> _machine;
		private readonly IEnumerable<ILink<TSwtiches>> _links;

		public LinkIterator(StateMachine<TSwtiches> machine, IEnumerable<ILink<TSwtiches>> links)
		{
			_machine = machine;
			_links = links;
		}

		public IEnumerable<State<TSwtiches>> Active()
		{
			return _links.Where(link => link.IsActive(_machine.Switches)).Select(link => link.Target);
		}

		public IEnumerable<State<TSwtiches>> Inactive()
		{
			return _links.Where(link => link.IsActive(_machine.Switches) == false).Select(link => link.Target);
		}

		public IEnumerator<ILink<TSwtiches>> GetEnumerator()
		{
			return _links.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
