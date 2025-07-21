using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models;

public partial class Account
{
    [Key]
    [Column("Account_ID")]
    public long AccountId { get; set; }

    [Column("User_ID")]
    public long? UserId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Balance { get; set; }
    

    [Column("AccountStatus_id")]
    public long? AccountStatusId { get; set; }

    [Column("Branch_id")]
    public long? BranchId { get; set; }

    [Column("Date_opened", TypeName = "datetime")]
    public DateTime? DateOpened { get; set; }

    [Column("Date_closed", TypeName = "datetime")]
    public DateTime? DateClosed { get; set; }

    [ForeignKey("AccountStatusId")]
    [InverseProperty("Accounts")]
    public virtual LookAccountStatus? AccountStatus { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Accounts")]
    public virtual Branch? Branch { get; set; }

    [InverseProperty("ReceiverAccount")]
    public virtual ICollection<Transaction> TransactionReceiverAccounts { get; set; } = new List<Transaction>();

    [InverseProperty("SenderAccount")]
    public virtual ICollection<Transaction> TransactionSenderAccounts { get; set; } = new List<Transaction>();

    [ForeignKey("UserId")]
    [InverseProperty("Accounts")]
    public virtual User? User { get; set; }
    [Column("AccountType_id")]
    public int AccountTypeId { get; set; }  

    [ForeignKey("AccountTypeId")]
    [InverseProperty("Accounts")]
    public virtual AccountType? AccountType { get; set; }
    [Column("Currency_id")]
    public int CurrencyId { get; set; }

    [ForeignKey("CurrencyId")]
    [InverseProperty("Accounts")]
    public virtual Currency Currency { get; set; } = null!;
}
