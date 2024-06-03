namespace Slush.Models.Profile
{
    public class WalletTransactionsModel
    {
        public Guid id { get; set; }
        public Guid userId { get; set; }
        public Guid transactionObj { get; set; }
        public float currency { get; set; }

        public DateTime? createdAt { get; set; }
    }
}
