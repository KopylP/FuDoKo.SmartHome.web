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
                .WithOne(p => p.User);

            modelBuilder.Entity<UserHasController>()
                .ToTable("Users_Have_Controllers");
            modelBuilder.Entity<UserHasController>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<UserHasController>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<UserHasController>()
                .HasOne(p => p.User)
                .WithMany(p => p.UserHasControllers);
            modelBuilder.Entity<UserHasController>()
                .HasOne(p => p.Controller)
                .WithMany(p => p.UsersHaveController);
            modelBuilder.Entity<UserHasController>()
                .HasMany(p => p.UsersHaveDevices)
                .WithOne(p => p.UserHasController);

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
                .WithOne(p => p.Controller);
            modelBuilder.Entity<Controller>()
                .HasMany(p => p.Sensors)
                .WithOne(p => p.Controller);
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
                .WithMany(p => p.Sensors);
            sensor.HasOne(p => p.SensorType)
                .WithMany(p => p.Sensors);

            var sensorType = modelBuilder.Entity<SensorType>();
            sensorType.ToTable("Sensor_Types");
            sensorType.HasKey(p => p.Id);
            sensorType.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            sensorType.HasMany(p => p.Sensors)
                .WithOne(p => p.SensorType);
            sensorType.HasIndex(p => p.TypeName)
                .IsUnique();

            var userHasDevice = modelBuilder.Entity<UserHasDevice>();
            userHasDevice.ToTable("Users_Have_Devices");
            userHasDevice.HasKey(p => p.Id);
            userHasDevice.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            userHasDevice.HasOne(p => p.UserHasController)
                .WithMany(p => p.UsersHaveDevices);
            userHasDevice.HasOne(p => p.Device)
                .WithMany(p => p.UsersHaveDevice);

            var devices = modelBuilder.Entity<Device>();
            devices.ToTable("Devices");
            devices.HasKey(p => p.Id);
            devices.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            devices.Property(p => p.Status)
                .HasDefaultValue(false);
            devices.ToTable("Devices");
            devices.HasMany(p => p.UsersHaveDevice)
                .WithOne(p => p.Device);
            devices.HasIndex(p => p.MAC)
                .IsUnique();

            var configurations = modelBuilder.Entity<DeviceConfiguration>();
            configurations.ToTable("Device_Configurations");
            configurations.HasKey(p => p.Id);
            configurations.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            configurations.HasOne(p => p.Device)
                .WithMany(p => p.DeviceConfigurations);
            configurations.HasOne(p => p.Measure)
                .WithMany(p => p.DeviceConfigurations);
            configurations.HasMany(p => p.Commands).WithOne(p => p.DeviceConfiguration);

            var measures = modelBuilder.Entity<Measure>();
            measures.ToTable("Measures");
            measures.HasKey(p => p.Id);
            measures.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            measures.HasMany(p => p.DeviceConfigurations)
                .WithOne(p => p.Measure);
            measures.HasIndex(p => p.MeasureName)
                .IsUnique();

            var commands = modelBuilder.Entity<Command>();
            commands.ToTable("Commands");
            commands.HasKey(p => p.Id);
            commands.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            commands.HasOne(p => p.Script)
                .WithMany(p => p.Commands);
            commands.HasOne(p => p.DeviceConfiguration)
                .WithMany(p => p.Commands);

            var scripts = modelBuilder.Entity<Script>();
            scripts.ToTable("Scripts");
            scripts.HasKey(p => p.Id);
            scripts.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            scripts.Property(p => p.Priority)
                .HasDefaultValue(0);
            scripts.HasOne(p => p.ConditionType)
                .WithMany(p => p.Scripts);
            scripts.HasOne(p => p.Sensor)
                .WithMany(p => p.Scripts);
            scripts.HasMany(p => p.Commands)
                .WithOne(p => p.Script);

            var conditionTypes = modelBuilder.Entity<ConditionType>();
            conditionTypes.ToTable("Condition_Types");
            conditionTypes.HasKey(p => p.Id);
            conditionTypes.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            conditionTypes.HasMany(p => p.Scripts)
                .WithOne(p => p.ConditionType);
            conditionTypes.HasIndex(p => p.Type)
                .IsUnique();
        }
        #endregion
    }
}
