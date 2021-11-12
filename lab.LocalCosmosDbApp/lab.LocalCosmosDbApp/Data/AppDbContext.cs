using lab.LocalCosmosDbApp.EntityModels;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;

namespace lab.LocalCosmosDbApp.Data
{
    public partial class AppDbContext : DbContext
    {
        private readonly IConfigurationRoot _iConfigurationRoot;
        private readonly string _databaseName;
        private readonly string _containerName;

        public AppDbContext()
        {
            _iConfigurationRoot = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _databaseName = _iConfigurationRoot.GetValue<string>("AppDbConnectionConfig:DatabaseName");
            _containerName = _iConfigurationRoot.GetValue<string>("AppDbConnectionConfig:ContainerName");
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var accountEndpoint = _iConfigurationRoot.GetValue<string>("AppDbConnectionConfig:EndPointUrl");
                var accountKey = _iConfigurationRoot.GetValue<string>("AppDbConnectionConfig:AuthKey");

                optionsBuilder.UseCosmos(accountEndpoint, accountKey, _databaseName);

                //optionsBuilder.UseCosmos(accountEndpoint, accountKey, _databaseName, options =>
                //{
                //    //options.ConnectionMode(ConnectionMode.Gateway);
                //    //options.WebProxy(new WebProxy());
                //    options.ConnectionMode(ConnectionMode.Direct);
                //    options.LimitToEndpoint();
                //    options.Region(Regions.AustraliaCentral);
                //    options.GatewayModeMaxConnectionLimit(32);
                //    options.MaxRequestsPerTcpConnection(8);
                //    options.MaxTcpConnectionsPerEndpoint(16);
                //    options.IdleTcpConnectionTimeout(TimeSpan.FromMinutes(1));
                //    options.OpenTcpConnectionTimeout(TimeSpan.FromMinutes(1));
                //    options.RequestTimeout(TimeSpan.FromMinutes(1));
                //});
            }
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
                entity.ToContainer(_containerName);
                entity.HasKey(x => x.Id);
                entity.HasPartitionKey(x => x.Id);
            });

            modelBuilder.Entity<ToolInfoApproverSource>(entity =>
            {
                entity.ToContainer(_containerName);
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
        //    modelBuilder.HasDefaultContainer(_containerName);
        //    modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());
        //}

    }

    public static class AppDbContextInitializer
    {
        public static bool CreateDatabaseIfNotExists()
        {
            using (var context = new AppDbContext())
            {
               return context.Database.EnsureCreated();
            }
        }

        public static void MasterData()
        {
            //Create Tables And Insert Master Data
            CreateTableAndInsertMasterData();
        }

        private static void CreateTableAndInsertMasterData()
        {
            // Create an instance and save the entity to the database

            #region Person
            var person1 = new Person() { Id = "c10f6763-1c25-431f-9ceb-e8e3d7c8b88d", PersonName = "Rasel Ahmmed", EmailAddress = "raselahmmed@mail.com", DateOfBirth = new DateTime(1985, 12, 1) };
            var person2 = new Person() { Id = "e66a44e5-0072-40bc-95c6-ef76df6540eb", PersonName = "John Mia", EmailAddress = "johnmia@mail.com", DateOfBirth = new DateTime(1910, 8, 10) };

            using (var context = new AppDbContext())
            {
                context.Add(person1);
                context.Add(person2);
                context.SaveChanges();
            }
            #endregion

            #region Tool Info

            //001
            ToolInfoApproverSource toolInfoApproverSource1 = new ToolInfoApproverSource();
            toolInfoApproverSource1.Id = "4ebfed32-da3c-4269-8c10-a4d373b859df";
            toolInfoApproverSource1.Building = "Sample Building 001";
            toolInfoApproverSource1.BU = "Sample BU 001";
            toolInfoApproverSource1.KPU = "Sample KPU 001";

            ToolProfile toolProfile1 = new ToolProfile();
            toolProfile1.ToolId = "b11dd969-5fcd-4d02-a727-fede48895308";
            toolProfile1.ToolName = "Glacier 001";
            toolProfile1.Bay = "Bay 001";
            toolProfile1.Lab = "Lab 001";
            toolProfile1.Room = "Room 001";
            toolProfile1.Initiator = "Demo Initiator 001";
            toolProfile1.ToolOwner = "John Doe 001";
            toolProfile1.SecondaryContact = "John Mia 001";
            toolProfile1.LabManager = "Lab Manager 001";

            EHSAssignment eHSAssignment1 = new EHSAssignment();
            eHSAssignment1.RegionSite = " RegionSite 001";
            eHSAssignment1.BuildingEnvironmental = " BuildingEnvironmental 001";
            eHSAssignment1.EnvironmentalAdditionalReviewerOne = "Good environment 001";
            eHSAssignment1.EnvironmentalAdditionalReviewerTwo = "Excellent Environment 001";
            eHSAssignment1.OccupationalSafety = "A+ Safety 001";
            eHSAssignment1.ChemAuthFacilities = "Chem is not available 001";
            eHSAssignment1.ProductSafety = "Prodduct Safety Level 5 001";
            eHSAssignment1.AdditionalEHSIH = string.Empty;

            toolInfoApproverSource1.ToolProfile = toolProfile1;
            toolInfoApproverSource1.EHSAssignment = eHSAssignment1;

            //002
            ToolInfoApproverSource toolInfoApproverSource2 = new ToolInfoApproverSource();
            toolInfoApproverSource2.Id = "58908831-6b91-412e-8cb1-1f5ef11069c3";
            toolInfoApproverSource2.Building = "Sample Building 002";
            toolInfoApproverSource2.BU = "Sample BU 002";
            toolInfoApproverSource2.KPU = "Sample KPU 002";

            ToolProfile toolProfile2 = new ToolProfile();
            toolProfile2.ToolId = "3f52b23b-54e4-4560-8c7a-1d6703cac615";
            toolProfile2.ToolName = "Glacier 002";
            toolProfile2.Bay = "Bay 002";
            toolProfile2.Lab = "Lab 002";
            toolProfile2.Room = "Room 002";
            toolProfile2.Initiator = "Demo Initiator 002";
            toolProfile2.ToolOwner = "John Doe 002";
            toolProfile2.SecondaryContact = "John Mia 002";
            toolProfile2.LabManager = "Lab Manager 002";

            EHSAssignment eHSAssignment2 = new EHSAssignment();
            eHSAssignment2.RegionSite = " RegionSite 002";
            eHSAssignment2.BuildingEnvironmental = " BuildingEnvironmental 002";
            eHSAssignment2.EnvironmentalAdditionalReviewerOne = "Good environment 002";
            eHSAssignment2.EnvironmentalAdditionalReviewerTwo = "Excellent Environment 002";
            eHSAssignment2.OccupationalSafety = "A+ Safety 002";
            eHSAssignment2.ChemAuthFacilities = "Chem is not available 002";
            eHSAssignment2.ProductSafety = "Prodduct Safety Level 5 002";
            eHSAssignment2.AdditionalEHSIH = string.Empty;

            toolInfoApproverSource2.ToolProfile = toolProfile2;
            toolInfoApproverSource2.EHSAssignment = eHSAssignment2;

            using (var context = new AppDbContext())
            {
                context.Add(toolInfoApproverSource1);
                context.Add(toolInfoApproverSource2);
                context.SaveChanges();
            }
            #endregion

        }
    }
}
