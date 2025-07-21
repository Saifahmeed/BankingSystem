using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Models.ViewModels
{
    public class AccountViewModel
    {
        [Required(ErrorMessage = "Please select a branch.")]
        [Display(Name = "Branch")]
        public long BranchId { get; set; }

        [Required(ErrorMessage = "Please select an account type.")]
        [Display(Name = "Account Type")]
        public int AccountTypeId { get; set; }

        [Required(ErrorMessage = "Please select a currency.")]
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        public List<SelectListItem> AccountTypes { get; set; } = new();
        public List<SelectListItem> Branches { get; set; } = new();
        public List<SelectListItem> Currencies { get; set; } = new();
    }
}
