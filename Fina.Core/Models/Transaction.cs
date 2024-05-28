using Fina.Core.Enums;

namespace Fina.Core.Models
{
    internal class Transaction
    {
        public long Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? PaidOrReceivedAt { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public string UserId { get; set; } = String.Empty;
        public ETransactionType Type { get; set; } = ETransactionType.Withdraw;
        public decimal Amount { get; set; }
    }
}
