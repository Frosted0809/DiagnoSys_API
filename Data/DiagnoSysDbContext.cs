using DiagnoSys_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DiagnoSys_API.Data;

public partial class DiagnoSysDbContext : DbContext
{
    public DiagnoSysDbContext()
    {
    }

    public DiagnoSysDbContext(DbContextOptions<DiagnoSysDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cbc> Cbcs { get; set; }

    public virtual DbSet<ClinicPatient> ClinicPatients { get; set; }

    public virtual DbSet<Fecalysi> Fecalyses { get; set; }

    public virtual DbSet<LabTest> LabTests { get; set; }

    public virtual DbSet<LabTestCatalog> LabTestCatalogs { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Urinalysi> Urinalyses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cbc>(entity =>
        {
            entity.HasKey(e => e.CbcId).HasName("PK__cbc__9B1AD3CAEF27CBF4");

            entity.ToTable("cbc");

            entity.HasIndex(e => e.OrderId, "UQ__cbc__46596228C82D270A").IsUnique();

            entity.Property(e => e.CbcId).HasColumnName("cbc_id");
            entity.Property(e => e.Basophils)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("basophils");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Eosinophils)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("eosinophils");
            entity.Property(e => e.Hematocrit)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("hematocrit");
            entity.Property(e => e.Hemoglobin)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("hemoglobin");
            entity.Property(e => e.Lymphocytes)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("lymphocytes");
            entity.Property(e => e.Mch).HasColumnName("mch");
            entity.Property(e => e.Mcv).HasColumnName("mcv");
            entity.Property(e => e.Monocytes)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("monocytes");
            entity.Property(e => e.Neutrophils)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("neutrophils");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Platelets).HasColumnName("platelets");
            entity.Property(e => e.Rbc)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("rbc");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Wbc)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("wbc");

            entity.HasOne(d => d.Order).WithOne(p => p.Cbc)
                .HasForeignKey<Cbc>(d => d.OrderId)
                .HasConstraintName("fk_cbc_order");
        });

        modelBuilder.Entity<ClinicPatient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__clinic_p__4D5CE476E22C2360");

            entity.ToTable("clinic_patients");

            entity.HasIndex(e => e.Contact, "uq_clinic_patients_contact").IsUnique();

            entity.Property(e => e.PatientId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("patient_id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Contact)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.RegisteredAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("registered_at");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sex");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Fecalysi>(entity =>
        {
            entity.HasKey(e => e.FaId).HasName("PK__fecalysi__BD0CA41419877B55");

            entity.ToTable("fecalysis");

            entity.HasIndex(e => e.OrderId, "UQ__fecalysi__46596228FBD19A8F").IsUnique();

            entity.Property(e => e.FaId).HasColumnName("fa_id");
            entity.Property(e => e.Appearance)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("appearance");
            entity.Property(e => e.Bacteria)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("bacteria");
            entity.Property(e => e.Consistency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("consistency");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.OccultBlood)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("occult_blood");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OtherFindings)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("other_findings");
            entity.Property(e => e.ParasiteId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("parasite_id");
            entity.Property(e => e.Rbc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rbc");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Wbc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("wbc");

            entity.HasOne(d => d.Order).WithOne(p => p.Fecalysi)
                .HasForeignKey<Fecalysi>(d => d.OrderId)
                .HasConstraintName("fk_fecalysis_order");
        });

        modelBuilder.Entity<LabTest>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__lab_test__465962298320D67D");

            entity.ToTable("lab_test");

            entity.HasIndex(e => e.PatientId, "idx_lab_test_patient_id");

            entity.HasIndex(e => e.TestId, "idx_lab_test_test_id");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.PatientId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("patient_id");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("PENDING")
                .HasColumnName("status");
            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Patient).WithMany(p => p.LabTests)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("fk_lab_test_patient");

            entity.HasOne(d => d.Test).WithMany(p => p.LabTests)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("fk_lab_test_test");
        });

        modelBuilder.Entity<LabTestCatalog>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__lab_test__F3FF1C0261698CE6");

            entity.ToTable("lab_test_catalog");

            entity.HasIndex(e => e.TestName, "uq_lab_test_name").IsUnique();

            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.TestName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("test_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__payments__ED1FC9EA74C6EEF7");

            entity.ToTable("payments");

            entity.HasIndex(e => e.OrderId, "idx_payments_order");

            entity.HasIndex(e => e.Status, "idx_payments_status");

            entity.HasIndex(e => e.ReceiptNumber, "uq_payments_receipt").IsUnique();

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.AmountPaid)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount_paid");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("CASH")
                .HasColumnName("payment_method");
            entity.Property(e => e.ReceiptNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("receipt_number");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("PAID")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_payments_order");
        });

        modelBuilder.Entity<Urinalysi>(entity =>
        {
            entity.HasKey(e => e.UaId).HasName("PK__urinalys__3065E5979BEDC201");

            entity.ToTable("urinalysis");

            entity.HasIndex(e => e.OrderId, "UQ__urinalys__46596228461B431F").IsUnique();

            entity.Property(e => e.UaId).HasColumnName("ua_id");
            entity.Property(e => e.Appearance)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("appearance");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Glucose)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("glucose");
            entity.Property(e => e.Ketones)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ketones");
            entity.Property(e => e.Nitrites)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nitrites");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OtherFindings)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("other_findings");
            entity.Property(e => e.Ph)
                .HasColumnType("decimal(3, 1)")
                .HasColumnName("ph");
            entity.Property(e => e.Protein)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("protein");
            entity.Property(e => e.SpecificGravity)
                .HasColumnType("decimal(4, 3)")
                .HasColumnName("specific_gravity");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Order).WithOne(p => p.Urinalysi)
                .HasForeignKey<Urinalysi>(d => d.OrderId)
                .HasConstraintName("fk_urinalysis_order");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F2C762CD6");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "uq_users_email").IsUnique();

            entity.HasIndex(e => e.Username, "uq_users_username").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Staff")
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
