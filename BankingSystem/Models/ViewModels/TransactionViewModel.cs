using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Models.ViewModels
{
    public class TransactionViewModel
    {
        [Required]
        public long SenderAccountId { get; set; }

        [Required]
        public long ReceiverAccountId { get; set; }

        public decimal? Amount { get; set; }

        public List<SelectListItem> SenderAccounts { get; set; } = new();
        public Dictionary<long, decimal> AccountBalances { get; set; } = new();
    }
}
