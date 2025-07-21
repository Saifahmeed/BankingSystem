using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models;

public partial class BankingContext : DbContext
{
    public BankingContext()
    {
    }

    public BankingContext(DbContextOptions<BankingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<LookAccountStatus> LookAccountStatuses { get; set; }

    public virtual DbSet<LookUserType> LookUserTypes { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }
    public virtual DbSet<Currency> Currencies { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=MiniBank;Trusted_Connection=True;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasOne(d => d.AccountStatus).WithMany(p => p.Accounts).HasConstraintName("FK_Accounts_Look_AccountStatus");

            entity.HasOne(d => d.Branch).WithMany(p => p.Accounts).HasConstraintName("FK_Accounts_Branches");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts).HasConstraintName("FK_Accounts_Users");
            entity.HasOne(d => d.AccountType) .WithMany(p => p.Accounts) .HasForeignKey(d => d.AccountTypeId)
                                    .HasConstraintName("FK_Accounts_AccountType");
            entity.HasIndex(a => new { a.UserId, a.AccountTypeId })
                .IsUnique()
                .HasDatabaseName("UX_User_AccountType");
            entity.HasOne(d => d.Currency)
    .WithMany(p => p.Accounts)
    .HasForeignKey(d => d.CurrencyId)
    .OnDelete(DeleteBehavior.Cascade)
    .IsRequired()
    .HasConstraintName("FK_Accounts_Currency");
        });
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.Property(e => e.BranchId).ValueGeneratedNever();
        });
        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(c => c.CurrencyId);
            entity.Property(c => c.Code).HasMaxLength(10).IsUnicode(false);
            entity.Property(c => c.ExchangeRate).HasColumnType("decimal(20, 4)"); // 20 digits, 4 decimal places
        });
        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(e => e.AccountTypeId);

            entity.Property(e => e.TypeName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.ToTable("AccountType");
        });
        modelBuilder.Entity<LookAccountStatus>(entity =>
        {
            entity.Property(e => e.StatusId).ValueGeneratedNever();
        });

        modelBuilder.Entity<LookUserType>(entity =>
        {
            entity.Property(e => e.TypeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId);
            entity.Property(e => e.TransactionId)
        .UseIdentityColumn();

            entity.HasOne(d => d.ReceiverAccount).WithMany(p => p.TransactionReceiverAccounts).HasConstraintName("FK_Transaction_Accounts1");

            entity.HasOne(d => d.SenderAccount).WithMany(p => p.TransactionSenderAccounts).HasConstraintName("FK_Transaction_Accounts");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasOne(d => d.UserType).WithMany(p => p.Users).HasConstraintName("FK_Users_Look_UserType");
            entity.HasIndex(u => u.Email).IsUnique().HasDatabaseName("UX_User_Email");
            entity.HasIndex(u => u.PhoneNumber).IsUnique().HasDatabaseName("UX_User_Phone");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
