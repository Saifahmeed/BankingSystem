using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models;

[Table("Look_AccountStatus")]
public partial class LookAccountStatus
{
    [Key]
    [Column("Status_id")]
    public long StatusId { get; set; }

    [Column("Status_name")]
    [StringLength(20)]
    [Unicode(false)]
    public string? StatusName { get; set; }

    [InverseProperty("AccountStatus")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
