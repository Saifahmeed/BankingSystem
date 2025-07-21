using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models;

[Table("Look_UserType")]
public partial class LookUserType
{
    [Key]
    [Column("TypeID")]
    public int TypeId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? TypeName { get; set; }

    [InverseProperty("UserType")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
