namespace WoodClub
{
    public class TransactionAddition
    {
        public TransactionAddition(string code, string desc, float amount)
        {
            this.TotalAmount = amount;
            this.EventType = desc;
            this.Code = code;
        }
        public float TotalAmount { get; set; } = 0;
        public string EventType { get; set; }
        public string Code { get; set; }
    }
}
