using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AdvantageWeb.Models
{
    public partial class StaticdbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public StaticdbContext(DbContextOptions<StaticdbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<AuthGroup> AuthGroup { get; set; }
        public virtual DbSet<AuthGroupPermissions> AuthGroupPermissions { get; set; }
        public virtual DbSet<AuthPermission> AuthPermission { get; set; }
        public virtual DbSet<AuthUser> AuthUser { get; set; }
        public virtual DbSet<AuthUserGroups> AuthUserGroups { get; set; }
        public virtual DbSet<AuthUserUserPermissions> AuthUserUserPermissions { get; set; }
        public virtual DbSet<DjangoAdminLog> DjangoAdminLog { get; set; }
        public virtual DbSet<DjangoContentType> DjangoContentType { get; set; }
        public virtual DbSet<DjangoMigrations> DjangoMigrations { get; set; }
        public virtual DbSet<DjangoSession> DjangoSession { get; set; }
        public virtual DbSet<PacingClient> PacingClient { get; set; }
        public virtual DbSet<PacingClientdivisionproduct> PacingClientdivisionproduct { get; set; }
        public virtual DbSet<PacingDropdownchoices> PacingDropdownchoices { get; set; }
        public virtual DbSet<PacingEntry> PacingEntry { get; set; }
        public virtual DbSet<PacingVendor> PacingVendor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_configuration["TMConnection"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthGroup>(entity =>
            {
                entity.ToTable("auth_group");

                entity.HasIndex(e => e.Name)
                    .HasDatabaseName("auth_group_name_a6ea08ec_like")
                    .HasOperators(new[] { "varchar_pattern_ops" });

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<AuthGroupPermissions>(entity =>
            {
                entity.ToTable("auth_group_permissions");

                entity.HasIndex(e => e.GroupId)
                    .HasDatabaseName("auth_group_permissions_group_id_b120cbf9");

                entity.HasIndex(e => e.PermissionId)
                    .HasDatabaseName("auth_group_permissions_permission_id_84c5c92e");

                entity.HasIndex(e => new { e.GroupId, e.PermissionId })
                    .HasDatabaseName("auth_group_permissions_group_id_permission_id_0cd325b0_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AuthGroupPermissions)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_group_permissions_group_id_b120cbf9_fk_auth_group_id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.AuthGroupPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_group_permissio_permission_id_84c5c92e_fk_auth_perm");
            });

            modelBuilder.Entity<AuthPermission>(entity =>
            {
                entity.ToTable("auth_permission");

                entity.HasIndex(e => e.ContentTypeId)
                    .HasDatabaseName("auth_permission_content_type_id_2f476e4b");

                entity.HasIndex(e => new { e.ContentTypeId, e.Codename })
                    .HasDatabaseName("auth_permission_content_type_id_codename_01ab375a_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codename)
                    .IsRequired()
                    .HasColumnName("codename")
                    .HasMaxLength(100);

                entity.Property(e => e.ContentTypeId).HasColumnName("content_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.AuthPermission)
                    .HasForeignKey(d => d.ContentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_permission_content_type_id_2f476e4b_fk_django_co");
            });

            modelBuilder.Entity<AuthUser>(entity =>
            {
                entity.ToTable("auth_user");

                entity.HasIndex(e => e.Username)
                    .HasDatabaseName("auth_user_username_6821ab7c_like")
                    .HasOperators(new[] { "varchar_pattern_ops" });

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateJoined)
                    .HasColumnName("date_joined")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(254);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(30);

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsStaff).HasColumnName("is_staff");

                entity.Property(e => e.IsSuperuser).HasColumnName("is_superuser");

                entity.Property(e => e.LastLogin)
                    .HasColumnName("last_login")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(150);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(128);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<AuthUserGroups>(entity =>
            {
                entity.ToTable("auth_user_groups");

                entity.HasIndex(e => e.GroupId)
                    .HasDatabaseName("auth_user_groups_group_id_97559544");

                entity.HasIndex(e => e.UserId)
                    .HasDatabaseName("auth_user_groups_user_id_6a12ed8b");

                entity.HasIndex(e => new { e.UserId, e.GroupId })
                    .HasDatabaseName("auth_user_groups_user_id_group_id_94350c0c_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AuthUserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_user_groups_group_id_97559544_fk_auth_group_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuthUserGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_user_groups_user_id_6a12ed8b_fk_auth_user_id");
            });

            modelBuilder.Entity<AuthUserUserPermissions>(entity =>
            {
                entity.ToTable("auth_user_user_permissions");

                entity.HasIndex(e => e.PermissionId)
                    .HasDatabaseName("auth_user_user_permissions_permission_id_1fbb5f2c");

                entity.HasIndex(e => e.UserId)
                    .HasDatabaseName("auth_user_user_permissions_user_id_a95ead1b");

                entity.HasIndex(e => new { e.UserId, e.PermissionId })
                    .HasDatabaseName("auth_user_user_permissions_user_id_permission_id_14a6b632_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.AuthUserUserPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_user_user_permi_permission_id_1fbb5f2c_fk_auth_perm");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuthUserUserPermissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_user_user_permissions_user_id_a95ead1b_fk_auth_user_id");
            });

            modelBuilder.Entity<DjangoAdminLog>(entity =>
            {
                entity.ToTable("django_admin_log");

                entity.HasIndex(e => e.ContentTypeId)
                    .HasDatabaseName("django_admin_log_content_type_id_c4bce8eb");

                entity.HasIndex(e => e.UserId)
                    .HasDatabaseName("django_admin_log_user_id_c564eba6");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionFlag).HasColumnName("action_flag");

                entity.Property(e => e.ActionTime)
                    .HasColumnName("action_time")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.ChangeMessage)
                    .IsRequired()
                    .HasColumnName("change_message");

                entity.Property(e => e.ContentTypeId).HasColumnName("content_type_id");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.ObjectRepr)
                    .IsRequired()
                    .HasColumnName("object_repr")
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.DjangoAdminLog)
                    .HasForeignKey(d => d.ContentTypeId)
                    .HasConstraintName("django_admin_log_content_type_id_c4bce8eb_fk_django_co");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DjangoAdminLog)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("django_admin_log_user_id_c564eba6_fk_auth_user_id");
            });

            modelBuilder.Entity<DjangoContentType>(entity =>
            {
                entity.ToTable("django_content_type");

                entity.HasIndex(e => new { e.AppLabel, e.Model })
                    .HasDatabaseName("django_content_type_app_label_model_76bd3d3b_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppLabel)
                    .IsRequired()
                    .HasColumnName("app_label")
                    .HasMaxLength(100);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasColumnName("model")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<DjangoMigrations>(entity =>
            {
                entity.ToTable("django_migrations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.App)
                    .IsRequired()
                    .HasColumnName("app")
                    .HasMaxLength(255);

                entity.Property(e => e.Applied)
                    .HasColumnName("applied")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<DjangoSession>(entity =>
            {
                entity.HasKey(e => e.SessionKey)
                    .HasName("django_session_pkey");

                entity.ToTable("django_session");

                entity.HasIndex(e => e.ExpireDate)
                    .HasDatabaseName("django_session_expire_date_a5c62663");

                entity.HasIndex(e => e.SessionKey)
                    .HasDatabaseName("django_session_session_key_c0390e0f_like")
                    .HasOperators(new[] { "varchar_pattern_ops" });

                entity.Property(e => e.SessionKey)
                    .HasColumnName("session_key")
                    .HasMaxLength(40);

                entity.Property(e => e.ExpireDate)
                    .HasColumnName("expire_date")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.SessionData)
                    .IsRequired()
                    .HasColumnName("session_data");
            });

            modelBuilder.Entity<PacingClient>(entity =>
            {
                entity.HasKey(e => e.Client)
                    .HasName("pacing_client_pkey");

                entity.ToTable("pacing_client");

                entity.HasIndex(e => e.Client)
                    .HasDatabaseName("pacing_client_client_bf94f004_like")
                    .HasOperators(new[] { "varchar_pattern_ops" });

                entity.Property(e => e.Client)
                    .HasColumnName("client")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<PacingClientdivisionproduct>(entity =>
            {
                entity.ToTable("pacing_clientdivisionproduct");

                entity.HasIndex(e => e.Client)
                    .HasDatabaseName("pacing_clientdivisionproduct_client_10fad1b1_like")
                    .HasOperators(new[] { "varchar_pattern_ops" });

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Client)
                    .IsRequired()
                    .HasColumnName("client")
                    .HasMaxLength(1000);

                entity.Property(e => e.ClientDivisionProduct)
                    .IsRequired()
                    .HasColumnName("client_division_product")
                    .HasMaxLength(1000);

                entity.HasOne(d => d.ClientNavigation)
                    .WithMany(p => p.PacingClientdivisionproduct)
                    .HasForeignKey(d => d.Client)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pacing_clientdivisio_client_10fad1b1_fk_pacing_cl");
            });

            modelBuilder.Entity<PacingDropdownchoices>(entity =>
            {
                entity.ToTable("pacing_dropdownchoices");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Choices)
                    .IsRequired()
                    .HasColumnName("choices")
                    .HasMaxLength(500);

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasColumnName("field_name")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<PacingEntry>(entity =>
            {
                entity.ToTable("pacing_entry");

                entity.HasIndex(e => e.Client)
                    .HasDatabaseName("pacing_entry_client_2b1f8683_like")
                    .HasOperators(new[] { "varchar_pattern_ops" });

                entity.HasIndex(e => e.UserId)
                    .HasDatabaseName("pacing_entry_user_id_a578d101");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdFormat)
                    .HasColumnName("ad_format")
                    .HasMaxLength(500);

                entity.Property(e => e.CampaignDescription1)
                    .HasColumnName("campaign_description1")
                    .HasMaxLength(500);

                entity.Property(e => e.CampaignDescription2)
                    .HasColumnName("campaign_description2")
                    .HasMaxLength(500);

                entity.Property(e => e.CampaignIo)
                    .IsRequired()
                    .HasColumnName("campaign_IO")
                    .HasMaxLength(50);

                entity.Property(e => e.CampaignName)
                    .IsRequired()
                    .HasColumnName("campaign_name")
                    .HasMaxLength(1000);

                entity.Property(e => e.Channel)
                    .IsRequired()
                    .HasColumnName("channel")
                    .HasMaxLength(255);

                entity.Property(e => e.Client)
                    .IsRequired()
                    .HasColumnName("client")
                    .HasMaxLength(1000);

                entity.Property(e => e.ClientDivisionProduct)
                    .IsRequired()
                    .HasColumnName("client_division_product")
                    .HasMaxLength(1000);

                entity.Property(e => e.DailyBudgetEven)
                    .IsRequired()
                    .HasColumnName("daily_budget_even")
                    .HasMaxLength(200);

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("date");

                entity.Property(e => e.EntryName)
                    .IsRequired()
                    .HasColumnName("entry_name")
                    .HasMaxLength(1000);

                entity.Property(e => e.Flight10End)
                    .HasColumnName("flight10_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight10NetBudget)
                    .HasColumnName("flight10_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight10PlannedImpressions).HasColumnName("flight10_planned_impressions");

                entity.Property(e => e.Flight10Start)
                    .HasColumnName("flight10_start")
                    .HasColumnType("date");

                entity.Property(e => e.Flight11End)
                    .HasColumnName("flight11_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight11NetBudget)
                    .HasColumnName("flight11_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight11PlannedImpressions).HasColumnName("flight11_planned_impressions");

                entity.Property(e => e.Flight11Start)
                    .HasColumnName("flight11_start")
                    .HasColumnType("date");

                entity.Property(e => e.Flight12End)
                    .HasColumnName("flight12_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight12NetBudget)
                    .HasColumnName("flight12_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight12PlannedImpressions).HasColumnName("flight12_planned_impressions");

                entity.Property(e => e.Flight12Start)
                    .HasColumnName("flight12_start")
                    .HasColumnType("date");

                entity.Property(e => e.Flight13End)
                    .HasColumnName("flight13_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight13NetBudget)
                    .HasColumnName("flight13_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight13PlannedImpressions).HasColumnName("flight13_planned_impressions");

                entity.Property(e => e.Flight13Start)
                    .HasColumnName("flight13_start")
                    .HasColumnType("date");

                entity.Property(e => e.Flight14End)
                    .HasColumnName("flight14_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight14NetBudget)
                    .HasColumnName("flight14_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight14PlannedImpressions).HasColumnName("flight14_planned_impressions");

                entity.Property(e => e.Flight14Start)
                    .HasColumnName("flight14_start")
                    .HasColumnType("date");

                entity.Property(e => e.Flight15End)
                    .HasColumnName("flight15_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight15NetBudget)
                    .HasColumnName("flight15_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight15PlannedImpressions).HasColumnName("flight15_planned_impressions");

                entity.Property(e => e.Flight15Start)
                    .HasColumnName("flight15_start")
                    .HasColumnType("date");

                entity.Property(e => e.Flight1End)
                    .HasColumnName("flight1_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight1NetBudget)
                    .HasColumnName("flight1_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight1PlannedImpressions).HasColumnName("flight1_planned_impressions");

                entity.Property(e => e.Flight1Start)
                    .HasColumnName("flight1_start")
                    .HasColumnType("date");

                entity.Property(e => e.Flight2End)
                    .HasColumnName("flight2_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight2NetBudget)
                    .HasColumnName("flight2_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight2PlannedImpressions).HasColumnName("flight2_planned_impressions");

                entity.Property(e => e.Flight2Start)
                    .HasColumnName("flight2_start")
                    .HasColumnType("date");

                entity.Property(e => e.Flight3End)
                    .HasColumnName("flight3_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight3NetBudget)
                    .HasColumnName("flight3_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight3PlannedImpressions).HasColumnName("flight3_planned_impressions");

                entity.Property(e => e.Flight3Start)
                    .HasColumnName("flight3_start")
                    .HasColumnType("date");

                entity.Property(e => e.Flight4End)
                    .HasColumnName("flight4_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight4NetBudget)
                    .HasColumnName("flight4_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight4PlannedImpressions).HasColumnName("flight4_planned_impressions");

                entity.Property(e => e.Flight4Start)
                    .HasColumnName("flight4_start")
                    .HasColumnType("date");

                entity.Property(e => e.Flight5End)
                    .HasColumnName("flight5_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight5NetBudget)
                    .HasColumnName("flight5_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight5PlannedImpressions).HasColumnName("flight5_planned_impressions");

                entity.Property(e => e.Flight5Start)
                    .HasColumnName("flight5_start")
                    .HasColumnType("date");

                entity.Property(e => e.Flight6End)
                    .HasColumnName("flight6_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight6NetBudget)
                    .HasColumnName("flight6_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight6PlannedImpressions).HasColumnName("flight6_planned_impressions");

                entity.Property(e => e.Flight6Start)
                    .HasColumnName("flight6_start")
                    .HasColumnType("date");

                entity.Property(e => e.Flight7End)
                    .HasColumnName("flight7_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight7NetBudget)
                    .HasColumnName("flight7_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight7PlannedImpressions).HasColumnName("flight7_planned_impressions");

                entity.Property(e => e.Flight7Start)
                    .HasColumnName("flight7_start")
                    .HasColumnType("date");

                entity.Property(e => e.Flight8End)
                    .HasColumnName("flight8_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight8NetBudget)
                    .HasColumnName("flight8_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight8PlannedImpressions).HasColumnName("flight8_planned_impressions");

                entity.Property(e => e.Flight8Start)
                    .HasColumnName("flight8_start")
                    .HasColumnType("date");

                entity.Property(e => e.Flight9End)
                    .HasColumnName("flight9_end")
                    .HasColumnType("date");

                entity.Property(e => e.Flight9NetBudget)
                    .HasColumnName("flight9_net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Flight9PlannedImpressions).HasColumnName("flight9_planned_impressions");

                entity.Property(e => e.Flight9Start)
                    .HasColumnName("flight9_start")
                    .HasColumnType("date");

                entity.Property(e => e.Goal)
                    .IsRequired()
                    .HasColumnName("goal")
                    .HasMaxLength(255);

                entity.Property(e => e.Identifier)
                    .IsRequired()
                    .HasColumnName("identifier")
                    .HasMaxLength(100);

                entity.Property(e => e.IoCpm)
                    .HasColumnName("IO_CPM")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.MonthlyBudgetRollOver)
                    .IsRequired()
                    .HasColumnName("monthly_budget_roll_over")
                    .HasMaxLength(200);

                entity.Property(e => e.NetBudget)
                    .HasColumnName("net_budget")
                    .HasColumnType("numeric(14,2)");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(2500);

                entity.Property(e => e.PackageDescription)
                    .HasColumnName("package_description")
                    .HasMaxLength(500);

                entity.Property(e => e.PackageName)
                    .IsRequired()
                    .HasColumnName("package_name")
                    .HasMaxLength(1000);

                entity.Property(e => e.PlannedImpressions).HasColumnName("planned_impressions");

                entity.Property(e => e.Platform)
                    .IsRequired()
                    .HasColumnName("platform")
                    .HasMaxLength(1000);

                entity.Property(e => e.PlatformCommission)
                    .HasColumnName("platform_commission")
                    .HasColumnType("numeric(14,6)");

                entity.Property(e => e.PlatformLink)
                    .HasColumnName("platform_link")
                    .HasMaxLength(2000);

                entity.Property(e => e.PrimaryKpi)
                    .IsRequired()
                    .HasColumnName("primary_KPI")
                    .HasMaxLength(255);

                entity.Property(e => e.PrimaryKpiValue)
                    .IsRequired()
                    .HasColumnName("primary_KPI_value")
                    .HasMaxLength(50);

                entity.Property(e => e.ProjectManager)
                    .HasColumnName("project_manager")
                    .HasMaxLength(255);

                entity.Property(e => e.SecondaryKpi)
                    .HasColumnName("secondary_KPI")
                    .HasMaxLength(255);

                entity.Property(e => e.SecondaryKpiValue)
                    .HasColumnName("secondary_KPI_value")
                    .HasMaxLength(50);

                entity.Property(e => e.Specialist)
                    .IsRequired()
                    .HasColumnName("specialist")
                    .HasMaxLength(255);

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.Property(e => e.Strategist1)
                    .IsRequired()
                    .HasColumnName("strategist1")
                    .HasMaxLength(255);

                entity.Property(e => e.Strategist2)
                    .HasColumnName("strategist2")
                    .HasMaxLength(255);

                entity.Property(e => e.Tactic)
                    .HasColumnName("tactic")
                    .HasMaxLength(500);

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("date");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Vendor)
                    .HasColumnName("vendor")
                    .HasMaxLength(500);

                entity.Property(e => e.VerticalSubvertical)
                    .IsRequired()
                    .HasColumnName("vertical_subvertical")
                    .HasMaxLength(255);

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasColumnName("year")
                    .HasMaxLength(100);

                entity.HasOne(d => d.ClientNavigation)
                    .WithMany(p => p.PacingEntry)
                    .HasForeignKey(d => d.Client)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pacing_entry_client_2b1f8683_fk_pacing_client_client");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PacingEntry)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("pacing_entry_user_id_a578d101_fk_auth_user_id");
            });

            modelBuilder.Entity<PacingVendor>(entity =>
            {
                entity.HasKey(e => e.Vendor)
                    .HasName("pacing_vendor_pkey");

                entity.ToTable("pacing_vendor");

                entity.HasIndex(e => e.Vendor)
                    .HasDatabaseName("pacing_vendor_vendor_eb69b3c4_like")
                    .HasOperators(new[] { "varchar_pattern_ops" });

                entity.Property(e => e.Vendor)
                    .HasColumnName("vendor")
                    .HasMaxLength(1000);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
