namespace Slush.Entity.Profile
{
    public class WalletTransactions
    {
        public WalletTransactions() { }

        public WalletTransactions(Guid id, Guid userId, Guid transactionObj, float currency, DateTime? createdAt) 
        {
            this.id = id;
            this.userId = userId;
            this.transactionObj = transactionObj;
            this.currency = currency;
            this.createdAt = createdAt;
        }

        public Guid id {  get; set; }
        public Guid userId { get; set; }
        public Guid transactionObj {  get; set; }
        public float currency { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
