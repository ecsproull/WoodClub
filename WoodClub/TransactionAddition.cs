namespace WoodClub
{
	/// <summary>
	/// Used when adding credits to the Transactions table.
	/// <see cref="MemberEditor"/>
	/// </summary>
	public class TransactionAddition
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TransactionAddition"/> class.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="desc">The desc.</param>
		/// <param name="amount">The amount.</param>
		public TransactionAddition(string code, string desc, float amount)
		{
			this.TotalAmount = amount;
			this.EventType = desc;
			this.Code = code;
		}

		/// <summary>
		/// Gets or sets the total amount.
		/// </summary>
		/// <value>
		/// The total amount.
		/// </value>
		public float TotalAmount { get; set; } = 0;

		/// <summary>
		/// Gets or sets the type of the event.
		/// </summary>
		/// <value>
		/// The type of the event.
		/// </value>
		public string EventType { get; set; }

		/// <summary>
		/// Gets or sets the code.
		/// </summary>
		/// <value>
		/// The code.
		/// </value>
		public string Code { get; set; }
	}
}
