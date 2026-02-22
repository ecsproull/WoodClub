namespace WoodClub
{
	internal class QbInventoryItem
	{
		/// <summary>
		/// Represents an Item from QuickBooks
		/// </summary>
		public string ListID { get; set; }
		public string EditSequence { get; set; }
		public string Name { get; set; }
		public string FullName { get; set; }
		public string Type { get; set; }
		public bool IsActive { get; set; }
		public string Description { get; set; }
		public string Price { get; set; }
		public string Cost { get; set; }
		public string AverageCost { get; set; }
		public string QuantityOnHand { get; set; }
		public string IncomeAccountRef { get; set; }
		public string AssetAccountRef { get; set; }
		public string COGSAccountRef { get; set; }
	}
}
