using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace LazyLoading
{
    /// <summary>
    /// .Configuration.LazyLoadingEnabled = false;
    /// </summary>
    public class LazyLoadingDataContext:DbContext,IDisposable
    {
        public LazyLoadingDataContext()
            : base("DataContext")
        {
            base.Configuration.LazyLoadingEnabled = false;
        }

        #region NotVirtual
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Log> Logs { get; set; }
        #endregion

        public LazyLoadingDataContext(String connectionStrings) : base(connectionStrings) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.Delete("DataContext");
            modelBuilder.Configurations.Add(new DepartmentConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new LogConfiguration());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<LazyLoadingDataContext>());
        }
    }


    public class UnLazyLoadingDataContext : DbContext, IDisposable
    {
        public UnLazyLoadingDataContext()
            : base("DataContext")
        {
            base.Configuration.LazyLoadingEnabled = false;
        }

        #region NotVirtual
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Log> Logs { get; set; }
        #endregion

        public UnLazyLoadingDataContext(String connectionStrings) : base(connectionStrings) { }
    }
}
