using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodClub
{
	internal class CustomerData
	{
		public string ListID { get; set; }
		public string EditSequence { get; set; }
		public string FullName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Balance { get; set; }
		public string PaidDate { get; set; }
		public string AccountNumber { get; set; }
		public string CustomerType { get; set; }
		public string Locker { get; set; }
		public string LockerWc { get; set; }
		public string AccountNumberWc { get; set; }
		public string EmailMissMatch { get; set; }
		public string PhoneMissMatch { get; set; }
        // List of open invoices for this customer (if requested)
        public List<InvoiceData> OpenInvoices { get; set; } = new List<InvoiceData>();
	}
}
