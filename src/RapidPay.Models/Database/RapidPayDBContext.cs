using Microsoft.EntityFrameworkCore;
using RapidPay.Models.Database.Entities;

namespace RapidPay.Models.Database;

public partial class RapidPayDBContext : DbContext
{
    public RapidPayDBContext()
    {
    }

    public RapidPayDBContext(DbContextOptions<RapidPayDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cards__3214EC0755C2F399");

            entity.Property(e => e.Balance).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Number)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Cards)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cards__UserId__15702A09");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC0761667DB6");

            entity.Property(e => e.Fee).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ItemPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Card).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__CardId__184C96B4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07DAD901AD");

            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
