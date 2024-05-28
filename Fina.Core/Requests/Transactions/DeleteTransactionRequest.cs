using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Transactions
{
    public class DeleteTransactionRequest : Request
    {
        [Required(ErrorMessage = "ID inválido")]
        public long Id { get; set; }
    }
}
