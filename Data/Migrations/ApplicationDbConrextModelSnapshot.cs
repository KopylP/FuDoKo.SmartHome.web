﻿// <auto-generated />
using System;
using FuDoKo.SmartHome.web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FuDoKo.SmartHome.web.Data.Migrations
{
    [DbContext(typeof(ApplicationDbConrext))]
    partial class ApplicationDbConrextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.Command", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DeviceConfigurationId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("ScriptId");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.HasIndex("DeviceConfigurationId");

                    b.HasIndex("ScriptId");

                    b.ToTable("Commands");
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.ConditionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("Type")
                        .IsUnique();

                    b.ToTable("ConditionTypes");
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.Controller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InstalledDate");

                    b.Property<DateTime>("LastConnection");

                    b.Property<string>("MAC")
                        .HasMaxLength(12);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PublicKey")
                        .IsRequired();

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.HasKey("Id");

                    b.HasIndex("MAC")
                        .IsUnique()
                        .HasFilter("[MAC] IS NOT NULL");

                    b.ToTable("Controllers");
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ControllerId");

                    b.Property<string>("MAC")
                        .HasMaxLength(16);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<int>("Pin")
                        .HasMaxLength(2);

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.HasIndex("ControllerId");

                    b.HasIndex("MAC")
                        .IsUnique()
                        .HasFilter("[MAC] IS NOT NULL");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.DeviceConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DeviceId");

                    b.Property<int>("MeasureId");

                    b.Property<int>("Value")
                        .HasMaxLength(3);

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("MeasureId");

                    b.ToTable("DeviceConfigurations");
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.Measure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MeasureName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("MeasureName")
                        .IsUnique();

                    b.ToTable("Measures");
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.Script", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConditionTypeId")
                        .HasMaxLength(2);

                    b.Property<float>("ConditionValue");

                    b.Property<float>("Delta")
                        .HasMaxLength(4);

                    b.Property<DateTime>("LastModificationDate");

                    b.Property<int>("Priority")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2)
                        .HasDefaultValue(0);

                    b.Property<int>("RepeatTimes");

                    b.Property<int>("SensorId");

                    b.Property<DateTime>("TimeFrom");

                    b.Property<DateTime>("TimeTo");

                    b.Property<bool>("Visible");

                    b.HasKey("Id");

                    b.HasIndex("ConditionTypeId");

                    b.HasIndex("SensorId");

                    b.ToTable("Scripts");
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.Sensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ControllerId");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<int>("Pin")
                        .HasMaxLength(2);

                    b.Property<int>("SensorTypeId");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int>("Value")
                        .HasMaxLength(3);

                    b.HasKey("Id");

                    b.HasIndex("ControllerId");

                    b.HasIndex("SensorTypeId");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.SensorType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(120);

                    b.HasKey("Id");

                    b.HasIndex("TypeName")
                        .IsUnique();

                    b.ToTable("SensorTypes");
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.UserHasController", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ControllerId");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ControllerId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersHaveControllers");
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.UserHasDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DeviceId");

                    b.Property<int>("UsersHaveControllerId");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("UsersHaveControllerId");

                    b.ToTable("UsersHaveDevices");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.Command", b =>
                {
                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.DeviceConfiguration", "DeviceConfiguration")
                        .WithMany("Commands")
                        .HasForeignKey("DeviceConfigurationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.Script", "Script")
                        .WithMany("Commands")
                        .HasForeignKey("ScriptId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.Device", b =>
                {
                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.Controller", "Controller")
                        .WithMany()
                        .HasForeignKey("ControllerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.DeviceConfiguration", b =>
                {
                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.Device", "Device")
                        .WithMany("DeviceConfigurations")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.Measure", "Measure")
                        .WithMany("DeviceConfigurations")
                        .HasForeignKey("MeasureId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.Script", b =>
                {
                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.ConditionType", "ConditionType")
                        .WithMany("Scripts")
                        .HasForeignKey("ConditionTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.Sensor", "Sensor")
                        .WithMany("Scripts")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.Sensor", b =>
                {
                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.Controller", "Controller")
                        .WithMany("Sensors")
                        .HasForeignKey("ControllerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.SensorType", "SensorType")
                        .WithMany("Sensors")
                        .HasForeignKey("SensorTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.UserHasController", b =>
                {
                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.Controller", "Controller")
                        .WithMany("UsersHaveController")
                        .HasForeignKey("ControllerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.ApplicationUser", "User")
                        .WithMany("UserHasControllers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FuDoKo.SmartHome.web.Data.Models.UserHasDevice", b =>
                {
                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.Device", "Device")
                        .WithMany("UsersHaveDevice")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.UserHasController", "UserHasController")
                        .WithMany("UsersHaveDevices")
                        .HasForeignKey("UsersHaveControllerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FuDoKo.SmartHome.web.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
