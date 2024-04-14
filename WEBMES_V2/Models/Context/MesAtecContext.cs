using Microsoft.EntityFrameworkCore;
using WEBMES_V2.Models.DomainModels.PlasmaMagazine;

namespace WEBMES_V2.Models.Context;

public partial class MesAtecContext : DbContext
{
    public MesAtecContext()
    {
    }

    public MesAtecContext(DbContextOptions<MesAtecContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MsMagazineStatus> MsMagazineStatuses { get; set; }

    public virtual DbSet<MsStationMagazine> MsStationMagazines { get; set; }

    public virtual DbSet<TrnLotMagazine> TrnLotMagazines { get; set; }

    public virtual DbSet<TrnMagazineDetail> TrnMagazineDetails { get; set; }

    public virtual DbSet<TrnMagazineDetailsHistory> TrnMagazineDetailsHistories { get; set; }

    public virtual DbSet<PsEquipment> PsEquipments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MsMagazineStatus>(entity =>
        {
            entity.ToTable("MS_Magazine_Status");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MsStationMagazine>(entity =>
        {
            entity.ToTable("MS_Station_Magazine");

            entity.Property(e => e.MagazineCode).HasMaxLength(255);
            entity.Property(e => e.MagazineQty).HasColumnName("MagazineQTY");
            entity.Property(e => e.TimeCreated).HasColumnType("datetime");
        });


        modelBuilder.Entity<TrnLotMagazine>(entity =>
        {
            entity.ToTable("TRN_Lot_Magazine");

            entity.Property(e => e.DateTimeStarted).HasColumnType("datetime");
            entity.Property(e => e.Lot).HasMaxLength(255);
            entity.Property(e => e.LotQty).HasColumnName("LotQTY");
            entity.Property(e => e.MachineCode).HasMaxLength(255);
            entity.Property(e => e.StatusRemarks).HasMaxLength(255);
        });

        modelBuilder.Entity<TrnMagazineDetail>(entity =>
        {
            entity.ToTable("TRN_MagazineDetails");

            entity.Property(e => e.CurrentScannedQty).HasColumnName("Current_Scanned_QTY");
            entity.Property(e => e.DateTimeScanned).HasColumnType("datetime");
            entity.Property(e => e.MagazineCode).HasMaxLength(255);
            entity.Property(e => e.MagazineQty).HasColumnName("MagazineQTY");
            entity.Property(e => e.ScannedBy).HasColumnName("Scanned_By");
            entity.Property(e => e.TrnLotMagazineId).HasColumnName("TRN_Lot_Magazine_Id");
        });

        modelBuilder.Entity<TrnMagazineDetailsHistory>(entity =>
        {
            entity.ToTable("Trn_MagazineDetails_History");

            entity.Property(e => e.CurrentScannedQty).HasColumnName("Current_Scanned_QTY");
            entity.Property(e => e.DateTimeScanned).HasColumnType("datetime");
            entity.Property(e => e.MagazineCode).HasMaxLength(255);
            entity.Property(e => e.MagazineQty).HasColumnName("MagazineQTY");
            entity.Property(e => e.ScannedBy).HasColumnName("Scanned_By");
            entity.Property(e => e.TrnLotMagazineId).HasColumnName("TRN_Lot_Magazine_Id");
        });

        modelBuilder.Entity<PsEquipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentCode);

            entity.ToTable("PS_Equipment");

            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EquipmentDescription).HasMaxLength(50);
            entity.Property(e => e.EquipmentId)
                .HasMaxLength(50)
                .HasColumnName("EquipmentID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
