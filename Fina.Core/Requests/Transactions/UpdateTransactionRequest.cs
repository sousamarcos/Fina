﻿using Fina.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Transactions
{
    public class UpdateTransactionRequest : Request
    {
        [Required(ErrorMessage = "ID inválido")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Títuto inválido")]
        [MaxLength(80, ErrorMessage = "O titudo deve conter até 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo inválido")]
        public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

        [Required(ErrorMessage = "Valor inválido")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Categoria inválida")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Data inválida")]
        public DateTime? PaidOrReceveidAt { get; set; }
    }
}