using FuDoKo.SmartHome.web.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data
{
    public class ApplicationDbConrext: IdentityDbContext<ApplicationUser>
    {
        #region props
        public DbSet<Controller> Controllers { get; set; }
        public DbSet<UserHasController> UserHasControllers { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorType> SensorTypes { get; set; }
        #endregion

        #region constructor
        public ApplicationDbConrext(DbContextOptions options) : base(options) { }
        #endregion

        #region methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");
            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.Status)
                .HasDefaultValue(true);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(p => p.UserHasControllers)
                .WithOne(p => p.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserHasController>()
                .ToTable("UsersHaveControllers");
            modelBuilder.Entity<UserHasController>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<UserHasController>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<UserHasController>()
                .HasOne(p => p.User)
                .WithMany(p => p.UserHasControllers)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserHasController>()
                .HasOne(p => p.Controller)
                .WithMany(p => p.UsersHaveController)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserHasController>()
                .HasMany(p => p.UsersHaveDevices)
                .WithOne(p => p.UserHasController)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Controller>()
                .ToTable("Controllers");
            modelBuilder.Entity<Controller>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Controller>()
                .Property(p => p.Status)
                .HasDefaultValue(true);
            modelBuilder.Entity<Controller>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Controller>()
                .HasMany(p => p.UsersHaveController)
                .WithOne(p => p.Controller)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Controller>()
                .HasMany(p => p.Sensors)
                .WithOne(p => p.Controller)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Controller>()
                .HasIndex(p => p.MAC)
                .IsUnique();

            var sensor = modelBuilder.Entity<Sensor>();
            sensor.ToTable("Sensors");
            sensor.HasKey(p => p.Id);
            sensor.Property(p => p.Status)
                .HasDefaultValue(true);
            sensor.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            sensor.HasOne(p => p.Controller)
                .WithMany(p => p.Sensors)
                .OnDelete(DeleteBehavior.Cascade);
            sensor.HasOne(p => p.SensorType)
                .WithMany(p => p.Sensors)
                .OnDelete(DeleteBehavior.Cascade);

            var sensorType = modelBuilder.Entity<SensorType>();
            sensorType.ToTable("SensorTypes");
            sensorType.HasKey(p => p.Id);
            sensorType.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            sensorType.HasMany(p => p.Sensors)
                .WithOne(p => p.SensorType)
                .OnDelete(DeleteBehavior.Cascade);
            sensorType.HasIndex(p => p.TypeName)
                .IsUnique();

            var userHasDevice = modelBuilder.Entity<UserHasDevice>();
            userHasDevice.ToTable("UsersHaveDevices");
            userHasDevice.HasKey(p => p.Id);
            userHasDevice.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            userHasDevice.HasOne(p => p.UserHasController)
                .WithMany(p => p.UsersHaveDevices)
                .OnDelete(DeleteBehavior.Cascade);
            userHasDevice.HasOne(p => p.Device)
                .WithMany(p => p.UsersHaveDevice)
                .OnDelete(DeleteBehavior.Cascade);

            var devices = modelBuilder.Entity<Device>();
            devices.ToTable("Devices");
            devices.HasKey(p => p.Id);
            devices.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            devices.Property(p => p.Status)
                .HasDefaultValue(false);
            devices.ToTable("Devices");
            devices.HasMany(p => p.UsersHaveDevice)
                .WithOne(p => p.Device)
                .OnDelete(DeleteBehavior.Cascade);
            devices.HasIndex(p => p.MAC)
                .IsUnique();

            var configurations = modelBuilder.Entity<DeviceConfiguration>();
            configurations.ToTable("DeviceConfigurations");
            configurations.HasKey(p => p.Id);
            configurations.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            configurations.HasOne(p => p.Device)
                .WithMany(p => p.DeviceConfigurations)
                .OnDelete(DeleteBehavior.Cascade);
            configurations.HasOne(p => p.Measure)
                .WithMany(p => p.DeviceConfigurations)
                .OnDelete(DeleteBehavior.Cascade);
            configurations.HasMany(p => p.Commands).WithOne(p => p.DeviceConfiguration);

            var measures = modelBuilder.Entity<Measure>();
            measures.ToTable("Measures");
            measures.HasKey(p => p.Id);
            measures.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            measures.HasMany(p => p.DeviceConfigurations)
                .WithOne(p => p.Measure)
                .OnDelete(DeleteBehavior.Cascade);
            measures.HasIndex(p => p.MeasureName)
                .IsUnique();

            var commands = modelBuilder.Entity<Command>();
            commands.ToTable("Commands");
            commands.HasKey(p => p.Id);
            commands.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            commands.HasOne(p => p.Script)
                .WithMany(p => p.Commands)
                .OnDelete(DeleteBehavior.Cascade);
            commands.HasOne(p => p.DeviceConfiguration)
                .WithMany(p => p.Commands)
                .OnDelete(DeleteBehavior.Cascade);

            var scripts = modelBuilder.Entity<Script>();
            scripts.ToTable("Scripts");
            scripts.HasKey(p => p.Id);
            scripts.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            scripts.Property(p => p.Priority)
                .HasDefaultValue(0);
            scripts.HasOne(p => p.ConditionType)
                .WithMany(p => p.Scripts)
                .OnDelete(DeleteBehavior.Cascade);
            scripts.HasOne(p => p.Sensor)
                .WithMany(p => p.Scripts)
                .OnDelete(DeleteBehavior.Cascade);
            scripts.HasMany(p => p.Commands)
                .WithOne(p => p.Script)
                .OnDelete(DeleteBehavior.Cascade);

            var conditionTypes = modelBuilder.Entity<ConditionType>();
            conditionTypes.ToTable("ConditionTypes");
            conditionTypes.HasKey(p => p.Id);
            conditionTypes.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            conditionTypes.HasMany(p => p.Scripts)
                .WithOne(p => p.ConditionType)
                .OnDelete(DeleteBehavior.Cascade);
            conditionTypes.HasIndex(p => p.Type)
                .IsUnique();
        }
        #endregion
    }
}
