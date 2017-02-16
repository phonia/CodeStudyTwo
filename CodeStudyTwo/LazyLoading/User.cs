using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace LazyLoading
{
    /// <summary>
    /// 不使用virtual
    /// </summary>
    public class User
    {
        public Int32 Id { get; set; }
        public String UserName { get; set; }
        public Department Department { get; set; }
        public IList<Log> Logs { get; set; }
    }

    public class Department
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
    }

    public class Log
    {
        public Int32 Id { get; set; }
        public String Message { get; set; }
    }


    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Sys_user");
            HasKey(e => e.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(e => e.UserName).IsRequired();
            HasRequired(e => e.Department).WithMany().Map(e => e.MapKey("DepartmentId"));
            HasMany(e => e.Logs).WithRequired().Map(e => e.MapKey("UserId"));
        }
    }

    public class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentConfiguration()
        {
            ToTable("Sys_department");
            HasKey(e => e.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }

    public class LogConfiguration : EntityTypeConfiguration<Log>
    {
        public LogConfiguration()
        {
            ToTable("Sys_log");
            HasKey(e => e.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }
}
