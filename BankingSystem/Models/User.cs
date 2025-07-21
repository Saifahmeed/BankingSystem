using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models;

public partial class User
{
    [Key]
    [Column("User_id")]
    public long UserId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Fname { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Lname { get; set; }

    [Column("UserType_id")]
    public int? UserTypeId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [ForeignKey("UserTypeId")]
    [InverseProperty("Users")]
    public virtual LookUserType? UserType { get; set; }
    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;
    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; set; }
    [MaxLength(255)]
    public string Address { get; set; }
    [Required]
    [Column(TypeName = "VARCHAR(255)")]
    public string Password { get; set; }
}
