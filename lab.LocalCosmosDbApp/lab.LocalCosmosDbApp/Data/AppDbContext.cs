using lab.LocalCosmosDbApp.Config;
using lab.LocalCosmosDbApp.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;

namespace lab.LocalCosmosDbApp.Data
{
    public partial class AppDbContext : DbContext
    {
        private readonly AppDbConnectionConfig _appDbConnectionConfig;
        private readonly IConfigurationRoot _iConfigurationRoot;
        private readonly string _databaseName;
        private readonly string _containerName;

        //public AppDbContext()
        //{
        //    _iConfigurationRoot = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        //    _databaseName = _iConfigurationRoot.GetValue<string>("AppDbConnectionConfig:DatabaseName");
        //    _containerName = _iConfigurationRoot.GetValue<string>("AppDbConnectionConfig:ContainerName");
        //}

        //public AppDbContext(DbContextOptions<AppDbContext> options)
        //    : base(options)
        //{
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        var accountEndpoint = _iConfigurationRoot.GetValue<string>("AppDbConnectionConfig:EndPointUrl");
        //        var accountKey = _iConfigurationRoot.GetValue<string>("AppDbConnectionConfig:AuthKey");

        //        optionsBuilder.UseCosmos(accountEndpoint, accountKey, _databaseName);

        //        //optionsBuilder.UseCosmos(accountEndpoint, accountKey, _databaseName, options =>
        //        //{
        //        //    //options.ConnectionMode(ConnectionMode.Gateway);
        //        //    //options.WebProxy(new WebProxy());
        //        //    options.ConnectionMode(ConnectionMode.Direct);
        //        //    options.LimitToEndpoint();
        //        //    options.Region(Regions.AustraliaCentral);
        //        //    options.GatewayModeMaxConnectionLimit(32);
        //        //    options.MaxRequestsPerTcpConnection(8);
        //        //    options.MaxTcpConnectionsPerEndpoint(16);
        //        //    options.IdleTcpConnectionTimeout(TimeSpan.FromMinutes(1));
        //        //    options.OpenTcpConnectionTimeout(TimeSpan.FromMinutes(1));
        //        //    options.RequestTimeout(TimeSpan.FromMinutes(1));
        //        //});
        //    }
        //}

        public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<AppDbConnectionConfig> appDbConnectionConfig)
            : base(options)
        {
            _appDbConnectionConfig = appDbConnectionConfig.Value;
        }

        #region Identity EntityModels
        //public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        //public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        //public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        #endregion Identity EntityModels

        public DbSet<Person> Person { get; set; }

        public DbSet<ToolInfoApproverSource> ToolInfoApproverSource { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*Non mapped database property so column in database table will not create for it*/
            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToContainer(_appDbConnectionConfig.ContainerName);
                entity.HasKey(x => x.Id);
                entity.HasPartitionKey(x => x.Id);
            });

            modelBuilder.Entity<ToolInfoApproverSource>(entity =>
            {
                entity.ToContainer(_appDbConnectionConfig.ContainerName);
                entity.HasKey(x => x.Id);
                entity.HasPartitionKey(x => x.Id);
                entity.OwnsOne(x => x.ToolProfile);
                entity.OwnsOne(x => x.EHSAssignment);

                //entity.OwnsOne(x => x.EHSAssignment).OwnsMany(c => c.ManagementApprovers);
                //entity.OwnsOne(x => x.EHSAssignment).OwnsMany(c => c.BuildingEnvironmentalBackups);
                //entity.OwnsOne(x => x.EHSAssignment).OwnsMany(c => c.OccupationalSafetyBackups);
                //entity.OwnsOne(x => x.EHSAssignment).OwnsMany(c => c.ChemAuthFacilitiesBackups);
                //entity.OwnsOne(x => x.EHSAssignment).OwnsMany(c => c.ProductSafetyBackups);
                //entity.OwnsOne(x => x.EHSAssignment).OwnsMany(c => c.AdditionalEHSIHBackups);
            });

            #region Identity EntityModels
            //modelBuilder.Entity<AspNetRole>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedName] IS NOT NULL)");

            //    entity.Property(e => e.Name).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
            //});

            //modelBuilder.Entity<AspNetRoleClaim>(entity =>
            //{
            //    entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            //    entity.Property(e => e.RoleId).IsRequired();

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetRoleClaims)
            //        .HasForeignKey(d => d.RoleId);
            //});

            //modelBuilder.Entity<AspNetUser>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            //    entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedUserName] IS NOT NULL)");

            //    entity.Property(e => e.Email).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

            //    entity.Property(e => e.UserName).HasMaxLength(256);
            //});

            //modelBuilder.Entity<AspNetUserClaim>(entity =>
            //{
            //    entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            //    entity.Property(e => e.UserId).IsRequired();

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserClaims)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserLogin>(entity =>
            //{
            //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            //    entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            //    entity.Property(e => e.LoginProvider).HasMaxLength(128);

            //    entity.Property(e => e.ProviderKey).HasMaxLength(128);

            //    entity.Property(e => e.UserId).IsRequired();

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserLogins)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserRole>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.RoleId });

            //    entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

            //    entity.Property(e => e.UserId).HasMaxLength(256);

            //    entity.Property(e => e.RoleId).HasMaxLength(256);

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetUserRoles)
            //        .HasForeignKey(d => d.RoleId);

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserRoles)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserToken>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            //    entity.Property(e => e.UserId).HasMaxLength(256);

            //    entity.Property(e => e.LoginProvider).HasMaxLength(128);

            //    entity.Property(e => e.Name).HasMaxLength(128);

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserTokens)
            //        .HasForeignKey(d => d.UserId);
            //});
            #endregion Identity EntityModels

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasDefaultContainer(_appDbConnectionConfig.ContainerName);
        //    modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());
        //}

    }
}
