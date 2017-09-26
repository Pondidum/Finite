using System;

namespace Sample.CustomStateLoading
{
	public class CreditRequest
	{
		public Guid ID { get; set; }

		public decimal Amount { get; set; }
		public string Justification { get; set; }

		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		public DateTime ValidatedOn { get; set; }
		public string ValidatedBy { get; set; }

		public Progress Progress { get; set; }
	}
}
