using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models;

public partial class Branch
{
    [Key]
    [Column("Branch_id")]
    public long BranchId { get; set; }

    [Column("Branch_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? BranchName { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Location { get; set; }

    [InverseProperty("Branch")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
