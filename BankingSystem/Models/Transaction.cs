using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models;

[Table("Transaction")]
public partial class Transaction
{
    [Key]
    [Column("Transaction_id")]
    public long TransactionId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TimeStamp { get; set; }
    [Column("EquivalentRate", TypeName = "decimal(20, 4)")]
    public decimal? EquivalentRate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [Column("Sender_Account_id")]
    public long? SenderAccountId { get; set; }

    [Column("Receiver_Account_id")]
    public long? ReceiverAccountId { get; set; }

    [ForeignKey("ReceiverAccountId")]
    [InverseProperty("TransactionReceiverAccounts")]
    public virtual Account? ReceiverAccount { get; set; }

    [ForeignKey("SenderAccountId")]
    [InverseProperty("TransactionSenderAccounts")]
    public virtual Account? SenderAccount { get; set; }
    [Required]
    [Column("Status")]
    public bool Status { get; set; } 
}
