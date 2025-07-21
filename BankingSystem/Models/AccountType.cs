using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Models
{

    [Table("AccountType")]
    public partial class AccountType
    {
        [Key]
        [Column("AccountType_id")]
        public int AccountTypeId { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string TypeName { get; set; } = null!;

        [InverseProperty("AccountType")]
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
