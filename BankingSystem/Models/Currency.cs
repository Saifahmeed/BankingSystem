using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace BankingSystem.Models;
[Table("Look_Currency")]
public class Currency
{
    [Key]
    [Column("Currency_id")]
    public int CurrencyId { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string Code { get; set; } = null!;  
    [Required]
    public decimal ExchangeRate { get; set; }

    [InverseProperty("Currency")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
