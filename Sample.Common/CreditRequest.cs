using System;
using Finite;

namespace Sample.Common
{
	public class CreditRequest
	{
		public decimal Amount { get; set; }
		public string Justification { get; set; }

		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		public DateTime ValidatedOn { get; set; }
		public string ValidatedBy { get; set; }

		public Type State { get; set; }
	}
}
